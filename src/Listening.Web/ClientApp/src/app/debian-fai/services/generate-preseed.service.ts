import { Injectable } from '@angular/core';
import { FileSystemType, PartitionType, PreseedSettingsViewModel } from 'apiDefinitions';

@Injectable({
  providedIn: 'root'
})
export class GeneratePreseedService {

  private _bootTemplate: string = `            _sz_ _sz_ _sz_ _fstp_                           \\
          $primary{ } $bootable{ }                \\
          method{ format } format{ }              \\
          use_filesystem{ } filesystem{ _fstp_ }    \\
          mountpoint{ /boot }                     \\
        .                                           \\ 
        `;

  private _swapTemplate = `            _sz_ _sz_ _sz_ linux-swap                           \\
          $lvmok{ }                               \\
          lv_name{ swap } in_vg { debian }        \\
          $primary{ }                             \\
          method{ swap } format{ }                \\
        .                                           \\ 
        `;

  private _template = `            _sz_ _sz_ _sz_ _fstp_                           \\
          $lvmok{ }                               \\
          lv_name{ _lvnm_ } in_vg { debian }        \\
          $primary{ }                             \\
          method{ format } format{ }              \\
          use_filesystem{ } filesystem{ _fstp_ }    \\
          mountpoint{ _mnpt_ }                         \\
        .                                           \\ 
        `;

  // private _dictionary: { [key: number]: string; } = {}
  private _dictionaryTemplate: Map<PartitionType, string>;
  // private _dictionaryTemplate: Map<PartitionType, string>;

  private diskClass: string = 'pi pi-disk text-info';
  private folderExpandedClass: string = 'pi pi-folder-open text-info';
  private folderCollapsedClass: string = 'pi pi-folder text-info';


  constructor() {
    this._dictionaryTemplate = new Map<PartitionType, string>();
    this._dictionaryTemplate.set(PartitionType.Boot, this._bootTemplate);
    this._dictionaryTemplate.set(PartitionType.Swap, this._swapTemplate);
    this._dictionaryTemplate.set(PartitionType.Root, this._template);
    this._dictionaryTemplate.set(PartitionType.Opt, this._template);
    this._dictionaryTemplate.set(PartitionType.Tmp, this._template);
    this._dictionaryTemplate.set(PartitionType.Var, this._template);
    this._dictionaryTemplate.set(PartitionType.VarLog, this._template);
  }

  private _findCapitalAndReplace(name: string, symbol: string): string {
    let letters = [name[0].toLowerCase()];

    for (let i = 1; i < name.length; i++)
      if (name[i] == name[i].toUpperCase())
        letters.push(`${symbol}${name[i].toLowerCase()}`);
      else
        letters.push(name[i]);

    return letters.join('');
  }

  private _getLvName(name: string): string {
    const result = this._findCapitalAndReplace(name, '_');
    return result;
  }

  private _getMountPoint(name: string): string {
    const result = this._findCapitalAndReplace(name, '/');
    return `/${result}`;
  }

  getPreseed(settings: PreseedSettingsViewModel): string {
    const generatedHddPartitions = [];

    for (let i = 0; i < settings.hddSplitSettingsVM.configs.length; i++) {
      const config = settings.hddSplitSettingsVM.configs[i];
      const template = this._dictionaryTemplate.get(config.partitionType);
      generatedHddPartitions.push(
        template.replace(new RegExp('_sz_', 'g'), config.size.toString())
          .replace(new RegExp('_fstp_', 'g'), FileSystemType[config.fileSystemType].toLowerCase())
          .replace(new RegExp('_lvnm_', 'g'), this._getLvName(PartitionType[config.partitionType]))
          .replace(new RegExp('_mnpt_', 'g'), this._getMountPoint(PartitionType[config.partitionType]))
      );
    }

    const hddSplit = `
            d-i partman-auto/expert_recipe string               \
              boot-lvm ::                                     \
              ${generatedHddPartitions.join('\n\n')}
              `;

    const pressed = `# US locale/kbd map
        d-i debian-installer/locale string en_US
        d-i keyboard-configuration/xkb-keymap select us

        # automatically select network interface
        d-i netcfg/choose_interface select auto

        # set host and domain
        d-i netcfg/get_hostname string debian-pxe
        d-i netcfg/get_domain string localdomain

        # disable WEP dialogue
        d-i netcfg/wireless_wep string

        # use http.us.debian.org as mirror with no proxy
        d-i mirror/country string manual
        #d-i mirror/http/hostname string http.us.debian.org
        d-i mirror/http/hostname string ${settings.mirror} #debian.org.ua
        d-i mirror/http/directory string /debian
        d-i mirror/http/proxy string
        #d-i apt-setup/no_mirror boolean true
        d-i apt-setup/cdrom/set-first boolean false

        ## don't make a regular user / set root password
        #d-i passwd/make-user boolean false
        d-i passwd/root-password password ${settings.rootPassword}
        d-i passwd/root-password-again password ${settings.rootPassword}
        #d-i passwd/root-password-crypted password $6$QUL/zO7kMOSMUgzk$DSeEAuWSbVfvZKReowmeLP9U4rjyhyG3B2oZ9nClha.CbO1WkZrVKLewtYJhjnNY32BWTKyIkJSDAl3VhFDB4/

        # Alternatively, you can preseed the user's name and login.
        passwd passwd/user-fullname string ${settings.userFullName}
        passwd passwd/username string ${settings.userName}
        # And their password, but use caution!
        passwd passwd/user-password password ${settings.userPassword}
        passwd passwd/user-password-again password ${settings.userPassword}

        # hardware clock is UTC, timezone is US/Eastern, use ntp to set clock
        d-i clock-setup/utc boolean true
        d-i time/zone string US/Eastern
        d-i clock-setup/ntp boolean true

        # use lvm partitioning
        d-i partman-auto/method string lvm
        d-i partman-lvm/device_remove_lvm boolean true
        d-i partman-lvm/confirm boolean true
        d-i partman-lvm/confirm_nooverwrite boolean true

        # make lvm the max size
        d-i partman-auto-lvm/guided_size string max
        d-i partman-auto-lvm/new_vg_name string debian

        # use the following partition scheme on /dev/sda
        d-i partman-auto/disk string /dev/sda
        d-i partman-auto/choose_recipe select boot-lvm

        # hdd split
        ${hddSplit}

        # remove any RAID partitioning
        d-i partman-md/device_remove_md boolean true

        # don't confirm anything
        d-i partman-basicfilesystems/no_mount_point boolean false
        d-i partman-partitioning/confirm_write_new_label boolean true
        d-i partman/choose_partition select finish
        d-i partman/confirm boolean true
        d-i partman/confirm_nooverwrite boolean true

        # setup non-free and contrib repositories
        d-i apt-setup/non-free boolean true
        d-i apt-setup/contrib boolean true

        # install standard system with ssh-server
        tasksel tasksel/first multiselect standard, ssh-server

        # also install the htop package
        # xfce iceweasel
        d-i pkgsel/include string ${settings.additionalSoft}

        # upgrade all packages
        d-i pkgsel/upgrade select full-upgrade

        # disable popularity contest
        popularity-contest popularity-contest/participate boolean false

        # force grub install to /dev/sda
        d-i grub-installer/only_debian boolean true
        d-i grub-installer/with_other_os boolean true
        d-i grub-installer/bootdev  string /dev/sda

        #d-i preseed/late_command string \
        #mkdir /target/root/InstallScripts; \

        #base-config base-config/late_command string apt-get install zsh;

        # don't wait for confirm, just reboot when finished
        d-i finish-install/reboot_in_progress note
        `;

    return pressed;
  }

}

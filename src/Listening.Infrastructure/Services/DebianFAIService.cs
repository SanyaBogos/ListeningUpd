using Listening.Core.ViewModels.DebianFAI;
using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Entities.Specialized.ServiceModels.DebianFAI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services
{
    public class DebianFAIService : BaseFileService, IDebianFAIService
    {
        private const string PreseedFileName = "preseed.cfg";

        private readonly IWebPageService _webPageService;
        private readonly string _defaultDownloadUrl;
        private readonly string _defaultDownloadUrl2;
        private readonly string _archieve;
        private readonly int _maxTimeMs;
        private readonly string _tempIsoPath;
        private readonly string _resultIsoPath;
        private readonly ILogger<DebianFAIService> _logger;
        private readonly bool _isProd;
        private readonly Dictionary<DeviceType, string> _typeNames;
        private readonly Dictionary<PreseedSettingsViewModel, string> _settingsIdsDictionary;
        private readonly object _lock = new object();

        private int _folderId;

        private ArhitecturesWithTime _arhitecturesWithTime;

        public DebianFAIService(IConfiguration configuration,
            IWebPageService webPageService,
            IWebHostEnvironment env,
            ILogger<DebianFAIService> logger) : base(configuration)
        {
            _folderId = 0;
            _settingsIdsDictionary = new Dictionary<PreseedSettingsViewModel, string>();

            _webPageService = webPageService;
            _defaultDownloadUrl = configuration["Data:Debian:DefaultDownloadURL"];
            _defaultDownloadUrl2 = configuration["Data:Debian:DefaultDownloadURL2"];
            _archieve = configuration["Data:Debian:Archieve"];
            _maxTimeMs = Convert.ToInt32(configuration["Data:Debian:TimeToLive"]);
            _tempIsoPath = $"{Directory.GetCurrentDirectory()}/bin{configuration["Data:FileStorage:TempISOGen"]}";
            _resultIsoPath = $"{env.WebRootPath}{configuration["Data:FileStorage:DebianResults"]}";
            _logger = logger;
            _isProd = env.EnvironmentName == "Production";

            _typeNames = new Dictionary<DeviceType, string>()
            {
                { DeviceType.USB, "hd-media" },
                { DeviceType.CD, "cdrom" },
            };

            if (!Directory.Exists(_tempIsoPath))
                Directory.CreateDirectory(_tempIsoPath);

            if (!Directory.Exists(_resultIsoPath))
                Directory.CreateDirectory(_resultIsoPath);

            DebianCleanupService.TempIsoPath = _tempIsoPath;
        }

        public string GetPreseed(PreseedSettingsViewModel settings)
        {
            var pressed = $@"# US locale/kbd map
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
d-i mirror/http/hostname string {settings.Mirror}
d-i mirror/http/directory string /debian
d-i mirror/http/proxy string
#d-i apt-setup/no_mirror boolean true
d-i apt-setup/cdrom/set-first boolean false

## don't make a regular user / set root password
#d-i passwd/make-user boolean false
d-i passwd/root-password password {settings.RootPassword}
d-i passwd/root-password-again password {settings.RootPassword}
#d-i passwd/root-password-crypted password $6$QUL/zO7kMOSMUgzk$DSeEAuWSbVfvZKReowmeLP9U4rjyhyG3B2oZ9nClha.CbO1WkZrVKLewtYJhjnNY32BWTKyIkJSDAl3VhFDB4/

# Alternatively, you can preseed the user's name and login.
passwd passwd/user-fullname string {settings.UserFullName}
passwd passwd/username string {settings.UserName}
# And their password, but use caution!
passwd passwd/user-password password {settings.UserPassword}
passwd passwd/user-password-again password {settings.UserPassword}

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

# /boot 500M ext4
# swap 2G
# /tmp 2G ext4
# /var/log 4G ext4
# / 8G+ ext4
{settings.HddSplitSettingsVM}

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
d-i pkgsel/include string {settings.AdditionalSoft}

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
";

            return pressed;
        }

        public PreseedSettingsViewModel GetDefaultSettings()
        {
            var arhitectures = GetArhitectures().Keys.ToArray();

            var soft = !_isProd ?
                "htop lxde iceweasel linux-headers-amd64 dkms apt-transport-https curl tesseract-ocr conky conky-all lm-sensors hddtemp dirmngr git mc vim gxneur gedit libgdiplus clamav bsdtar genisoimage tig"
                : "htop lxde iceweasel linux-headers-amd64 dkms apt-transport-https curl";

            var settings = new PreseedSettingsViewModel
            {
                Mirror = "deb.debian.org",
                RootPassword = "r00tme",
                UserFullName = "Vasa",
                UserName = "vasa",
                UserPassword = "r00tme1",
                AdditionalSoft = soft,
                CurrentArchitectures = arhitectures,
                HddSplitSettingsVM = new HddSplitSettingsViewModel
                {
                    Configs = new PartitionConfig[] {
                        new PartitionConfig
                        {
                            PartitionType = PartitionType.boot,
                            FileSystemType = FileSystemType.ext2,
                            Size = 200
                        },
                        new PartitionConfig
                        {
                            PartitionType = PartitionType.swap,
                            FileSystemType = FileSystemType.swap,
                            Size = 2048
                        },
                        new PartitionConfig
                        {
                            PartitionType = PartitionType.var,
                            FileSystemType = FileSystemType.ext4,
                            Size = 2048
                        },
                        new PartitionConfig
                        {
                            PartitionType = PartitionType.tmp,
                            FileSystemType = FileSystemType.ext4,
                            Size = 2048
                        },
                        new PartitionConfig
                        {
                            PartitionType = PartitionType.root,
                            FileSystemType = FileSystemType.ext4,
                            Size = 8192
                        },
                    }
                }
            };

            return settings;
        }

        private void CheckUrlSettings(PreseedSettingsViewModel settings)
        {
            // var digitsRegex = new Regex(@"^[0-9.]+$");
            // var charsRegex = new Regex(@"^[a-z_0-9-]+$");
            var nameRegex = new Regex(@"^[A-Za-z_0-9-.]+$");

            if (!(nameRegex.IsMatch(settings.ImageConfig.Name) || nameRegex.IsMatch(settings.ImageConfig.Name)))
                throw new DebianException("wrng_ver");

            // if (!(digitsRegex.IsMatch(settings.Version) || charsRegex.IsMatch(settings.Version)))
            //     throw new DebianException("wrng_ver");

            // var arhitectures = GetArhitectures().Select(x => x.Key).ToArray();

            // if (!arhitectures.Contains(settings.Architecture))
            //     throw new DebianException("wrng_arch");

            // var imgNameRegex = new Regex(@"^[a-z_0-9-]+$");

            // if (!imgNameRegex.IsMatch(settings.ImageName))
            //     throw new DebianException("wrng_img");
        }

        public string GetImage(PreseedSettingsViewModel settings)
        {
            if (_settingsIdsDictionary.ContainsKey(settings))
                return _settingsIdsDictionary[settings];

            CheckUrlSettings(settings);

            var preseed = GetPreseed(settings);
            var path = $"{_tempIsoPath}{++_folderId}";
            Directory.CreateDirectory(path);

            File.WriteAllText($"{path}/{PreseedFileName}", preseed);

            string downloadLink = null;//, distributiveName, distributiveNameOnly, distributiveResultName;

            if (settings.ImageConfig.Name == "crnt")
                downloadLink = GetArhitectures()[settings.ImageConfig.ArchitectureType];
            else
                downloadLink = $"{this._archieve}{settings.ImageConfig.ToString()}";

            var distributiveName = downloadLink.Split("/").Last();
            var distributiveNameOnly = distributiveName.Substring(0, distributiveName.LastIndexOf('.'));
            var distributiveResultName = $"{distributiveNameOnly}-{_folderId}-PRESEEDED.iso";

            lock (_lock)
            {
                var isFileExist = File.Exists($"{_tempIsoPath}/{distributiveName}");

                if (settings.ForceDownload || !isFileExist)
                {
                    if (isFileExist)
                        File.Delete($"{_tempIsoPath}/{distributiveName}");

                    _webPageService.DownloadFile(downloadLink, _tempIsoPath);

                    if (settings.ImageConfig.UrlType == UrlType.jigdo)
                    {
                        var additionLink = $"{downloadLink.Substring(0, downloadLink.LastIndexOf("."))}.template";
                        _webPageService.DownloadFile(additionLink, _tempIsoPath);
                        var additionalResult = $"jigdo-lite --noask {distributiveName}".Bash($"{path}/..");

                        if (!File.Exists($"{_tempIsoPath}/{distributiveNameOnly}.iso"))
                            throw new DebianException("file_not_found");
                    }
                }
            }



            var command = $@"
# algorythm of FAI debian

# 1. Extract files

	#7z x -oisofiles ../{distributiveName};
    mkdir isofiles;
    bsdtar -C isofiles -xf ../{distributiveNameOnly}.iso;

# 2. put preseed files

	cp preseed.cfg isofiles/;
	
# 3. set autostart to usb flash drive

	#sed -i 's|.$|	append auto=true priority=high locale=en_GB.UTF-8 keymap=gb file=/hd-media/preseed.cfg vga=788 initrd=/install.amd/initrd.gz --- quiet |g' isolinux/txt.cfg > barada.txt	
	#sed -i 's|.$|	append auto=true priority=high locale=en_GB.UTF-8 keymap=gb file=/hd-media/preseed.cfg vga=788 initrd=/install.amd/initrd.gz --- quiet |' txt.cfg
	
	sed -i '$d' isofiles/isolinux/txt.cfg;
	sed -i '$s/$/\n         	append auto=true priority=high locale=en_GB.UTF-8 keymap=gb file=\/{_typeNames[settings.DeviceType]}\/preseed.cfg vga=788 initrd=\/install.amd\/initrd.gz --- quiet /' isofiles/isolinux/txt.cfg;
	
	# sed -i '$s/$/\n         	append auto=true priority=high locale=en_GB.UTF-8 keymap=gb file=\/cdrom\/preseed.cfg vga=788 initrd=\/install.amd\/initrd.gz --- quiet /' isofiles/isolinux/txt.cfg
	
# 4. generate image
	
	genisoimage -r -J -b isolinux/isolinux.bin -c isolinux/boot.cat -no-emul-boot -boot-load-size 4 -boot-info-table -o {distributiveResultName} isofiles/;
	
";

            var result = command.Bash(path);

            _logger.LogInformation(result);

            _settingsIdsDictionary.Add(settings, distributiveResultName);
            File.Copy($"{path}/{distributiveResultName}", $"{_resultIsoPath}/{distributiveResultName}", true);

            return distributiveResultName;
        }

        public async Task<byte[]> GetFileBytes(string distributiveResultName)
        {
            var bytes = await File.ReadAllBytesAsync($"{_resultIsoPath}/{distributiveResultName}");
            return bytes;
        }

        private Dictionary<ArchitectureType, string> GetArhitectures()
        {
            var page = _webPageService.GetHtmlByUrl(_defaultDownloadUrl2);

            if (_arhitecturesWithTime == null || _arhitecturesWithTime.Time.AddMilliseconds(_maxTimeMs) < DateTime.Now)
            {
                var architectures = _webPageService.GetArhitecturesDictionary(page);
                _arhitecturesWithTime = new ArhitecturesWithTime
                {
                    Arhitectures = architectures,
                    Time = DateTime.Now
                };
            }

            return _arhitecturesWithTime.Arhitectures;
        }
    }
}

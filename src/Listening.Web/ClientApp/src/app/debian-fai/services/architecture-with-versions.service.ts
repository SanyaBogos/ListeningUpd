import { Injectable } from '@angular/core';
import { ArchitectureType, UrlType } from 'apiDefinitions';
import { VersionCompressed } from '../models/version';

@Injectable({
  providedIn: 'root'
})
export class ArchitectureWithVersionsService {

  private _versionsCompressed: VersionCompressed[];

  constructor(
  ) {
    this._versionsCompressed = this.getAllVersionsCompressed();
  }

  getVersionNames(): string[] {
    var result = this._versionsCompressed.map(x => x.name);
    return result;
  }

  getArchitectures(name: string): ArchitectureType[] {
    var version = this._versionsCompressed.find(x => x.name === name);
    var archNumber = version.stngs[0];
    var archs: ArchitectureType[] = [];
    var sum: number = 0;

    for (const item in Object.keys(ArchitectureType)) {
      const val = 1 << Number(item);
      sum += val;

      if ((archNumber & val) > 0)
        archs.push(Number(item));

      if (sum >= archNumber)
        break;
    }

    return archs;
  }

  getUrlType(name: string): UrlType {
    const version = this._versionsCompressed.find(x => x.name === name);
    const urlTypeIndex = version.stngs[1];
    return urlTypeIndex;
  }

  getAllVersionsCompressed(): VersionCompressed[] {
    const result = [
      // stngs - is settings; first item is summarize of arhitectures (binary encoding)
      // alpha = 1 means 
      // { name: '3.1_r0a', stngs: [1535, 1, 1] },
      // { name: '3.1_r2', stngs: [1535, 1, 1] },

      // // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace
      // { name: '3.1_r3', stngs: [1535, 1, 1] },
      // { name: '3.1_r4', stngs: [1535, 1, 1] },
      // { name: '3.1_r5', stngs: [1535, 1, 1] },

      // // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace, amd64
      // { name: '3.1_r6a', stngs: [1535, 1, 1] },
      // { name: '3.1_r7', stngs: [1535, 1, 1] },
      // { name: '3.1_r8', stngs: [1535, 1, 1] },

      // // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace, amd64, multi-arch
      // { name: '4.0_r0', stngs: [5599, 1, 1] },
      // { name: '4.0_r1', stngs: [5599, 1, 1] },
      // { name: '4.0_r2', stngs: [5599, 1, 1] },
      // { name: '4.0_r3', stngs: [5599, 1, 1] },
      // { name: '4.0_r4', stngs: [5599, 1, 1] },
      // { name: '4.0_r4a', stngs: [5599, 1, 1] },
      // { name: '4.0_r5', stngs: [5599, 1, 1] },
      // { name: '4.0_r6', stngs: [5599, 1, 1] },
      // { name: '4.0_r7', stngs: [5599, 1, 1] },
      // { name: '4.0_r8', stngs: [5599, 1, 1] },
      // { name: '4.0_r9', stngs: [5599, 1, 1] },

      // // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace, amd64, multi-arch, armel
      // { name: '5.0.0', stngs: [21983, 1, 1] },
      // { name: '5.0.1', stngs: [21983, 1, 1] },
      // { name: '5.0.2', stngs: [21983, 1, 1] },
      // { name: '5.0.2a', stngs: [21983, 1, 1] },
      // { name: '5.0.3', stngs: [21983, 1, 1] },
      // { name: '5.0.4', stngs: [21983, 1, 1] },
      // { name: '5.0.5', stngs: [21983, 1, 1] },
      // { name: '5.0.6', stngs: [21983, 1, 1] },
      // { name: '5.0.7', stngs: [21983, 1, 1] },
      // { name: '5.0.8', stngs: [21983, 1, 1] },
      // { name: '5.0.9', stngs: [21983, 1, 1] },
      // { name: '5.0.10', stngs: [21983, 1, 1] },

      // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace, amd64, multi-arch, armel, kfreebsd-amd64, kfreebsd-i386
      { name: '6.0.0', stngs: [120280, 4, 2] },
      { name: '6.0.1a', stngs: [120280, 4, 2] },
      { name: '6.0.2.1', stngs: [120280, 4, 2] },
      { name: '6.0.3', stngs: [120280, 4, 2] },
      { name: '6.0.4', stngs: [120280, 4, 2] },
      { name: '6.0.5', stngs: [120280, 4, 2] },
      { name: '6.0.6', stngs: [120280, 4, 2] },
      { name: '6.0.7', stngs: [120280, 4, 2] },
      { name: '6.0.8', stngs: [120280, 4, 2] },
      { name: '6.0.9', stngs: [120280, 4, 2] },
      { name: '6.0.10', stngs: [120280, 1, 2] },

      // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace, amd64, multi-arch, armel, kfreebsd-amd64, kfreebsd-i386, armhf, s390x
      { name: '7.0.0', stngs: [514008, 4, 2] },
      { name: '7.1.0', stngs: [514008, 4, 2] },
      { name: '7.2.0', stngs: [514008, 4, 2] },
      { name: '7.3.0', stngs: [514008, 4, 2] },
      { name: '7.4.0', stngs: [514008, 4, 2] },
      { name: '7.5.0', stngs: [514008, 4, 2] },
      { name: '7.6.0', stngs: [514008, 4, 2] },
      { name: '7.7.0', stngs: [514008, 4, 2] },
      { name: '7.8.0', stngs: [514008, 4, 2] },
      { name: '7.9.0', stngs: [514008, 4, 2] },
      { name: '7.10.0', stngs: [514008, 4, 2] },
      { name: '7.11.0', stngs: [514008, 1, 2] },

      // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace, amd64, multi-arch, armel, kfreebsd-amd64, kfreebsd-i386, armhf, s390x, arm64, ppc64el
      { name: '8.0.0', stngs: [1987016, 4, 2] },
      { name: '8.1.0', stngs: [1987016, 4, 2] },
      { name: '8.2.0', stngs: [1987016, 4, 2] },
      { name: '8.3.0', stngs: [1987016, 4, 2] },
      { name: '8.4.0', stngs: [1987016, 4, 2] },
      { name: '8.5.0', stngs: [1987016, 4, 2] },
      { name: '8.6.0', stngs: [1987016, 4, 2] },
      { name: '8.7.0', stngs: [1987016, 4, 2] },
      { name: '8.7.1', stngs: [1987016, 4, 2] },
      { name: '8.8.0', stngs: [1987016, 4, 2] },
      { name: '8.9.0', stngs: [1987016, 4, 2] },
      { name: '8.10.0', stngs: [1987016, 1, 2] },
      { name: '8.11.0', stngs: [1987016, 1, 2] },
      { name: '8.11.1', stngs: [151560, 1, 2] },

      // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace, amd64, multi-arch, armel, kfreebsd-amd64, kfreebsd-i386, armhf, s390x, arm64, ppc64el, mips64el
      { name: '9.0.0', stngs: [4083912, 4, 2] },
      { name: '9.1.0', stngs: [4083912, 4, 2] },
      { name: '9.2.0', stngs: [4083912, 4, 2] },
      { name: '9.2.1', stngs: [4083912, 1, 2] },
      { name: '9.3.0', stngs: [4083912, 4, 2] },
      { name: '9.4.0', stngs: [4083912, 4, 2] },
      { name: '9.5.0', stngs: [4083912, 4, 2] },
      { name: '9.6.0', stngs: [4083912, 4, 2] },
      { name: '9.7.0', stngs: [4083912, 4, 2] },
      { name: '9.8.0', stngs: [4083912, 4, 2] },
      { name: '9.9.0', stngs: [4083912, 4, 2] },
      { name: '9.11.0', stngs: [4083912, 4, 2] },
      { name: '9.12.0', stngs: [4083912, 4, 2] },
      { name: '9.13.0', stngs: [4083912, 1, 2] },
      { name: '10.0.0', stngs: [4083912, 4, 2] },
      { name: '10.1.0', stngs: [4083912, 4, 2] },
      { name: '10.2.0', stngs: [4083912, 4, 2] },
      { name: '10.3.0', stngs: [4083912, 4, 2] },
      { name: '10.4.0', stngs: [4083912, 4, 2] },
      { name: '10.5.0', stngs: [4083912, 4, 2] },
      { name: '10.6.0', stngs: [4083912, 4, 2] },
      { name: '10.7.0', stngs: [4083912, 1, 2] },
      { name: '10.8.0', stngs: [4083912, 1, 2] },
      { name: '10.9.0', stngs: [4083912, 1, 2] },
      { name: '10.10.0', stngs: [4083912, 1, 2] },
      { name: '10.11.0', stngs: [4083912, 1, 2] },
      { name: '11.0.0', stngs: [4083848, 1, 2] },
      { name: 'bullseye-DI-alpha1', stngs: [4083848, 1, 4] },
      { name: 'bullseye-DI-alpha2', stngs: [4083848, 1, 4] },
      { name: 'bullseye-DI-alpha3', stngs: [4083848, 1, 4] },
      { name: 'bullseye-DI-rc1', stngs: [4083848, 1, 4] },
      { name: 'bullseye-DI-rc2', stngs: [4083848, 1, 4] },
      { name: 'bullseye-DI-rc3', stngs: [4083848, 1, 4] },
      { name: 'buster-DI-alpha1', stngs: [1986760, 1, 4] },
      { name: 'buster-DI-alpha2', stngs: [4083912, 1, 4] },
      { name: 'buster-DI-alpha3', stngs: [4083912, 1, 4] },
      { name: 'buster-DI-alpha4', stngs: [4083912, 1, 4] },
      { name: 'buster-DI-alpha5', stngs: [4083912, 1, 4] },
      { name: 'buster-DI-rc1', stngs: [4083912, 1, 4] },
      { name: 'buster-DI-rc2', stngs: [4083912, 1, 4] },
      { name: 'buster-DI-rc3', stngs: [4083912, 1, 4] },
      // { name: 'latest-oldoldstable', stngs: [] },
      // { name: 'latest-oldstable', stngs: [] },

      // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace, amd64, multi-arch, armel, kfreebsd-amd64, kfreebsd-i386, armhf, s390x, arm64, ppc64el, mips64el, 1.3, 2.0, 2.1, 2.2
      // { name: 'older-contrib', stngs: [] },
      // { name: 'project', stngs: [] },
      // { name: 'stretch-DI-alpha1', stngs: [] },
      { name: 'stretch-DI-alpha2', stngs: [1987016, 1, 4] },
      { name: 'stretch-DI-alpha3', stngs: [1987016, 1, 4] },
      { name: 'stretch-DI-alpha4', stngs: [1987016, 1, 4] },
      { name: 'stretch-DI-alpha5', stngs: [1987016, 1, 4] },
      { name: 'stretch-DI-alpha6', stngs: [1987016, 1, 4] },
      { name: 'stretch-DI-alpha7', stngs: [1987016, 1, 4] },
      { name: 'stretch-DI-alpha8', stngs: [1986760, 1, 4] },
      { name: 'stretch-DI-rc1', stngs: [1986760, 1, 4] },
      { name: 'stretch-DI-rc2', stngs: [1986760, 1, 4] },
      { name: 'stretch-DI-rc3', stngs: [1986760, 1, 4] },
      { name: 'stretch-DI-rc4', stngs: [4083912, 1, 4] },
      { name: 'stretch-DI-rc5', stngs: [4083912, 1, 4] },

      // alpha, arm, hppa, i386, ia64, m68k, mips, mipsel, powerpc, s390, sparc, trace, amd64, multi-arch, armel, kfreebsd-amd64, kfreebsd-i386, armhf, s390x, arm64, ppc64el, mips64el, 1.3, 2.0, 2.1, 2.2
    ];

    return result;
  }
}

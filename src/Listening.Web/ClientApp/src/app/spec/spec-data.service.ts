import { Injectable } from '@angular/core';
import { AppService } from '@app/app.service';
import { VideoDescription } from '@app/appshared/models/videoDescrption';
import { Book } from './models/book';
import { TreeNode } from 'primeng/api';
import { CourseDto, SpecClient, VideoDto } from 'apiDefinitions';
import { Video } from './models/video';

@Injectable()
export class SpecDataService {

  private videoClass: string = 'pi pi-video';
  private videoExpandedClass: string = 'pi pi-folder-open text-info';
  private videoCollapsedClass: string = 'pi pi-folder text-info';

  constructor(
    private appService: AppService,
    private specClient: SpecClient,
  ) { }

  getFoldersAndFiles(course: CourseDto, videoId: number): TreeNode[] {
    let basePath = `archive/${course.type}/${course.author}`
    const nodes: TreeNode[] = [];

    for (let i = 0; i < course.folders.length; i++) {
      const fldr = course.folders[i];
      let node: TreeNode;

      if (fldr.name.indexOf('/') > -1) {

        const subNames = fldr.name.split('/');

        // build nodes - insert into nodes; WARNING nodes varible will be changed inside
        if (!nodes || nodes.length === 0) {
          node = this._initNodesByName(subNames, nodes);
        } else {
          node = this._insertNode(subNames, nodes);
        }

      } else {
        node = <TreeNode>{
          label: fldr.name,
          expandedIcon: this.videoExpandedClass,
          collapsedIcon: this.videoCollapsedClass,
        };

        nodes.push(node);
      }

      const children = fldr.videos.map(video => {

        const path = `${basePath}/${fldr.path}`;

        if (video.repeat === 0)
          return this._noRepeate(video, path);

        const children = this._repeate(video, path);

        const resultFolder = <TreeNode>{
          label: video.name,
          expandedIcon: this.videoExpandedClass,
          collapsedIcon: this.videoCollapsedClass,
          children: children
        };

        return resultFolder;
      });

      if (node.children && node.children.length > 0)
        node.children.push(...children);
      else
        node.children = children;

    }

    return nodes;
  }

  private _insertNode(subNames: string[], allNodes: TreeNode[]): TreeNode {
    let found: TreeNode;
    let currentNode: TreeNode;
    let nodesArr: TreeNode[] = allNodes;

    for (let i = 0; i < subNames.length; i++) {
      const item = subNames[i];

      if (nodesArr)
        found = nodesArr.find(x => x.label === item);

      if (found)
        currentNode = found;
      else {
        const newNode = this._createNewNode(item);
        currentNode.children ? currentNode.children.push(newNode) : currentNode.children = [newNode];
        currentNode = newNode;
      }

      nodesArr = currentNode.children;
    }

    return currentNode;
  }

  private _initNodesByName(subNames: string[], nodes: TreeNode[]): TreeNode {
    const headNode: TreeNode = this._createNewNode(subNames[0]);
    let currentNode: TreeNode = headNode;

    for (let i = 1; i < subNames.length; i++) {
      const item = subNames[i];
      const newNode = this._createNewNode(item);
      currentNode.children = [newNode];
      currentNode = newNode;
    }

    nodes.push(headNode);
    return currentNode;
  }

  private _createNewNode(name: string): TreeNode {
    const node = <TreeNode>{
      label: name,
      expandedIcon: this.videoExpandedClass,
      collapsedIcon: this.videoCollapsedClass,
    };

    return node;
  }

  private _repeate(video: VideoDto, path: string): TreeNode[] {
    const children: TreeNode[] = [];

    for (let i = 0; i < video.repeat; i++) {
      const data = {
        id: video.id,
        path: `${path}/${video.path}-${i}.${video.ext}`,
        description: video.description,
      } as Video;

      const item = <TreeNode>{
        label: `${video.name} - ${i + 1}`,
        icon: this.videoClass,
        data: JSON.stringify(data)
      };

      children.push(item);
    }

    return children;
  }

  private _noRepeate(video: VideoDto, path: string): TreeNode {
    const data = {
      id: video.id,
      path: `${path}/${video.path}.${video.ext}`,
      description: video.description,
    } as Video;

    const child = <TreeNode>{
      label: video.name,
      icon: this.videoClass,
      data: JSON.stringify(data)
    };

    return child;
  }

  getVideoClass(): string {
    return this.videoClass;
  }

  getSpecInfo(id: number) {
    this.specClient.getVideoDescriptionList(id)
      .subscribe(res => {
        const basePath = `archive/${res.type}/${res.name}`;

        const folders = res.folders.map(folder => {

          const videos = folder.videos.map(video => {
            const treeVideo = <TreeNode>{
              label: video.name,
              icon: this.videoClass,
              data: `${basePath}/${folder.path}/${video.path}`
            };

            return treeVideo;
          });

          const treeFolder = <TreeNode>{
            label: folder.name,
            expandedIcon: this.videoExpandedClass,
            collapsedIcon: this.videoCollapsedClass,
            children: videos
          };

          return treeFolder;
        });

        return folders;
      });
  }

  getShirBooks(): Book[] {
    const content = this.appService.appData.content;
    const ecoTech = content['eco_tech'];

    const books: Book[] = [
      {
        name: content['present'],
        file: 'present',
        type: 'pdf',
        folder: 'shir'
      },
      {
        name: content['straw'],
        file: 'straw',
        type: 'pdf',
        folder: 'shir'
      },
      {
        name: `${ecoTech} 1`,
        file: 'eco_tech_1',
        type: 'pdf',
        folder: 'shir'
      },
      {
        name: `${ecoTech} 2`,
        file: 'eco_tech_2',
        type: 'pdf',
        folder: 'shir'
      },
    ];

    return books;
  }

  getKovBooks(): Book[] {
    const books: Book[] = [
      {
        name: this.appService.appData.content['iz_kvart'],
        file: 'iz_kvart',
        type: 'pdf',
        folder: 'kov'
      }
    ];

    return books;
  }

  getKedrBooks(): Book[] {
    const content = this.appService.appData.content;

    const books: Book[] = [
      {
        name: content['dom_iz_samana'],
        file: 'dom_iz_samana',
        type: 'pdf',
        folder: 'ked'
      },
      {
        name: content['kladka_pech'],
        file: 'kladka_pech',
        type: 'pdf',
        folder: 'ked'
      },
      {
        name: content['shtuk_dekor'],
        file: 'shtuk_dekor',
        type: 'djvu',
        folder: 'ked'
      },
      {
        name: content['sovrem_krovlya'],
        file: 'sovrem_krovlya',
        type: 'pdf',
        folder: 'ked'
      },
      {
        name: content['entsikl_rab_po_der'],
        file: 'entsikl_rab_po_der',
        type: 'pdf',
        folder: 'ked'
      },
      {
        name: content['haslak_obrb_derev'],
        file: 'haslak_obrb_derev',
        type: 'djvu',
        folder: 'ked'
      },
    ];

    return books;
  }

  getVideoShirBaseDescriptions(): VideoDescription[] {
    const basePath = 'archive/ecobuild/shirokov/base/';

    const videoBaseDescriptions: VideoDescription[] = [
      { name: 'shir_intro', src: `${basePath}01_vvedenie_istoria_razvitia_energoinformatika_`, type: 'mp4', isAllowed: true },
      { name: 'shir_pechi', src: `${basePath}02_1_Konstruktive_pechi_zolotoe_sechenie`, type: 'mp4', isAllowed: true },
      { name: 'shir_pechi2', src: `${basePath}02_2_Konstruktive_pechi_zolotoe_sechenie`, type: 'mp4', isAllowed: true },
      { name: 'shir_ecodesign', src: `${basePath}03_ecodesign_primery_proektov`, type: 'mp4', isAllowed: true },
      { name: 'shir_nuances', src: `${basePath}04_nuansy_stroitelstva`, type: 'mp4', isAllowed: true },
      { name: 'shir_permaculture', src: `${basePath}05_permacultura`, type: 'mp4', isAllowed: true },
      { name: 'shir_permaculture2', src: `${basePath}06_permacultura_prodolzhenie`, type: 'mp4', isAllowed: true },
      { name: 'shir_select_area', src: `${basePath}07_vibor_uchastka`, type: 'mp4', isAllowed: true },
      // { name: 'shir_clear', src: '08_1_chistka.mp4', type: 'mp4', isAllowed: true },
      { name: 'shir_clear', src: `${basePath}08_1_chistka_`, type: 'mp4', isAllowed: true },
      { name: 'shir_pechi_nuans', src: `${basePath}09_pechi_njuanses`, type: 'mp4', isAllowed: true },
      { name: 'shir_answers', src: `${basePath}10_Otvety_na_voprosy`, type: 'mp4', isAllowed: true },
      { name: 'shir_last', src: `${basePath}11_`, type: 'mp4', isAllowed: true },
    ];

    return videoBaseDescriptions;
  }

  getShirBaseVideo(): TreeNode[] {
    const basePath = 'archive/ecobuild/shirokov/base';
    const content = this.appService.appData.content;

    const intro = content['shir_intro'];
    const pechi = content['shir_pechi'];
    const pechi2 = content['shir_pechi2'];
    const ecodesign = content['shir_ecodesign'];
    const nuances = content['shir_nuances'];
    const permaculture = content['shir_permaculture'];
    const permaculture2 = content['shir_permaculture2'];
    const selectArea = content['shir_select_area'];
    const clear = content['shir_clear'];
    const pechiNuans = content['shir_pechi_nuans'];
    const answers = content['shir_answers'];
    const last = content['shir_last'];

    const baseFiles: TreeNode[] = [
      {
        label: intro.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: intro, icon: this.videoClass, data: `${basePath}/01_vvedenie_istoria_razvitia_energoinformatika_` }]
      },
      {
        label: pechi.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: pechi, icon: this.videoClass, data: `${basePath}/02_1_Konstruktive_pechi_zolotoe_sechenie` }]
      },
      {
        label: pechi2.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: pechi2, icon: this.videoClass, data: `${basePath}/02_2_Konstruktive_pechi_zolotoe_sechenie` }]
      },
      {
        label: ecodesign.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: ecodesign, icon: this.videoClass, data: `${basePath}/03_ecodesign_primery_proektov` }]
      },
      {
        label: nuances.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: nuances, icon: this.videoClass, data: `${basePath}/04_nuansy_stroitelstva` }]
      },
      {
        label: permaculture.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: permaculture, icon: this.videoClass, data: `${basePath}/05_permacultura` }]
      },
      {
        label: permaculture2.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: permaculture2, icon: this.videoClass, data: `${basePath}/06_permacultura_prodolzhenie` }]
      },
      {
        label: selectArea.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: selectArea, icon: this.videoClass, data: `${basePath}/07_vibor_uchastka` }]
      },
      {
        label: clear.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: clear, icon: this.videoClass, data: `${basePath}/08_1_chistka_` }]
      },
      {
        label: pechiNuans.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: pechiNuans, icon: this.videoClass, data: `${basePath}/09_pechi_njuanses` }]
      },
      {
        label: answers.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: answers, icon: this.videoClass, data: `${basePath}/10_Otvety_na_voprosy` }]
      },
      {
        label: last.split('.')[0],
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{ label: last, icon: this.videoClass, data: `${basePath}/11_` }]
      },
    ];

    return baseFiles;
  }

  getShirExperienceVideo(): TreeNode[] {
    const path = 'archive/ecobuild/shirokov/experience';
    const content = this.appService.appData.content;

    let practice = content['yura_ko_pract'];
    let howToBuild = content['vystup_kak_stroit'];
    let web = content['yura_ko_web'];

    const experienceFiles: TreeNode[] = [
      {
        label: `${practice}-1`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${practice}-1-1`, icon: this.videoClass, data: `${path}/yura_ko_pract1-0` },
          { label: `${practice}-1-2`, icon: this.videoClass, data: `${path}/yura_ko_pract1-1` },
          { label: `${practice}-1-3`, icon: this.videoClass, data: `${path}/yura_ko_pract1-2` },
          { label: `${practice}-1-4`, icon: this.videoClass, data: `${path}/yura_ko_pract1-3` },
          { label: `${practice}-1-5`, icon: this.videoClass, data: `${path}/yura_ko_pract1-4` },
        ]
      },

      {
        label: `${practice}-2`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${practice}-2-1`, icon: this.videoClass, data: `${path}/yura_ko_pract2-0` },
          { label: `${practice}-2-2`, icon: this.videoClass, data: `${path}/yura_ko_pract2-1` },
          { label: `${practice}-2-3`, icon: this.videoClass, data: `${path}/yura_ko_pract2-2` },
        ]
      },

      {
        label: `${practice}-3`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${practice}-3-1`, icon: this.videoClass, data: `${path}/yura_ko_pract3-0` },
          { label: `${practice}-3-2`, icon: this.videoClass, data: `${path}/yura_ko_pract3-1` },
          { label: `${practice}-3-3`, icon: this.videoClass, data: `${path}/yura_ko_pract3-2` },
          { label: `${practice}-3-4`, icon: this.videoClass, data: `${path}/yura_ko_pract3-3` },
          { label: `${practice}-3-5`, icon: this.videoClass, data: `${path}/yura_ko_pract3-4` },
        ]
      },

      {
        label: `${howToBuild}-3`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${howToBuild}-1`, icon: this.videoClass, data: `${path}/vystup_kak_stroit-0` },
          { label: `${howToBuild}-2`, icon: this.videoClass, data: `${path}/vystup_kak_stroit-1` },
          { label: `${howToBuild}-3`, icon: this.videoClass, data: `${path}/vystup_kak_stroit-2` },
          { label: `${howToBuild}-4`, icon: this.videoClass, data: `${path}/vystup_kak_stroit-3` },
          { label: `${howToBuild}-5`, icon: this.videoClass, data: `${path}/vystup_kak_stroit-4` },
          { label: `${howToBuild}-6`, icon: this.videoClass, data: `${path}/vystup_kak_stroit-5` },
          { label: `${howToBuild}-7`, icon: this.videoClass, data: `${path}/vystup_kak_stroit-6` },
          { label: `${howToBuild}-8`, icon: this.videoClass, data: `${path}/vystup_kak_stroit-7` },
        ]
      },

      {
        label: `${web}-3`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${web}-1`, icon: this.videoClass, data: `${path}/yura_ko_web-0` },
          { label: `${web}-2`, icon: this.videoClass, data: `${path}/yura_ko_web-1` },
          { label: `${web}-3`, icon: this.videoClass, data: `${path}/yura_ko_web-2` },
          { label: `${web}-4`, icon: this.videoClass, data: `${path}/yura_ko_web-3` },
          { label: `${web}-5`, icon: this.videoClass, data: `${path}/yura_ko_web-4` },
          { label: `${web}-6`, icon: this.videoClass, data: `${path}/yura_ko_web-5` },
          { label: `${web}-7`, icon: this.videoClass, data: `${path}/yura_ko_web-6` },
          { label: `${web}-8`, icon: this.videoClass, data: `${path}/yura_ko_web-7` },
        ]
      }

    ];

    return experienceFiles;
  }

  getShirAddVideo(): TreeNode[] {
    const path = 'archive/ecobuild/shirokov/dom_za_5';
    const content = this.appService.appData.content;

    let omsk = content['omsk'];
    let tatarstan = content['tatarstan'];

    const addFiles: TreeNode[] = [
      {
        label: omsk,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${omsk}-1`, icon: this.videoClass, data: `${path}/omsk-0` },
          { label: `${omsk}-2`, icon: this.videoClass, data: `${path}/omsk-1` },
          { label: `${omsk}-3`, icon: this.videoClass, data: `${path}/omsk-2` },
          { label: `${omsk}-4`, icon: this.videoClass, data: `${path}/omsk-3` },
          { label: `${omsk}-5`, icon: this.videoClass, data: `${path}/omsk-4` },
          { label: `${omsk}-6`, icon: this.videoClass, data: `${path}/omsk-5` },
        ]
      },

      {
        label: `${tatarstan}-1`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${tatarstan}-1-1`, icon: this.videoClass, data: `${path}/tatarstan-1-0` },
          { label: `${tatarstan}-1-2`, icon: this.videoClass, data: `${path}/tatarstan-1-1` },
          { label: `${tatarstan}-1-3`, icon: this.videoClass, data: `${path}/tatarstan-1-2` },
          { label: `${tatarstan}-1-4`, icon: this.videoClass, data: `${path}/tatarstan-1-3` },
        ]
      },

      {
        label: `${tatarstan}-2`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${tatarstan}-2-1`, icon: this.videoClass, data: `${path}/tatarstan-2-0` },
          { label: `${tatarstan}-2-2`, icon: this.videoClass, data: `${path}/tatarstan-2-1` },
          { label: `${tatarstan}-2-3`, icon: this.videoClass, data: `${path}/tatarstan-2-2` },
        ]
      },

      {
        label: `${tatarstan}-3`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${tatarstan}-3-1`, icon: this.videoClass, data: `${path}/tatarstan-3-0` },
          { label: `${tatarstan}-3-2`, icon: this.videoClass, data: `${path}/tatarstan-3-1` },
          { label: `${tatarstan}-3-3`, icon: this.videoClass, data: `${path}/tatarstan-3-2` },
          { label: `${tatarstan}-3-4`, icon: this.videoClass, data: `${path}/tatarstan-3-3` },
          { label: `${tatarstan}-3-5`, icon: this.videoClass, data: `${path}/tatarstan-3-4` },
        ]
      },

      {
        label: `${tatarstan}-4`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${tatarstan}-4-1`, icon: this.videoClass, data: `${path}/tatarstan-4-0` },
          { label: `${tatarstan}-4-2`, icon: this.videoClass, data: `${path}/tatarstan-4-1` },
        ]
      },

      {
        label: `${tatarstan}-5`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${tatarstan}-5-1`, icon: this.videoClass, data: `${path}/tatarstan-5-0` },
          { label: `${tatarstan}-5-2`, icon: this.videoClass, data: `${path}/tatarstan-5-1` },
          { label: `${tatarstan}-5-3`, icon: this.videoClass, data: `${path}/tatarstan-5-2` },
          { label: `${tatarstan}-5-4`, icon: this.videoClass, data: `${path}/tatarstan-5-3` },
        ]
      },

      {
        label: `${tatarstan}-6`,
        // data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: `${tatarstan}-6-1`, icon: this.videoClass, data: `${path}/tatarstan-6-0` },
          { label: `${tatarstan}-6-2`, icon: this.videoClass, data: `${path}/tatarstan-6-1` },
        ]
      },
    ];

    return addFiles;
  }



  getKovBaseVideo(): TreeNode[] {
    const baseVideoPath: string = 'archive/ecobuild/kovalenko/base';

    const baseFiles = [
      {
        label: "1 ФУНДАМЕНТ",
        data: "Background",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{
          label: "Типы фундаментов",
          data: "BackgroundTypes",
          expandedIcon: this.videoExpandedClass,
          collapsedIcon: this.videoCollapsedClass,
          children: [
            { label: "Ленточный", icon: this.videoClass, data: `${baseVideoPath}/Lenta` },
            { label: "Плитный", icon: this.videoClass, data: `${baseVideoPath}/Plita` },
            { label: "Свайный", icon: this.videoClass, data: `${baseVideoPath}/Svai` },
            { label: "Солэкофундамент", icon: this.videoClass, data: `${baseVideoPath}/Soleco` }]
        },

        { label: "Разметка, опалубка, вязка арматуры", data: `${baseVideoPath}/Razmetka`, icon: this.videoClass },
        { label: "Закладка коммуникаций", data: `${baseVideoPath}/ZakladCommunicazii`, icon: this.videoClass },
        { label: "Вибрирование бетона", data: `${baseVideoPath}/VibrirovanieBetona`, icon: this.videoClass }
        ]
      },

      {
        label: "2 СТЕНЫ",
        data: "Walls",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: "Мауэрлат", data: `${baseVideoPath}/Mauerlat`, icon: this.videoClass },
          { label: "Стеновые панели", data: `${baseVideoPath}/StenoviePaneli`, icon: this.videoClass },
          { label: "Межэтажное перекрытие", data: `${baseVideoPath}/MezhetazhnoePerekritie`, icon: this.videoClass },

          {
            label: "Отделка фасада",
            data: "Otdelka",
            expandedIcon: this.videoExpandedClass,
            collapsedIcon: this.videoCollapsedClass,
            children: [
              {
                label: "Штукатурка",
                data: "Shtukaturka",
                expandedIcon: this.videoExpandedClass,
                collapsedIcon: this.videoCollapsedClass,
                children: [
                  { label: "Глиняная штукатурка", icon: this.videoClass, data: `${baseVideoPath}/Glina` },
                  { label: "Декоративнее покрытие по штукатурке", icon: this.videoClass, data: `${baseVideoPath}/Dekor` },
                  { label: "Известковая штукатурка", icon: this.videoClass, data: `${baseVideoPath}/Isvest` },
                  {
                    label: "Мастер класс",
                    data: "MasterClass",
                    expandedIcon: this.videoExpandedClass,
                    collapsedIcon: this.videoCollapsedClass,
                    children: [
                      { label: "Саманный слой", icon: this.videoClass, data: `${baseVideoPath}/Saman` },
                      { label: "То, чего не стоить бояться", icon: this.videoClass, data: `${baseVideoPath}/NoWorry` },
                      { label: "Финишный слой", icon: this.videoClass, data: `${baseVideoPath}/FinishSloy` }
                    ]
                  }
                ]
              },

              { label: "Дерево", icon: this.videoClass, data: `${baseVideoPath}/Derevo` },
              { label: "Кирпич", icon: this.videoClass, data: `${baseVideoPath}/Kirpich` },
              { label: "ЦСП", icon: this.videoClass, data: `${baseVideoPath}/CSP` }]
          },


        ]
      },

      {
        label: "3 ОКНА",
        data: "Windows",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [{
          label: "Виды окон",
          data: "WindowsTypes",
          expandedIcon: this.videoExpandedClass,
          collapsedIcon: this.videoCollapsedClass,
          children: [
            { label: "Алюминиевые окна", icon: this.videoClass, data: `${baseVideoPath}/Alumminium` },
            { label: "Деревяные окна", icon: this.videoClass, data: `${baseVideoPath}/DerevianieOkna` },
            { label: "Металлопластиковые окна ", icon: this.videoClass, data: `${baseVideoPath}/MetaloPlastik` }
          ]
        },

        { label: "Какие окна выбрать", data: `${baseVideoPath}/KakieOknaVybrat`, icon: this.videoClass },
        { label: "Подоконники", data: `${baseVideoPath}/Podokonniki`, icon: this.videoClass },
        ]
      },

      {
        label: "4 КРОВЛЯ",
        data: "Roof",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: "Типы кровель", icon: this.videoClass, data: `${baseVideoPath}/TipyCrovel` },
          { label: "Устройство кровли", icon: this.videoClass, data: `${baseVideoPath}/UstroistvoKrovli` },
          {
            label: "Виды кровельных покрытий",
            data: "RoofForms",
            expandedIcon: this.videoExpandedClass,
            collapsedIcon: this.videoCollapsedClass,
            children: [
              { label: "Металочерепица", icon: this.videoClass, data: `${baseVideoPath}/Metalocherepitsa` },
              { label: "Битумная черепица", icon: this.videoClass, data: `${baseVideoPath}/Bitum` },
              { label: "Ондулин", icon: this.videoClass, data: `${baseVideoPath}/Ondulin` },
              { label: "Тростниковая кровля и дранка", icon: this.videoClass, data: `${baseVideoPath}/TrosnikIDranka` },
              { label: "Черепица", icon: this.videoClass, data: `${baseVideoPath}/Cherepiza` },
              { label: "Шифер и шиферная черепица", icon: this.videoClass, data: `${baseVideoPath}/Shifer` }
            ]
          }
        ]
      },

      {
        label: "5 КОММУНИКАЦИИ",
        data: "Communications",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: "Теплые стены", icon: this.videoClass, data: `${baseVideoPath}/TeplieSteny` },
          { label: "Электрика", icon: this.videoClass, data: `${baseVideoPath}/Electrica` },
          { label: "Отопление", icon: this.videoClass, data: `${baseVideoPath}/Otoplenie` },
          { label: "Водопровод и канализация", icon: this.videoClass, data: `${baseVideoPath}/VodoprovodIKanal` }
        ]
      },

      {
        label: "6 ОТДЕЛОЧНЫЕ РАБОТЫ",
        data: "OtdelochnieRaboty",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: "Глиняная и известковая штукатурка", icon: this.videoClass, data: `${baseVideoPath}/GlinaIIzvest` },
          { label: "Облицовка стен деревом", icon: this.videoClass, data: `${baseVideoPath}/Oblizovka` },
          { label: "Подшивка потолка", icon: this.videoClass, data: `${baseVideoPath}/PodshivkaPotolka` },
          { label: "Покрытие пола", icon: this.videoClass, data: `${baseVideoPath}/PokrytiePola` },
          {
            label: "Cаманные перегородки",
            data: "SamanniePeregorodki",
            expandedIcon: this.videoExpandedClass,
            collapsedIcon: this.videoCollapsedClass,
            children: [
              { label: "саманные перегородки 1", icon: this.videoClass, data: `${baseVideoPath}/SamanPeregorodki1` },
              { label: "саманные перегородки 2", icon: this.videoClass, data: `${baseVideoPath}/SamanPeregorodki2` },
              { label: "саманные перегородки 3", icon: this.videoClass, data: `${baseVideoPath}/SamanPeregorodki3` }
            ]
          }
        ]
      },

      {
        label: "7 ОРГАНИЗАЦИЯ СТРОИТЕЛЬСТВА",
        data: "Organization",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: "Поиск строительной бригады", data: `${baseVideoPath}/PoiskBrigady`, icon: this.videoClass },
          { label: "Составление договора", data: `${baseVideoPath}/SostavlenieDogovora`, icon: this.videoClass },
          { label: "Поиск и закупка строительных материалов", data: `${baseVideoPath}/PoiskIZakupkaStroiMater`, icon: this.videoClass },
          { label: "Строительные инструменты", data: `${baseVideoPath}/StroiInstrum`, icon: this.videoClass },
        ]
      }
    ];

    return baseFiles;
  }

  getKovAddVideo(): TreeNode[] {
    const additionalVideoPath: string = 'archive/ecobuild/kovalenko/additional';

    const additionalFiles = [
      {
        label: "Дополнение",
        data: "Additional",
        expandedIcon: this.videoExpandedClass,
        collapsedIcon: this.videoCollapsedClass,
        children: [
          { label: "20+ рекомендаций как сэкономить на строительстве собственного дома", data: `${additionalVideoPath}/TwentyRecomendations`, icon: this.videoClass },
          { label: "Гид по инструментам, какой строительный инструмент понадобится чтобы построить экодом", data: `${additionalVideoPath}/GidInstruments`, icon: this.videoClass },
          { label: "Из чего постоить дом, обзор 6 самых популярных технологий строительства", data: `${additionalVideoPath}/SixTehnologiiStroitelstva`, icon: this.videoClass },
          { label: "Как спроектировать свой экодом за 3 часа, даже если Вы не знаете с чего начать", data: `${additionalVideoPath}/SproecttirovatDomZa3`, icon: this.videoClass },
          { label: "Отопление, как выбрать идеальную систему отопления для вашего дома", data: `${additionalVideoPath}/Otoplenie`, icon: this.videoClass },
          { label: "Теплый дом, как утеплить свой дом и снизить траты на отопление в несколько раз", data: `${additionalVideoPath}/KakUteplit`, icon: this.videoClass },
        ]
      }

    ];

    return additionalFiles;
  }
}

import { Injectable } from '@angular/core';
import { AppService } from '@app/app.service';
import { DiagramElementType, DiagramElement, dictionary } from './models/diagram';

@Injectable()
export class TranslateTextService {

  constructor(
    private appService: AppService
  ) { }

  getDiagramElement(type: DiagramElementType, value: number): DiagramElement {
    const content = this.appService.appData.content;
    const translated = content[dictionary[type]];
    const element = new DiagramElement(translated, value);

    return element;
  }

}

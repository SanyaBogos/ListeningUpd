import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input } from '@angular/core';
import { TextClient } from '../../../apiDefinitions';
import { Paragraph } from '../../performance-text/models/locators';
import { BuildWordService } from '../../shared/services/build-word.service';
import { MergeTextObject } from '../../performance-text/models/mergeTextObject';

@Component({
  selector: 'appc-preview-text',
  templateUrl: './preview-text.component.html',
  styleUrls: ['./preview-text.component.scss',
    '../../shared/styles/common.scss',
    '../../shared/styles/common-btns.scss',
    '../../performance-text/abstract-text/abstract-text.component.scss',
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PreviewTextComponent implements OnInit {

  @Input() text: string;

  public isPreviewVisible = false;
  public showSymbols = false;
  public paragraphs: Paragraph[] = [];

  constructor(
    private ref: ChangeDetectorRef,
    private textClient: TextClient,
    private buildWordService: BuildWordService
  ) { }

  ngOnInit() { }

  changePreviewVisibility() {
    this.isPreviewVisible = !this.isPreviewVisible;
    this.ref.markForCheck();
  }

  buildPreview() {
    const self = this;

    this.textClient.getTextPrview(!this.showSymbols, this.text)
      .subscribe(textGuessingDto => {
        if (self.showSymbols) {
          const mergeTextObject = new MergeTextObject(
            textGuessingDto.mergedText, textGuessingDto.resultsEncodedString
          );

          self.paragraphs = self.buildWordService.getMergedWords(mergeTextObject);
        } else {
          self.paragraphs = self.buildWordService.getHiddenWords(textGuessingDto);
        }

        this.isPreviewVisible = true;
        self.ref.markForCheck();
      });
  }
}

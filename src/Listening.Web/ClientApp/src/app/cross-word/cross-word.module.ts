import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CrosswordListComponent } from './crossword-list/crossword-list.component';
import { CrosswordComponent } from './crossword/crossword.component';
import { routing } from './cross-word.routes';

@NgModule({
  declarations: [CrosswordListComponent, CrosswordComponent],
  imports: [
    routing,
    CommonModule
  ]
})
export class CrossWordModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorizeDirective } from './authorize.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    AuthorizeDirective
  ],
  exports: [
    AuthorizeDirective
  ]
})
export class AuthorizeDirectivesModule { }

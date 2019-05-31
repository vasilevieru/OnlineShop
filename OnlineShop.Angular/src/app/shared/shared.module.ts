import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { MatSnackBarModule, MatDialogModule, MatButtonModule, MatProgressSpinnerModule } from '@angular/material';
import { MatLoadingComponent } from './components/mat-loading/mat-loading.component';
import { ImageComponent } from './components/image/image.component';

@NgModule({
  imports: [
    CommonModule,
    MatSnackBarModule,
    MatDialogModule,
    MatButtonModule,
    MatProgressSpinnerModule,
  ],
  declarations: [
    ConfirmationDialogComponent,
    MatLoadingComponent,
    ImageComponent
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ],
  exports: [
    MatLoadingComponent,
    ImageComponent
  ]
})
export class SharedModule { }

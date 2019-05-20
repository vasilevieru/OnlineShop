import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { MatSnackBarModule, MatDialogModule, MatButtonModule, MatProgressSpinnerModule } from '@angular/material';
import { MatLoadingComponent } from './components/mat-loading/mat-loading.component';

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
    MatLoadingComponent
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ],
  exports: [
    MatLoadingComponent
  ]
})
export class SharedModule { }

import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarVerticalPosition, MatSnackBarHorizontalPosition, MatSnackBarConfig } from '@angular/material';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SimpleSnackBarService {

  constructor(private snackBar: MatSnackBar) { }

  open(message: string, duration?: number, panelClass?: string | string[],
    positionVertical?: MatSnackBarVerticalPosition, positionHorizontal?: MatSnackBarHorizontalPosition) {
      const config = new MatSnackBarConfig();

      config.duration = duration || 4000;
      config.panelClass = panelClass || ['snackBar-default'];
      config.verticalPosition = positionVertical || 'top';
      config.horizontalPosition = positionHorizontal || 'center';

      this.snackBar.open(message, null, config);
  }

  openSuccess(message: string, duration?: number,
    positionVertical?: MatSnackBarVerticalPosition, positionHorizontal?: MatSnackBarHorizontalPosition) {
      this.open(message, duration, ['snackBar-success'], positionVertical, positionHorizontal);
  }

  openWarning(message: string, duration?: number,
    positionVertical?: MatSnackBarVerticalPosition, positionHorizontal?: MatSnackBarHorizontalPosition) {
      this.open(message, duration, ['snackBar-warning'], positionVertical, positionHorizontal);
  }

  openError(message: string, duration?: number,
    positionVertical?: MatSnackBarVerticalPosition, positionHorizontal?: MatSnackBarHorizontalPosition) {
      this.open(message, duration, ['snackBar-error'], positionVertical, positionHorizontal);
  }

  openErrorWithResponseMessage(message: string, error: HttpErrorResponse, duration?: number,
    positionVertical?: MatSnackBarVerticalPosition, positionHorizontal?: MatSnackBarHorizontalPosition) {
      if (error.error && error.error instanceof Object) {
        const values = <Array<string>> Object.values(error.error);
        message = values[0];
        for (let i = 1; i < values.length; i++) {
          message = `${message};\n${values[i]}`;
        }
      }
      this.open(message, duration, ['snackBar-error'], positionVertical, positionHorizontal);
  }
}

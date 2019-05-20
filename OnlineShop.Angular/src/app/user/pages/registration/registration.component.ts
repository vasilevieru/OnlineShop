import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { MatDialogRef, MatDialog } from '@angular/material';
import { SimpleSnackBarService, ComponentCanDeactivate, ConfirmationDialogComponent } from '@shared';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FormHelpers } from 'app/helpers/form-helpers';
import { UserService } from 'app/user/services/user.service';
import { User } from '@user';
import { addToViewTree } from '@angular/core/src/render3/instructions';
import { UserDetails } from 'app/user/models/user-details.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent extends ComponentCanDeactivate implements OnInit {

  form: FormGroup;

  isSubmitted = false;

  confirmationDialog: MatDialogRef<ConfirmationDialogComponent>;

  constructor(
    private userService: UserService,
    private simpleSnackbarService: SimpleSnackBarService,
    private router: Router,
    private dialog: MatDialog
  ) {
    super();
  }

  ngOnInit() {
    this.initForm();
  }

  initForm(): void {
    this.form = new FormGroup({
      firstname: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      lastname: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      phone: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.maxLength(60), Validators.email]),
      password: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required)
    });
  }

  onSubmit(): void {
    this.isSubmitted = true;

    const data = this.form.getRawValue();
    const user: UserDetails = {
      firstName: data.firstname,
      lastName: data.lastname,
      phone: data.phone,
      email: data.email,
      password: data.password
    };
    if (data.password !== data.confirmPassword) {
      this.form.get('confirmPassword').setErrors({ 'incorrect': true });
    } else if (!this.form.valid) {
      FormHelpers.markFormGroupTouched(this.form);
    } else {
      this.userService.registerUser(user).subscribe(() => {
        this.simpleSnackbarService.openSuccess('Successful registration');
        this.router.navigate(['/login']);
      }, (error: HttpErrorResponse) => {
        this.simpleSnackbarService.openErrorWithResponseMessage('Registration failed', error);
      });
    }
  }

  get firstname(): AbstractControl {
    return this.form.get('firstname');
  }

  get lastname
  (): AbstractControl {
    return this.form.get('lastname');
  }

  get phone(): AbstractControl {
    return this.form.get('phone');
  }

  get email(): AbstractControl {
    return this.form.get('email');
  }

  get password(): AbstractControl {
    return this.form.get('password');
  }

  get confirmPassword(): AbstractControl {
    return this.form.get('confirmPassword');
  }

  canDeactivate(): Observable<boolean> | boolean {
    if (!this.isSubmitted && this.form.dirty) {
      this.confirmationDialog = this.dialog.open(ConfirmationDialogComponent);
      return this.confirmationDialog.afterClosed()
        .pipe(
          map(res => {
            return res;
          })
        );
    }
    return true;
  }

}

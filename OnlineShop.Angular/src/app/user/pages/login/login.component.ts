import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { SimpleSnackBarService, AuthService } from '@shared';
import { FormHelpers } from 'app/helpers/form-helpers';
import { HttpErrorResponse } from '@angular/common/http';
import { UserLogin } from 'app/user/models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  hide = true;
  form: FormGroup;
  constructor(private router: Router,
    private authenticationService: AuthService,
    private simpleSnackbarService: SimpleSnackBarService
  ) { }

  ngOnInit() {
    this.initForm();
  }


  initForm(): void {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit(): void {
    const user:  UserLogin = this.form.getRawValue();
    if (!this.form.valid) {
      FormHelpers.markFormGroupTouched(this.form);
    } else {
      this.authenticationService.login(user)
        .subscribe(res => {
          this.simpleSnackbarService.openSuccess('Login successfully');
          this.router.navigate(['/catalog']);
        }, (error: HttpErrorResponse) => {
          this.simpleSnackbarService.openErrorWithResponseMessage('Login failed', error);
        });
    }
  }

  goToRegistrationPage() {
    this.router.navigate(['/registration']);
  }

  get email(): AbstractControl {
    return this.form.get('email');
  }

  get password(): AbstractControl {
    return this.form.get('password');
  }

}

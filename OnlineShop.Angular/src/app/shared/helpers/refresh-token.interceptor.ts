import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { RefreshToken } from 'app/user/models/refresh-token.model';

@Injectable()
export class RefreshTokenInterceptor implements HttpInterceptor {

    refreshToken: RefreshToken;

    constructor(
        private router: Router,
        public authService: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                const currentUser = this.authService.getDecodedAccessToken(this.authService.currentUserValue.accessToken);
                this.refreshToken = {
                    userId: currentUser.Id,
                    refreshToken: this.authService.currentUserValue.refreshToken
                };

                this.authService.refreshAccessToken(this.refreshToken)
                    .subscribe(res => {
                        if (!res) {
                            this.authService.refreshCache();
                            this.router.navigate(['/login']);
                        } else {
                            this.authService.saveUserInLocalStorage(res);
                            return request.clone({
                                setHeaders: {
                                    Authorization: `Bearer ${res.accessToken}`
                                }
                            });
                        }
                    });
            }

            const error = err.message || err.statusText;
            return throwError(error);
        }));

    }
}

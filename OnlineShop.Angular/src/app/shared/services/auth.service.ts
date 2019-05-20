import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { permissionsTable } from '../permissions';
import { User, UserLogin } from '@user';
import * as jwt_decode from 'jwt-decode';
import { HttpClient } from '@angular/common/http';
import { RefreshToken } from 'app/user/models/refresh-token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  role: string;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  get userIsAuthenticated(): boolean {
    return localStorage.getItem('currentUser') !== null;
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  saveUserInLocalStorage(res) {
    const currentUser: User = res.body;
    currentUser.isAuthenticated = true;
    localStorage.setItem('currentUser', JSON.stringify(currentUser));
    this.currentUserSubject.next(currentUser);
  }

  login(user: UserLogin): Observable<any> {
    return this.http.post('api/users/login', user, { observe: 'response' as 'body' })
      .pipe(tap(res => {
        this.saveUserInLocalStorage(res);
      }));
  }

  userHasPermission(functionality: string): boolean {
    const permission = permissionsTable.find((p) => p.description === functionality);
    if (permission) {
      this.currentUser.subscribe(user => {
        if (user) {
          const decodedToken = this.getDecodedAccessToken(user.accessToken);
          this.role = decodedToken.Roles;
        }
      });
      return this.role && permission.roles.includes(this.role);
    } else {
      console.error(`Permission ${functionality} not found`);
      return false;
    }
  }

  refreshAccessToken(refreshToken: RefreshToken): Observable<any> {
    return this.http.post('api/users/refreshtoken', refreshToken);
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch (Error) {
      return null;
    }
  }

  refreshCache(): void {
    if (this.currentUserSubject.value) {
      this.currentUserSubject.value.accessToken = null;
    }
    localStorage.removeItem('currentUser');
  }
}

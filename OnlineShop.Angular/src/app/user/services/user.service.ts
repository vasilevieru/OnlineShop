import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { UserDetails } from '../models/user-details.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  registerUser(user: UserDetails): Observable<UserDetails> {
    return this.http.post<UserDetails>('api/users/registration', user);
  }

  uploadAvatar(id: number, avatar: File) {
    const formData = new FormData();
    formData.append('avatar', avatar, avatar.name);
    return this.http.patch<any>(`api/users/${id}/image`, formData);
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`api/users/${id}`);
  }

  // updateUser(user: UpdateUser): Observable<void> {
  //   return this.http.put<void>(`api/users/${user.id}`, { user: user, old_password: user.oldPassword });
  // }

  getAvatar(id: number): Observable<any> {
    const requestOptions: Object = {
      responseType: 'text'
    };

    return this.http.get<any>(`api/users/${id}/avatar`, requestOptions);
  }
}

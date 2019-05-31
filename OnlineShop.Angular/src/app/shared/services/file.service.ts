import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FileDetails } from '../models/file-details.model';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private http: HttpClient) { }

  public uploadPhoto(productId: number, formData: FormData): Observable<FileDetails[]> {
    return this.http.post<FileDetails[]>(`api/files/images/${productId}`, formData);
  }

  public getImageBlob(id: number): Observable<Blob> {
    return this.http.get(`api/files/images/${id}/blob`, {
      responseType: 'blob'
    });
  }

  public getAllProductImages(productId: number): Observable<FileDetails[]> {
    return this.http.get<FileDetails[]>(`api/files/images/product/${productId}`);
  }

  public getImageThumbnailBlob(id: number): Observable<Blob> {
    return this.http.get(`api/files/images/${id}/thumbnail/blob`, {
      responseType: 'blob'
    });
  }

  public getImageById(id: number): Observable<FileDetails> {
    return this.http.get<FileDetails>(`api/files/images/${id}`);
  }

  public deleteImageById(id: number): Observable<any> {
    return this.http.delete(`api/files/images/${id}`);
  }

  public updateImage(id: number, formData: FormData): Observable<FileDetails> {
    return this.http.put<FileDetails>(`api/files/images/${id}`, formData);
  }
}

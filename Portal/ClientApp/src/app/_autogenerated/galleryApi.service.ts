import { IFileBindingModel } from './fileBindingModel';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GalleryApiService {

  constructor(@Inject('BASE_URL') public baseUrl: string) {}

  public Url_UploadFile = () => this.baseUrl + `api/gallery/file`;
  public Url_GetUploadedFiles = () => this.baseUrl + `api/gallery/file`;
  public Url_GetFile = (fileName: string) => this.baseUrl + `api/gallery/file/${encodeURIComponent(fileName)}`;
  public Url_DeleteFile = (fileName: string) => this.baseUrl + `api/gallery/file/${encodeURIComponent(fileName)}`;
  
}

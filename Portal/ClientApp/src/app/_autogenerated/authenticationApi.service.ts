import { ILoginBindingModel } from './loginBindingModel';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationApiService {

  constructor(@Inject('BASE_URL') public baseUrl: string) {}

  public Url_LoginAsync = () => this.baseUrl + `api/AuthenticationApi/login`;
  public Url_RegisterAsync = () => this.baseUrl + `api/AuthenticationApi/register`;
  public Url_LogOutAsync = () => this.baseUrl + `api/AuthenticationApi/signout`;
  
}

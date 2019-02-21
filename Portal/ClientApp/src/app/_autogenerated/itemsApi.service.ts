import { IItemBindingModel } from './itemBindingModel';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ItemsApiService {

  constructor(@Inject('BASE_URL') public baseUrl: string) {}

  public Url_GetItemsOverview = () => this.baseUrl + `api/items/overview`;
  public Url_GetItems = () => this.baseUrl + `api/items/store`;
  public Url_DeleteItem = (id: number) => this.baseUrl + `api/items/${id}`;
  public Url_GetItem = (id: number) => this.baseUrl + `api/items/${id}`;
  public Url_GetCategoriesForLookup = () => this.baseUrl + `api/items/categories/lookup`;
  public Url_GetPartsForLookup = () => this.baseUrl + `api/items/parts/lookup`;
  public Url_UpdateItem = (id: number) => this.baseUrl + `api/items/${id}`;
  public Url_CreateItem = () => this.baseUrl + `api/items`;
  public Url_IsEmailUnique = (email: string, id: number) => this.baseUrl + `api/items/validation/email?email=${encodeURIComponent(email)}&id=${id}`;
  public Url_GetBadRequest = () => this.baseUrl + `api/items/error`;
  public Url_GetDataForAuthorizedUsers = () => this.baseUrl + `api/items/authorized`;
  public Url_GetDataForAdmins = () => this.baseUrl + `api/items/authorized/admin`;
  public Url_GetDataForSupport = () => this.baseUrl + `api/items/authorized/support`;
  public Url_TryGettingUnattainableData = () => this.baseUrl + `api/items/authorized/god`;
  
}

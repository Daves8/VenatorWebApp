import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { getBaseUrl } from '../../main';
import { ItemCategory } from '../model/enum/item-category';
import { Role } from '../model/enum/role';
import { Item } from '../model/item';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient, private storageService: StorageService) { }

  getAllItems(): Observable<any> {
    const role = this.storageService.getUserRole();
    if (role != null && role > Role.user) {
      return this.http.get(getBaseUrl() + 'item/all-items');
    } else {
      return this.http.get(getBaseUrl() + 'item/not-hidden-items');
    }
  }

  getItemsInCart(): Observable<any> {
    return this.http.get(getBaseUrl() + 'item/items-in-cart');
  }

  getItemImage(item: Item) {
    const url = getBaseUrl() + "img/item/" + item.category + "" + item.imageUrl;
    return this.http.get(url, { responseType: 'arraybuffer' });
  }

  addToCart(item: Item): Observable<any> {
    return this.http.post(getBaseUrl() + 'item/add-to-cart', item);
  }

  removeItemFromCart(item: Item): Observable<any> {
    return this.http.get(getBaseUrl() + 'item/remove-item-from-cart/' + item.id);
  }

  removeAllItemsFromCart(): Observable<any> {
    return this.http.get(getBaseUrl() + 'item/remove-all-items-from-cart');
  }

  buyItemsInCart(): Observable<any> {
    return this.http.get(getBaseUrl() + 'item/buy-items-in-cart');
  }

  getCurrentUser(): Observable<any> {
    return this.http.get(getBaseUrl() + 'user/' + this.storageService.getUserId());
  }

  getItemCategoryLabel(category: ItemCategory): string {
    switch (category) {
      case ItemCategory.buildings: return 'Будівля';
      case ItemCategory.animals: return 'Тварини';
      case ItemCategory.clothers: return 'Одяг';
      case ItemCategory.weapons: return 'Зброя';
    }
  }
}

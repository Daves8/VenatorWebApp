import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ItemCategory } from '../model/enum/item-category';
import { Item } from '../model/item';
import { ItemService } from '../service/item.service';
import { StorageService } from '../service/storage.service';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.css']
})
export class StoreComponent implements OnInit {

  allItems: Item[] = [];
  groupedItems: { [category: number]: Item[] } = {};
  isLogged: boolean | null = null;
  // Filter
  minPrice: number = 0;
  maxPrice: number = 999999;
  isWeapon: boolean = true;
  isClothes: boolean = true;
  isAnimals: boolean = true;
  isBuildings: boolean = true;

  constructor(private itemService: ItemService, private storageService: StorageService, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.isLogged = this.storageService.isAuthorized();
    this.getAllItems();
  }

  getAllItems(): void {
    this.itemService.getAllItems().subscribe(response => {
      this.allItems = response;

      // max price
      this.maxPrice = this.allItems.reduce((max, item) => {
        return item.price > max ? item.price : max;
      }, 0);
      // min price
      this.minPrice = this.allItems.reduce((min, item) => {
        return item.price < min ? item.price : min;
      }, Number.MAX_SAFE_INTEGER);

      this.applyImages();
      this.groupItems();
    });
  }

  groupItems() {
    this.groupedItems = {};
    for (let item of this.allItems) {
      if (!this.groupedItems[item.category]) {
        this.groupedItems[item.category] = [];
      }
      this.groupedItems[item.category].push(item);
    }
  }

  applyImages(): void {
    this.allItems.forEach(item => {
      this.itemService.getItemImage(item).subscribe((imageData: ArrayBuffer) => {
        item.imageSafeUrl = this.saveImageLocally(imageData);
      });
    });
  }

  saveImageLocally(imageData: ArrayBuffer): SafeUrl {
    const blob = new Blob([imageData], { type: 'image/jpeg' });
    const url = URL.createObjectURL(blob);
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  getItemCategoryLabel(category: ItemCategory): string {
    return this.itemService.getItemCategoryLabel(category);
  }

  handleAddToCartClick(item: Item): void {
    this.itemService.addToCart(item).subscribe(() => {
      alert("\"" + item.name + "\" успішно додано у кошик!");
    }, error => alert(error));
  }

  applyFilter(): void {
    this.itemService.getAllItems().subscribe(response => {
      this.allItems = response;
      this.allItems = this.allItems.filter(item => item.price >= this.minPrice && item.price <= this.maxPrice);
      this.applyImages();
      this.groupItems();
    });
  }
}

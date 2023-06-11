import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ItemCategory } from '../model/enum/item-category';
import { Item } from '../model/item';
import { ItemService } from '../service/item.service';
import { StorageService } from '../service/storage.service';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  allItems: Item[] = [];
  priceAll: number = 0;
  goldInUser: number = 0;
  error: string = '';

  constructor(private itemService: ItemService, private storageService: StorageService, private router: Router, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    if (!this.storageService.isAuthorized()) {
      this.router.navigate(['/']);
    }

    this.itemService.getCurrentUser().subscribe((response) => {
      this.goldInUser = response.goldAmount;
    });

    this.getItemsInCart();
  }

  getItemsInCart(): void {
    this.itemService.getItemsInCart().subscribe(response => {
      this.allItems = response;
      this.applyPriceAll();
      this.applyImages();
    });
  }

  applyPriceAll(): void {
    this.priceAll = parseFloat(this.allItems.reduce((sum, item) => sum + item.price, 0).toFixed(2));
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

  handleDeleteFromCartClick(item: Item): void {
    this.itemService.removeItemFromCart(item).subscribe(() => {
      const index = this.allItems.findIndex(i => i.id === item.id);
      this.allItems.splice(index, 1);
      this.applyPriceAll();
      this.error = '';
    }, error => console.error(error));
  }

  buyItems(): void {
    const result = confirm('Ви підтверджуєте придбання ' + this.allItems.length + ' предметів на суму ' + this.priceAll + ' золота?');
    if (result) {
      this.itemService.buyItemsInCart().subscribe(() => {
        this.getItemsInCart();
      }, error => this.error = error.error);
    }
  }

  removeAllItemsFromCart(): void {
    this.itemService.removeAllItemsFromCart().subscribe(() => {
      this.getItemsInCart();
    }, error => console.error(error));
  }
}

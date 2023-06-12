import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ItemCategory } from '../model/enum/item-category';
import { Item } from '../model/item';
import { Statistics } from '../model/statistics';
import { User } from '../model/user';
import { ItemService } from '../service/item.service';
import { StorageService } from '../service/storage.service';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  user: User = new User();
  statistics!: Statistics;
  items: Item[] = [];

  constructor(private userService: UserService, private storageService: StorageService, private itemService: ItemService, private router: Router, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    if (!this.storageService.isAuthorized()) {
      this.router.navigate(['/']);
    }

    this.applyUser();
    this.applyStatistics();
    this.applyItems();
  }

  applyUser(): void {
    this.userService.getCurrentUser().subscribe((response) => {
      this.user = response;
      this.applyUserImage();
    });
  }

  applyStatistics(): void {
    this.userService.getUserStatistics(this.storageService.getUserId()).subscribe((response) => {
      this.statistics = response;
    });
  }

  applyItems(): void {
    this.userService.getItemsInInventory().subscribe((response) => {
      this.items = response;
      this.applyItemsImages();
    });
  }

  applyUserImage(): void {
    this.userService.getUserImage(this.user).subscribe((imageData: ArrayBuffer) => {
      this.user.imageSafeUrl = this.saveImageLocally(imageData);
    });
  }

  applyItemsImages(): void {
    this.items.forEach(item => {
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
}

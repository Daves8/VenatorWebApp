import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  localStorage = localStorage;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  counter(): void {
    this.router.navigate(['/counter']);
  }

  fetchData(): void {
    this.router.navigate(['/fetch-data']);
  }
}

import { Component, OnInit } from '@angular/core';
import { AuthService } from './authservice';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'myfirstQBO';

  constructor() {}


  ngOnInit(): void {
    
  }
  // authorize() {
  //   // this.authService.authorize();
  //   this.r.navigate(['/callback']);
  //   // this.route.navigate(['/login'])

  // }
}

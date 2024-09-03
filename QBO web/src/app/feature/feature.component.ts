import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../authservice';

@Component({
  selector: 'app-feature',
  templateUrl: './feature.component.html',
  styleUrls: ['./feature.component.scss']
})
export class FeatureComponent implements OnInit {
  title = 'myfirstQBO';
  constructor(private authService: AuthService,private r:Router) { }

  ngOnInit(): void {

  }
  authorize() {
    this.authService.authorize();
    this.r.navigate(['/callback']);
    // this.route.navigate(['/login'])

  }
}
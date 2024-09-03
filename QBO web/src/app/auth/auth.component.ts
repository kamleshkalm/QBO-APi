import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../authservice';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  authCode: string | null = null;

  constructor(private authService: AuthService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    debugger
    this.route.queryParams.subscribe(params => {
      this.authCode = params['code'] || null;
      if (this.authCode) {
        this.authService.handleCallback(this.authCode).subscribe(response => {
          console.log('Tokens:', response);
          debugger
        });
      }
    });
  }
}

import { NgModule } from '@angular/core';
import { AuthComponent } from './auth.component';
import { AuthRoutingModule } from './auth-routing.module';
import { AuthService } from '../authservice';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AuthComponent
  ],
  imports: [
    AuthRoutingModule,RouterModule,CommonModule
  ],
  providers: [],
  bootstrap: [AuthComponent]
})
export class AuthModule { }

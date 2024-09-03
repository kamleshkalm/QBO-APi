import { NgModule } from '@angular/core';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CookieService } from 'ngx-cookie-service';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    HomeRoutingModule,RouterModule,CommonModule,FormsModule
  ],
  providers: [CookieService],
  bootstrap: [HomeComponent]
})
export class HomeModule { }

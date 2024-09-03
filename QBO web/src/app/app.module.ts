import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FeatureRoutingModule } from './feature/feature-routing.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,HttpClientModule,RouterModule,CommonModule,FeatureRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

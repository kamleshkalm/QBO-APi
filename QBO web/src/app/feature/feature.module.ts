import { NgModule } from '@angular/core';
import { FeatureComponent } from './feature.component';
import { FeatureRoutingModule } from './feature-routing.module';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './layout/header/header.component';

@NgModule({
  declarations: [
    FeatureComponent,HeaderComponent
  ],
  imports: [
    FeatureRoutingModule,RouterModule,CommonModule
  ],
  providers: [],
  bootstrap: [FeatureComponent]
})
export class FeatureModule { }

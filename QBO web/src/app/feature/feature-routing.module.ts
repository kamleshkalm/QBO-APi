import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeatureComponent } from './feature.component';

const routes: Routes = [
  {path:'',component:FeatureComponent,
    children:[
      { path: 'callback', loadChildren: () => import('../auth/auth.module').then(m => m.AuthModule), data: { title: 'Auth' } },
      { path: 'home', loadChildren: () => import('../feature/home/home.module').then(m => m.HomeModule), data: { title: 'Home' } },

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FeatureRoutingModule { }

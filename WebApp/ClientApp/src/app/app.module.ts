import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CompanyComponent } from "./company/company.component";
import { DepartmentComponent } from './department/department.component';
import { FieldComponent } from './field/field.component';
import { BoreholeComponent } from './borehole/borehole.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CompanyComponent,
    DepartmentComponent,
    FieldComponent,
    BoreholeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'company', component: CompanyComponent },
      { path: 'department', component: DepartmentComponent },
      { path: 'field', component: FieldComponent },
      { path: 'borehole', component: BoreholeComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

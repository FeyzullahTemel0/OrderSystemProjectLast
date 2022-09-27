import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FooterComponent } from '../components/app/footer/footer.component';
import { NavbarComponent } from '../components/app/navbar/navbar.component';
import { SidebarComponent } from '../components/app/sidebar/sidebar.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { OrderComponent } from '../components/app/order/order.component';
import { ProductComponent } from '../components/app/product/product.component';
import { CustomerComponent } from '../components/app/customer/customer.component';
import { WareHouseComponent } from '../components/app/wareHouse/wareHouse.component';
import { ComponentLayoutRoutes } from './Component-layout.routing';
import { HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { AdminLayoutRoutes } from '../components/app/layouts/admin-layout/admin-layout.routing';
import { TranslationService } from '../services/translation.service';


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(ComponentLayoutRoutes),
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatRippleModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatTooltipModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatCheckboxModule,
        NgbModule,
        NgMultiSelectDropDownModule,
        SweetAlert2Module,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useClass: TranslationService,
                deps: [HttpClient]
            }
        })
  ],
  declarations: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent, //*
    OrderComponent, //*
    ProductComponent,//*
    CustomerComponent,//*
    WareHouseComponent
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    OrderComponent, //*
    ProductComponent,//*
    CustomerComponent,//*
    WareHouseComponent
  ]
})
export class ComponentsModule { }

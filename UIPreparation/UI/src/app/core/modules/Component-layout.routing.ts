import { Routes } from "@angular/router";
import { CustomerComponent } from "../components/app/customer/customer.component";
import { OrderComponent } from "../components/app/order/order.component";
import { ProductComponent } from "../components/app/product/product.component";
import { WareHouseComponent } from "../components/app/wareHouse/wareHouse.component";
import { LoginGuard } from "../guards/login-guard";




export const ComponentLayoutRoutes: Routes = [
    { path: 'customer',      component: CustomerComponent,canActivate:[LoginGuard] }, 
    { path: 'product',           component: ProductComponent, canActivate:[LoginGuard] },
    { path: 'order',          component: OrderComponent, canActivate:[LoginGuard] },
    { path: 'warehouse',          component: WareHouseComponent,canActivate:[LoginGuard] }
];

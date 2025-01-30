import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { AuthGuard } from './auth/auth.guard';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { AddProductComponent } from './products/add-product/add-product.component';


export const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'login', component: LoginComponent },
    { path: 'products', component: ProductListComponent, canActivate: [AuthGuard] },
    { path: 'add-product', component: AddProductComponent, canActivate: [AuthGuard] },
    { path: 'edit-product/:id', component: ProductEditComponent, canActivate: [AuthGuard] }
];

import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';

const ROUTES: Routes = [
    { path: 'products', component: ProductListComponent },
    { path: 'products/:id/edit', component: ProductEditComponent },
    { path: '', pathMatch: 'full', redirectTo: 'products' },
    { path: '**', pathMatch: 'full', redirectTo: 'products' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(ROUTES)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule {
}

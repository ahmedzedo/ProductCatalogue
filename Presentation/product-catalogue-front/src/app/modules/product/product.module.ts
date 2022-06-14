import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './component/product-list/product-list.component';
import { AddProductComponent } from './component/add-product/add-product.component';



@NgModule({
  declarations: [
    ProductListComponent,
    AddProductComponent
  ],
  exports:[

    ProductListComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ProductModule { }

import { ProductGetList } from './product-get-list.model';
import { ProductList } from './product-list.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProductListService {
  constructor(private http: HttpClient) {
    (this.getmodel.pageSize = 10), (this.getmodel.pagePerPages = 10);
  }
  getmodel: ProductGetList = new ProductGetList();
  formData: ProductList[] = [];
  readonly basUrl: string = 'http://localhost:13143/get-product-list';

  getProductList() {
 this.http.post(this.basUrl, this.getmodel).subscribe(
   res=> {


 },
 err=>{
console.log(err);
 }
 );
  }
}

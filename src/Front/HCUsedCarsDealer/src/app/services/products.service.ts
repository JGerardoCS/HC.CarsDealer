import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map } from 'rxjs/operators';

import { ProductModel } from '../models/products.model';
import { ResponseModel } from '../models/response.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  createProduct(product: ProductModel): Observable<ResponseModel<ProductModel>> {
    const url = `${environment.productsBaseUrl}/products`;

    return this.http.post<ResponseModel<ProductModel>>(url, product);
  }

  getProducts(): Observable<ProductModel[]> {
    const url = `${environment.productsBaseUrl}/products`;

    return this.http.get<ResponseModel<ProductModel[]>>(url)
            .pipe(
              map(response => response.data)
            );
  }

  getProduct(productId: number): Observable<ResponseModel<ProductModel>> {
    const url = `${environment.productsBaseUrl}/products/${productId}`;

    return this.http.get<ResponseModel<ProductModel>>(url);
  }

  updateProduct(product: ProductModel): Observable<any> {
    const url = `${environment.productsBaseUrl}/products`;

    return this.http.put(url, product);
  }

  deleteProduct(productId: number): Observable<ResponseModel<any>> {
    const url = `${environment.productsBaseUrl}/products/${productId}`;

    return this.http.delete<ResponseModel<any>>(url);
  }

  private handleError(err: any): Observable<never> {
    let message: string;
    if (err.error instanceof ErrorEvent) {
      message = `Local error: ${err.error.message}`;
    } else {
      message = `Remote error ${err.status}: ${err}`;
    }

    console.log('Error', message);

    return throwError(message);
  }
}

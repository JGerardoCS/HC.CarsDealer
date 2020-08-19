import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { ProductModel } from '../models/products.model';
import { ResponseModel } from '../models/response.model';
import { ProductsService } from './products.service';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductEditResolverService implements Resolve<ResponseModel<ProductModel>> {

  constructor(private productsService: ProductsService) {
  }

  resolve(route: ActivatedRouteSnapshot,
          state: RouterStateSnapshot)
        : Observable<ResponseModel<ProductModel>> {
    const id = route.paramMap.get('id');

    if (isNaN(+id)) {
      const message = ``;
      return of({ data: null, isSuccess: false, message });
    }

    return this.productsService.getProduct(+id)
            .pipe(map(response => response),
                  catchError(error => {
                    const message = `There was an error getting data`;
                    return of({ data: null, isSuccess: false, message });
                  }));
  }
}

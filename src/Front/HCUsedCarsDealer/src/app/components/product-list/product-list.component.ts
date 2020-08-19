import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../../models/products.model';
import { ProductsService } from '../../services/products.service';
import { AppMessagesService } from '../../services/app-messages.service';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: ProductModel[];

  constructor(private productsService: ProductsService,
              private appMessageService: AppMessagesService) {
    this.products = [];
  }

  ngOnInit(): void {
    this.productsService.getProducts()
        .subscribe(data => this.products = data);
  }

  deleteProduct(id: number, index: number): void {
    this.appMessageService.showConfirm('Please Confirm',
          'Do you want to delete this product?')
        .then(result => {
          if (result.value) {
            this.productsService.deleteProduct(id)
                .subscribe(result => {
                  if (result.isSuccess) {
                    this.appMessageService.showSuccess('Success', 'Product removed')
                        .then(result => {
                          this.products.splice(index, 1);
                        });
                  }
                });
          }
        });
  }

}

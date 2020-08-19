import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { ProductsService } from '../../services/products.service';
import { ProductModel } from '../../models/products.model';
import { AppMessagesService } from '../../services/app-messages.service';
import { ResponseModel } from '../../models/response.model';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  product: ProductModel;
  productForm: FormGroup;
  isUpdate: boolean;

  //#region Getters
  get isInvalidBrand(): boolean {
    const brandControl = this.productForm.get('brand');
    return (brandControl.touched || brandControl.dirty) && !brandControl.valid;
  }

  get isInvalidModel(): boolean {
    const control = this.productForm.get('model');
    return (control.touched || control.dirty) && !control.valid;
  }

  get isInvalidYear(): boolean {
    const control = this.productForm.get('year');
    return (control.touched || control.dirty) && !control.valid;
  }

  get isInvalidKilometers(): boolean {
    const control = this.productForm.get('kilometers');
    return (control.touched || control.dirty) && !control.valid;
  }

  get isInvalidPrice(): boolean {
    const control = this.productForm.get('price');
    return (control.touched || control.dirty) && !control.valid;
  }

  get isInvalidDescription(): boolean {
    const control = this.productForm.get('description');
    return (control.touched || control.dirty) && !control.valid;
  }
  //#endregion Getters

  constructor(private productsService: ProductsService,
              private route: ActivatedRoute,
              private fb: FormBuilder,
              private appMessagesService: AppMessagesService,
              private router: Router) {
    this.isUpdate = false;
  }

  ngOnInit(): void {
    this.initializeForm();
    const id = +this.route.snapshot.paramMap.get('id');
    this.isUpdate = id > 0;
    this.productsService.getProduct(id)
        .subscribe(data => this.onProductReceived(data));
  }

  private initializeForm(): void {
    this.productForm = this.fb.group({
      id: [0, [Validators.required]],
      model: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.maxLength(100)]],
      year: ['', [Validators.min(0), Validators.max(9000)]],
      brand: ['', [Validators.required, Validators.maxLength(50)]],
      kilometers: ['', [Validators.required, Validators.min(0), Validators.max(1000000)]],
      price: ['', [Validators.required, Validators.min(1), Validators.max(10000000)]]
    });
  }

  private onProductReceived(response: ResponseModel<ProductModel>) {
    this.product = response.data;
    this.productForm.setValue(response.data);
  }

  save() {
    if (this.product.id === 0) {
      this.productsService.createProduct(this.productForm.value)
            .subscribe(response => this.onProductCreated(response));
    } else {
      this.productsService.updateProduct(this.productForm.value)
            .subscribe(response => this.onProductUpdated());
    }
  }

  private onProductCreated(response: ResponseModel<ProductModel>) {
    if (response.isSuccess) {
      this.appMessagesService.showSuccess('Success', 'Product created').then(result => {
        this.initializeForm();
        this.onProductReceived(response);
        this.router.navigate(['products', response.data.id, 'edit']);
      });
    } else {
      this.appMessagesService.showError('Error', response.message);
    }
  }

  private onProductUpdated() {
  }

  cancel() {
    if (this.productForm.pristine) {
      this.router.navigate(['/products']);
      return false;
    }

    this.appMessagesService.showConfirm('Please Confirm',
          'Do you want to discard changes?')
        .then(result => {
          if (result.value) {
            this.router.navigate(['/products']);
          }
        });
  }

  getErrorMessage(controlName: string): string {
    let message = '';
    const errors = this.productForm.get(controlName).errors;

    if (errors) {

      if (errors.required) {
        message += 'This field is required';
      } else if (errors.min) {
        message += `The value must be greather than: ${errors.min.min}`;
      } else if (errors.max) {
        message += `The value must be grater than ${errors.max.max}`;
      } else if (errors.maxlength) {
        message += `The max length for this field is ${errors.maxlength.requiredLength}`;
      }

    }

    return message;
  }

}

import { TestBed } from '@angular/core/testing';

import { ProductEditResolverService } from './product-edit-resolver.service';

describe('ProductEditResolverService', () => {
  let service: ProductEditResolverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductEditResolverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { ProductModel } from './products.model';

export class ResponseModel<T> {
    data?: T;
    isSuccess: boolean;
    message: string;
}

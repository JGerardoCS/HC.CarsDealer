import { Injectable } from '@angular/core';
import Swal, { SweetAlertResult } from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AppMessagesService {

  constructor() { }

  showConfirm(title: string, text: string): Promise<SweetAlertResult> {
    return Swal.fire({
      title,
      text,
      icon: 'warning',
      showCancelButton: true,
    });
  }

  showError(title: string, text: string): void {
    Swal.fire({
      title,
      text,
      icon: 'error'
    });
  }

  showSuccess(title: string, text: string): Promise<SweetAlertResult> {
    return Swal.fire({
      title,
      text,
      icon: 'success'
    });
  }
}

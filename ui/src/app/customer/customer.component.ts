import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ReferenceDataService } from '../services/reference-data.service';
import { CustomerService } from '../services/customer.service';
import { CustomerModel } from '../models/customer.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  public customerTypes: string[] = [];

  public customers: CustomerModel[] = [];

  public newCustomer: CustomerModel = {
    customerId: null,
    customerName: null,
    customerType: null
  };

  constructor(
    private referenceDataService: ReferenceDataService,
    private customerService: CustomerService) { }

  ngOnInit() {
    this.referenceDataService.GetCustomerTypes().subscribe(customerTypes => this.customerTypes = customerTypes);
    this.customerService.GetCustomers().subscribe(customers => this.customers = customers);
  }

  public createCustomer(form: NgForm): void {
    if (form.invalid) {
      alert('form is not valid');
    } else {
      this.customerService.CreateCustomer(this.newCustomer).then(() => {
        this.customerService.GetCustomers().subscribe(customers => this.customers = customers);
      }).catch((errorResponse: HttpErrorResponse) => alert(`Error: ${errorResponse.error}`));
    }
  }
}

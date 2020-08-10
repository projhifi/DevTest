import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReferenceDataService {

  constructor(private httpClient: HttpClient) { }

  public GetCustomerTypes(): Observable<string[]> {
    return this.httpClient.get<string[]>('http://localhost:63235/ref-data/customer-types');
  }
}

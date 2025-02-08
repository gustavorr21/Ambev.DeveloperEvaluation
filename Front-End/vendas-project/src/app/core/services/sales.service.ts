import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SalesService {
  private apiUrl = 'https://localhost:7181/api/sales';

  constructor(private http: HttpClient) {}

  getSales(): Observable<any> {
    return this.http.get(`${this.apiUrl}`);
  }

  getSaleById(saleId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${saleId}`);
  }

  createSale(sale: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, sale);
  }

  updateSale(saleId: string, sale: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${saleId}`, sale);
  }

  cancelSale(saleId: string, isCancelled: boolean): Observable<any> {
    return this.http.put(`${this.apiUrl}/${saleId}/cancel/${isCancelled}`, {});
  }

  deleteSale(saleId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${saleId}`);
  }

  cancelSaleItem(itemId: string, isCancelled: boolean): Observable<any> {
    return this.http.put(
      `${this.apiUrl}/items/${itemId}/cancel/${isCancelled}`,
      {}
    );
  }
}

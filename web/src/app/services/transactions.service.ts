import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Transaction } from '../models/Transaction';
import { TransactionDetalle } from '../models/TransactionDetalle';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {
  private readonly apiUrl: string = environment.apiUrl;

  private http = inject(HttpClient);

  constructor() {
  }

  getTransactions(userName: string) {
    return this.http.get<Transaction[]>(`${this.apiUrl}/transaction?userName=${userName}`);
  }

  getTransactionPago(id: number) {
    return this.http.get<TransactionDetalle>(`${this.apiUrl}/transaction/pago?id=${id}`);
  }

  getTransactionRecarga(id: number) {
    return this.http.get<TransactionDetalle>(`${this.apiUrl}/transaction/recarga?id=${id}`);
  }

  getTransactionRetiro(id: number) {
    return this.http.get<TransactionDetalle>(`${this.apiUrl}/transaction/retiro?id=${id}`);
  }
}

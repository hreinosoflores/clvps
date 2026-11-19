import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { PagoDetalle } from '../models/PagoDetalle';
import { PagoConfirmacionResponse } from '../models/PagoConfirmacionResponse';

@Injectable({
  providedIn: 'root'
})
export class PagoService {

  private readonly apiUrl: string = environment.apiUrl;

  private http = inject(HttpClient);

  constructor() { }

  getPagoDetalle(userName: string, orden: number) {
    return this.http.get<PagoDetalle>(`${this.apiUrl}/pago?userName=${userName}&orden=${orden}`);
  }

  pagarOrden(userName: string, orden: number) {
    return this.http.get<PagoConfirmacionResponse>(`${this.apiUrl}/pago/pagar?userName=${userName}&orden=${orden}`);
  }

}

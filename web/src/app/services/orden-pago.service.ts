import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { OrdenPagoResponse } from '../models/OrdenPagoResponse';
import { OrdenPagoRequest } from '../models/OrdenPagoRequest';

@Injectable({
  providedIn: 'root'
})
export class OrdenPagoService {

  private readonly apiUrl: string = environment.apiUrl;

  private http = inject(HttpClient);

  private _idOrdenActual = signal(0);

  idOrdenActual = computed(() => this._idOrdenActual());

  setIdOrdenActual(id: number) {
    this._idOrdenActual.set(id);
  }

  constructor() {
    const idOrden = localStorage.getItem("orden");
    if (idOrden && parseInt(idOrden) > 0) {
      this.setIdOrdenActual(parseInt(idOrden));
    }
  }

  registrarOrdenPago(orden: OrdenPagoRequest) {
    return this.http.post<OrdenPagoResponse>(`${this.apiUrl}/ordenpago`, orden);
  }


}

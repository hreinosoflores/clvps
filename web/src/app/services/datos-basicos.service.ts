import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { DatosBasicos } from '../models/DatosBasicos';

@Injectable({
  providedIn: 'root'
})
export class DatosBasicosService {
  private readonly apiUrl: string = environment.apiUrl;

  private http = inject(HttpClient);

  constructor() {
  }

  getDatos(userName: string) {
    return this.http.get<DatosBasicos>(`${this.apiUrl}/datosBasicos?userName=${userName}`);
  }
}

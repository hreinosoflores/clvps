import { computed, inject, Injectable, signal } from '@angular/core';
import { Usuario } from '../models/Usuario';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly apiUrl: string = environment.apiUrl;

  private http = inject(HttpClient);

  //1: sin usuario autenticado, 2: ingresando, 3: usuario autenticado
  private _loginStatus = signal(1);
  private _usuarioActual = signal<Usuario | null>(null);

  loginStatus = computed(() => this._loginStatus());
  usuarioActual = computed(() => this._usuarioActual());

  setLoginStatus(status: number) {
    this._loginStatus.set(status);
  }

  setUsuario(usuario: Usuario | null) {
    this._usuarioActual.set(usuario);
  }

  constructor() {

    const userName = localStorage.getItem("userName");
    const nombreCompleto = localStorage.getItem("nombreCompleto");
    const rol = localStorage.getItem("rol");

    if (userName && nombreCompleto && rol) {
      this.setUsuario({
        userName,
        nombreCompleto,
        rol
      });
      this.setLoginStatus(3);
    } else {
      this.setUsuario(null);
      this.setLoginStatus(1);
    }
  }


  login(req: Usuario) {
    return this.http.post<Usuario>(`${this.apiUrl}/auth/login`, req);
  }




}

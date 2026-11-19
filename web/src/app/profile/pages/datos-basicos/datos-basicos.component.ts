import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit } from '@angular/core';
import { DatosBasicosService } from '../../../services/datos-basicos.service';
import { DatosBasicos } from '../../../models/DatosBasicos';
import { AuthService } from '../../../services/auth.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-datos-basicos',
  imports: [CommonModule, MatIconModule, MatButtonModule],
  templateUrl: './datos-basicos.component.html',
  styleUrl: './datos-basicos.component.css'
})
export class DatosBasicosComponent implements OnInit {
  private router = inject(Router);

  private datosService = inject(DatosBasicosService);

  private authService = inject(AuthService);

  usuarioActual = computed(() => this.authService.usuarioActual());

  datos?: DatosBasicos;

  ngOnInit(): void {
    this.datosService.getDatos(this.usuarioActual()?.userName!).subscribe({
      next: (resp) => {
        this.datos = resp;
        console.log(this.datos);
      },
      error: (error) => console.error('Failed to fetch transaction', error)
    })
  }

  recargar() {
    this.router.navigate(['/profile/recarga']);
  }

}

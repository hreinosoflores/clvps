import { Component, computed, inject, OnInit } from '@angular/core';
import { OrdenPagoService } from '../../../services/orden-pago.service';
import { OrdenPagoResponse } from '../../../models/OrdenPagoResponse';
import { ActivatedRoute, Router } from '@angular/router';
import { OrdenPagoRequest } from '../../../models/OrdenPagoRequest';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-redirect',
  imports: [CommonModule],
  templateUrl: './redirect.component.html',
  styleUrl: './redirect.component.css'
})
export class RedirectComponent implements OnInit {

  private route = inject(ActivatedRoute);
  private router = inject(Router);

  private ordenService = inject(OrdenPagoService);
  private authService = inject(AuthService);

  usuarioActual = computed(() => this.authService.usuarioActual());

  error: string = '';

  constructor() { }

  ngOnInit(): void {
    this.error = '';
    const params = this.route.snapshot.queryParams;
    const vendedor = params['vendedor'];
    const importe = params['importe'];
    const importeConPunto = parseFloat(importe.slice(0, -2) + '.' + importe.slice(-2));
    const request: OrdenPagoRequest = {
      monto: importeConPunto,
      usr_s: vendedor
    }

    this.ordenService.registrarOrdenPago(request).subscribe({
      next: (resp: OrdenPagoResponse) => {
        const id = resp.id!;
        localStorage.setItem('orden', id.toString());
        this.ordenService.setIdOrdenActual(id);
        if (this.usuarioActual()) {
          this.router.navigate(['/pago/detalle']);
        } else {
          this.router.navigate(['/auth']);
        }
      },
      error: (err) => {
        this.error = err.error.errorMessage;
      }
    });

  }

}

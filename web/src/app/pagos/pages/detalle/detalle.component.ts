import { Component, computed, inject, OnInit } from '@angular/core';
import { PagoService } from '../../../services/pago.service';
import { Router } from '@angular/router';
import { PagoDetalle } from '../../../models/PagoDetalle';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { SpinnerComponent } from '../../../shared/spinner/spinner.component';
import { PagoConfirmacionResponse } from '../../../models/PagoConfirmacionResponse';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../../services/auth.service';
import { OrdenPagoService } from '../../../services/orden-pago.service';

@Component({
  selector: 'app-detalle',
  imports: [CommonModule, MatIconModule, MatButtonModule],
  templateUrl: './detalle.component.html',
  styleUrl: './detalle.component.css'
})
export class DetalleComponent implements OnInit {

  private router = inject(Router);
  private dialog = inject(MatDialog);

  private pagoService = inject(PagoService);
  private authService = inject(AuthService);
  private ordenService = inject(OrdenPagoService);

  usuarioActual = computed(() => this.authService.usuarioActual());
  idOrdenActual = computed(() => this.ordenService.idOrdenActual());

  detalle?: PagoDetalle;
  saldoFinal?: number;
  saldoFinalAbs?: number;
  pagable: boolean = false;
  loaded: boolean = false;
  error: string = '';

  constructor() { }

  ngOnInit(): void {
    if (this.usuarioActual() && this.idOrdenActual()) {
      this.pagoService.getPagoDetalle(this.usuarioActual()?.userName!, this.idOrdenActual()).subscribe({
        next: (resp: PagoDetalle) => {
          this.detalle = resp;
          console.log(this.detalle);
          this.saldoFinal = (resp.saldoCuenta ?? 0) - (resp.montoPagar ?? 0);
          this.saldoFinalAbs = Math.abs(this.saldoFinal);
          this.pagable = this.saldoFinal >= 0;
          this.loaded = true;
        },
        error: (error) => console.error('Failed to fetch transaction', error)
      });
    }
  }

  pagarOrden() {
    this.error = '';
    const dialogRef = this.dialog.open(SpinnerComponent, {
      disableClose: true
    });
    if (this.usuarioActual() && this.idOrdenActual()) {
      this.pagoService.pagarOrden(this.usuarioActual()?.userName!, this.idOrdenActual()).subscribe({
        next: (resp: PagoConfirmacionResponse) => {
          localStorage.removeItem("orden");
          dialogRef.close();
          this.router.navigate(['/pago/confirmacion'], { state: { data: resp } });
        },
        error: (error) => {
          dialogRef.close();
          this.error = error.error.errorMessage;
        }
      });
    }

  }
}

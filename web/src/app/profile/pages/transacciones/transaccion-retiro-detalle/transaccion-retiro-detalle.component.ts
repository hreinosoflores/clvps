import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { TransactionsService } from '../../../../services/transactions.service';
import { ActivatedRoute } from '@angular/router';
import { TransactionDetalle } from '../../../../models/TransactionDetalle';
import { EstadoSolicitudRetiroPipe } from '../../../../pipes/estado-solicitud-retiro.pipe';

@Component({
  selector: 'app-transaccion-retiro-detalle',
  imports: [CommonModule, EstadoSolicitudRetiroPipe],
  templateUrl: './transaccion-retiro-detalle.component.html',
  styleUrl: './transaccion-retiro-detalle.component.css'
})
export class TransaccionRetiroDetalleComponent {
  private transactionsService = inject(TransactionsService);

  constructor(private route: ActivatedRoute) { }

  transaction?: TransactionDetalle;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.transactionsService.getTransactionRetiro(parseInt(id)).subscribe({
        next: (resp) => {
          this.transaction = resp;
          console.log(this.transaction);
        },
        error: (error) => console.error('Failed to fetch transaction', error)
      });
    }
  }
}

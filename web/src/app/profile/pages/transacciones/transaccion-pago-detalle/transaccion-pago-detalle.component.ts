import { Component, inject, OnInit } from '@angular/core';
import { TransactionsService } from '../../../../services/transactions.service';
import { TransactionDetalle } from '../../../../models/TransactionDetalle';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { EstadoOrdenPagoPipe } from '../../../../pipes/estado-orden-pago.pipe';

@Component({
  selector: 'app-transaccion-pago-detalle',
  imports: [CommonModule, EstadoOrdenPagoPipe],
  templateUrl: './transaccion-pago-detalle.component.html',
  styleUrl: './transaccion-pago-detalle.component.css'
})
export class TransaccionPagoDetalleComponent implements OnInit {
  private transactionsService = inject(TransactionsService);

  constructor(private route: ActivatedRoute) { }

  transaction?: TransactionDetalle;


  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.transactionsService.getTransactionPago(parseInt(id)).subscribe({
        next: (resp) => {
          this.transaction = resp;
          console.log(this.transaction);
        },
        error: (error) => console.error('Failed to fetch transaction', error)
      });
    }
  }
}

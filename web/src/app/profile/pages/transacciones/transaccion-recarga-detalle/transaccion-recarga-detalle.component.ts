import { Component, inject } from '@angular/core';
import { TransactionsService } from '../../../../services/transactions.service';
import { ActivatedRoute } from '@angular/router';
import { TransactionDetalle } from '../../../../models/TransactionDetalle';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-transaccion-recarga-detalle',
  imports: [CommonModule],
  templateUrl: './transaccion-recarga-detalle.component.html',
  styleUrl: './transaccion-recarga-detalle.component.css'
})
export class TransaccionRecargaDetalleComponent {
  private transactionsService = inject(TransactionsService);

  constructor(private route: ActivatedRoute) { }

  transaction?: TransactionDetalle;


  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.transactionsService.getTransactionRecarga(parseInt(id)).subscribe({
        next: (resp) => {
          this.transaction = resp;
          console.log(this.transaction);
        },
        error: (error) => console.error('Failed to fetch transaction', error)
      });
    }
  }
}

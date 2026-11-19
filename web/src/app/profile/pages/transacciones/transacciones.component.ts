import { CommonModule } from '@angular/common';
import { Component, computed, inject } from '@angular/core';
import { TransactionsService } from '../../../services/transactions.service';
import { AuthService } from '../../../services/auth.service';
import { Transaction } from '../../../models/Transaction';
import { TipoTransaccionPipe } from '../../../pipes/tipo-transaccion.pipe';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { TransaccionMontoComponent } from "../../components/transaccion-monto/transaccion-monto.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-transacciones',
  imports: [CommonModule, TipoTransaccionPipe, MatIconModule, MatButtonModule, TransaccionMontoComponent],
  templateUrl: './transacciones.component.html',
  styleUrl: './transacciones.component.css'
})
export class TransaccionesComponent {

  private router = inject(Router);
  private transactionsService = inject(TransactionsService);
  private authService = inject(AuthService);

  usuarioActual = computed(() => this.authService.usuarioActual());

  transactions: Transaction[] = [];

  ngOnInit(): void {
    this.transactionsService.getTransactions(this.usuarioActual()?.userName!).subscribe({
      next: (resp) => {
        this.transactions = resp;
        this.transactions.sort((a, b) => new Date(`${b.fecha!} ${b.hora!}`).getTime() - new Date(`${a.fecha!} ${a.hora!}`).getTime());
        console.log(this.transactions);
      },
      error: (error) => console.error('Login failed', error)
    })
  }

  goToDetails(transaction: Transaction) {
    if (transaction.id_ord) {
      this.router.navigate([`profile/mis-transacciones/pagos/${transaction.id}`]);
    } else if (transaction.id_rec) {
      this.router.navigate([`profile/mis-transacciones/recargas/${transaction.id}`]);
    } else if (transaction.id_retiro) {
      this.router.navigate([`profile/mis-transacciones/retiros/${transaction.id}`]);
    }
  }
}

import { Component, Input } from '@angular/core';
import { Transaction } from '../../../models/Transaction';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-transaccion-monto',
  imports: [CommonModule],
  templateUrl: './transaccion-monto.component.html',
  styleUrl: './transaccion-monto.component.css'
})
export class TransaccionMontoComponent {

  @Input() transaction!: Transaction;

  getColor() {
    if (this.transaction.id_ord) {
      switch (this.transaction.tipo) {
        case "1":
          return "text-danger";
        case "2":
          return "text-success";
      }
    } else if (this.transaction.id_rec) {
      return "text-success";
    } else if (this.transaction.id_retiro) {
      return "text-danger";
    }

    return "";
  }

  getSigno() {
    if (this.transaction.id_ord) {
      switch (this.transaction.tipo) {
        case "1":
          return "-";
        case "2":
          return "+";
      }
    } else if (this.transaction.id_rec) {
      return "+";
    } else if (this.transaction.id_retiro) {
      return "-";
    }

    return "";
  }


}

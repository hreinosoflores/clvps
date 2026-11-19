import { Pipe, PipeTransform } from '@angular/core';
import { Transaction } from '../models/Transaction';

@Pipe({
  name: 'tipoTransaccion'
})
export class TipoTransaccionPipe implements PipeTransform {

  transform(transaction: Transaction): string {

    if (transaction.id_ord) {
      switch (transaction.tipo) {
        case "1":
          return "Pago Realizado";
        case "2":
          return "Pago Recibido";
      }
    } else if (transaction.id_rec) {
      return "Recarga";
    } else if (transaction.id_retiro) {
      return "Retiro";
    }

    return "";
  }

}

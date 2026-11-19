import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'estadoOrdenPago'
})
export class EstadoOrdenPagoPipe implements PipeTransform {

  transform(estado?: string): string {
    switch (estado) {
      case "P":
        return "Pendiente";
      case "O":
        return "Pagada"
      case "X":
        return "Anulada"
      default:
        return "";
    }
  }

}

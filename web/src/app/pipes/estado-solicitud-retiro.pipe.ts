import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'estadoSolicitudRetiro'
})
export class EstadoSolicitudRetiroPipe implements PipeTransform {

  transform(estado?: string): string {
    switch (estado) {
      case "P":
        return "Pendiente";
      case "O":
        return "Efectuada"
      case "X":
        return "Anulada"
      default:
        return "";
    }
  }

}

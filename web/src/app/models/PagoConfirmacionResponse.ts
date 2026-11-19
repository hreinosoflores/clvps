export interface PagoConfirmacionResponse {
  idOrden?: number;
  beneficiario?: string;
  montoPagado?: number;
  fechaTransaccion?: string;
  horaTransaccion?: string;
  emailBuyer?: string;
  ok: boolean;
  errorMessage?: string;
}

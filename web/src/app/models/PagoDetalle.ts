export interface PagoDetalle {
  beneficiario?: string;
  idOrden?: number;
  nroCuenta?: string;
  saldoCuenta?: number;
  montoPagar?: number;
  ok: boolean;
  errorMessage?: string;
}

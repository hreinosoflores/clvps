import { OrdenPagoResponse } from "./OrdenPagoResponse";
import { Recarga } from "./Recarga";
import { SolicitudRetiro } from "./SolicitudRetiro";
import { VirtualAccount } from "./VirtualAccount";

export interface TransactionDetalle {
    id?: number;
    virtualAccount?: VirtualAccount;
    monto?: number;
    fecha?: Date;
    hora?: string;
    ok: boolean;
    errorMessage?: string;

    ordenPago?: OrdenPagoResponse;
    tipo?: string;
    recarga?: Recarga;
    solicitudRetiro?: SolicitudRetiro;
}














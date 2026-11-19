import { Buyer } from "./Buyer";
import { Seller } from "./Seller";

export interface OrdenPagoResponse {
  id?: number;
  monto?: number;
  estado?: string;
  fecha?: string;
  hora?: string;
  buyer?: Buyer;
  seller?: Seller;
  ok: boolean;
  errorMessage?: string;
}

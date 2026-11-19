import { Buyer } from "./Buyer";
import { Seller } from "./Seller";

export interface OrdenPagoRequest {
  id?: number;
  monto: number;
  usr_s: string;
}

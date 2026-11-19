import { Seller } from "./Seller";

export interface SolicitudRetiro {
  id?: number;
  monto?: number;
  estado?: string;
  fecha?: string;
  hora?: string;
  seller?: Seller;
}

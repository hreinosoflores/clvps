import { Usuario } from "./Usuario";

export interface Buyer {
  id?: string;
  user?: Usuario;
  nombres?: string;
  apellidos?: string;
  dni?: string;
  caR_EXT?: string;
}

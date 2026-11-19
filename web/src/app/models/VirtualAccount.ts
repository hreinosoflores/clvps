import { Usuario } from "./Usuario";

export interface VirtualAccount {
  id: number;
  user?: Usuario;
  nro_cuenta: string;
  saldo_soles: number;
  saldo_dolares: number;
  ok: boolean;
  errorMessage?: string;
}

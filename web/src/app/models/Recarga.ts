import { ExchangeSpot } from "./ExchangeSpot";
import { SystemManager } from "./SystemManager";

export interface Recarga {
  id?: number;
  monto?: number;
  ges?: number;
  gsm?: number;
  mesra?: number;
  msmra?: number;
  id_es?: number;
  exchangeSpot?: ExchangeSpot;
  id_sm?: number;
  systemManager?: SystemManager;
}

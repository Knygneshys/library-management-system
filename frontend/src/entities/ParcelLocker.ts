import { Guid } from "guid-typescript";
import type { ParcelLockerState } from "./ParcelLockerState";

export type ParcelLocker = {
  id: Guid;
  address: string;
  lockerState: ParcelLockerState;
};

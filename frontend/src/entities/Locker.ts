import { Guid } from "guid-typescript";
import type { LockerState } from "./LockerState";

export type Locker = {
  id: Guid,
  locationCode: string,
  height: number, 
  width: number,
  length: number,
  lockerState: LockerState,
  parcelLockerId: number
};

// TODO: Naudoti Šitus DTO kaip backe o ne form content
// export type LockerCreateDto = Omit<Locker, "id" | "parcelLockerId" | "lockerState">
export type LockerUpdateDto = Omit<Locker, "id" | "parcelLockerId">
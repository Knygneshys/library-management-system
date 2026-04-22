import { Guid } from "guid-typescript";

export type ParcelLocker = {
  id: Guid;
  address: string;
  lockerState: ParcelLockerState;
};

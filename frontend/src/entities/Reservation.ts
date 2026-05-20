import type { ReservationState } from "./enums/ReservationState";

export type Reservation = {
  id: string;
  isExtended: boolean;
  dueDate: string | null;
  wantsToReturn: boolean;
  state: ReservationState;
  bookId: string;
  copyId: string | null;
  userId: string;
};

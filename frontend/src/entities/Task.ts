import type { Guid } from "guid-typescript";
import type { LibrarianTaskType } from "./enums/LibrarianTaskType";

export type Task = {
  id: Guid;
  type: LibrarianTaskType;
  createdAt: string;
  reservationId: Guid;
  lockerId: Guid | null;
  bookId: Guid;
  lockerLocationCode: string | null;
  pinCodeLibrarian: string | null;
  pinCodeReader: string | null;
};

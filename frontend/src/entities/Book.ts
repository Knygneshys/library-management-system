import type { Guid } from "guid-typescript";
import type { ReservationState } from "./enums/ReservationState";

interface BookReservationDto {
  id: Guid;
  state: ReservationState;
}

interface BookCopyDto {
  id: Guid;
  code: string;
  isTaken: boolean;
}

export type Book = {
  id: Guid;
  title: string;
  summary: string;
  isbn: string;
  language: string;
  publishedAt: Date;
  author: string;
  printingHouse: string;
  genres: string[];
  publisher: string;
  activeReservation: BookReservationDto | null;
  copies: BookCopyDto[];
  freeCopyCount: number;
};

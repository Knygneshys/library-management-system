import { Guid } from "guid-typescript";

export type Author = {
  id: Guid;
  fullName: string;
  nationality: string;
  biography: string;
};

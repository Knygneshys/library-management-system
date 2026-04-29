import { Guid } from "guid-typescript";

export type PrintingHouse = {
  id: Guid;
  name: string;
  address: string;
  website: string;
  phone: string;
};

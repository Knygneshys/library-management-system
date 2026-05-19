import type { Guid } from "guid-typescript";

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
};

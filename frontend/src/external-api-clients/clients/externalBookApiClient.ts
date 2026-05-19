import { apiClient } from "../apiClient";
import { bookUris } from "../../utils/apiUriUtils";
import { type Book } from "../../entities/Book";

export const getAllBooks = async () => {
  try {
    const data = await apiClient.get<Book[]>(bookUris.GET_ALL);

    return data.data;
  } catch (error) {
    console.error("Error fetching books:", error);

    return [];
  }
};

export const createBook = async (book: Book) => {
  try {
    const data = await apiClient.post<Book[]>(bookUris.CREATE, book);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const updateBook = async (book: Book) => {
  try {
    const uri = `${bookUris.UPDATE}/${book.id}`;
    const data = await apiClient.put<Book[]>(uri, book);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const deleteBook = async (book: Book) => {
  try {
    const uri = `${bookUris.DELETE}/${book.id}`;
    const data = await apiClient.delete<Book[]>(uri);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

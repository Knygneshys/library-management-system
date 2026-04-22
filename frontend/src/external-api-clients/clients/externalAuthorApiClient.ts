import { apiClient } from "../apiClient";
import { authorUris } from "../../utils/apiUriUtils";
import { type Author } from "../../entities/Author";

export const getAllAuthors = async () => {
  try {
    const data = await apiClient.get<Author[]>(authorUris.GET_ALL);

    return data.data;
  } catch (error) {
    console.error("Error fetching authors:", error);

    return [];
  }
};

export const createAuthor = async (author: Author) => {
  try {
    const data = await apiClient.post<Author[]>(authorUris.CREATE, author);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const updateAuthor = async (author: Author) => {
  try {
    const uri = `${authorUris.UPDATE}/${author.id}`;
    const data = await apiClient.put<Author[]>(uri, author);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const deleteAuthor = async (author: Author) => {
  try {
    const uri = `${authorUris.DELETE}/${author.id}`;
    const data = await apiClient.delete<Author[]>(uri);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

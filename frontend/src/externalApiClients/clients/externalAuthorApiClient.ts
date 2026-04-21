import { apiClient } from "../apiClient";
import { authorUris } from "../api-utils/apiUriUtils";
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

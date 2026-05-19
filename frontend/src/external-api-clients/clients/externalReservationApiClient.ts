import { apiClient } from "../apiClient";
import { reservationUris } from "../../utils/apiUriUtils";
import type { Guid } from "guid-typescript";

export const returnBook = async (reservationId: Guid) => {
  console.log("Returning book with reservation ID:", reservationId);
  try {
    const data = await apiClient.put(
      `${reservationUris.RETURN_BOOK}/${reservationId}`,
    );

    return data.data;
  } catch (error) {
    console.error("Error returning book:", error);

    throw error;
  }
};
export const reserveBook = async (bookId: Guid | string) => {
  try {
    const response = await apiClient.post(
      `${reservationUris.RESERVE_BOOK}/${bookId}`,
    );

    return response.data;
  } catch (error) {
    console.error("Error reserving book:", error);
    throw error;
  }
};

export const goToQueue = async (bookId: Guid | string) => {
  try {
    const response = await apiClient.post(
      `${reservationUris.GO_TO_QUEUE}/${bookId}`,
    );

    return response.data;
  } catch (error) {
    console.error("Error joining queue:", error);
    throw error;
  }
};

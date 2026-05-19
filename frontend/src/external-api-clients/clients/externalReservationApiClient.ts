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

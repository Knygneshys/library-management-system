import { apiClient } from "../apiClient";
import { printingHouseUris } from "../../utils/apiUriUtils";
import { type PrintingHouse } from "../../entities/PrintingHouse";

export const getAll = async () => {
  try {
    const data = await apiClient.get<PrintingHouse[]>(printingHouseUris.GET_ALL);

    return data.data;
  } catch (error) {
    console.error("Error fetching printing houses:", error);

    return [];
  }
};

export const sendCreateRequest = async (printingHouse: PrintingHouse) => {
  try {
    const data = await apiClient.post<PrintingHouse[]>(printingHouseUris.CREATE, printingHouse);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const sendUpdateRequest = async (printingHouse: PrintingHouse) => {
  try {
    const uri = `${printingHouseUris.UPDATE}/${printingHouse.id}`;
    const data = await apiClient.put<PrintingHouse[]>(uri, printingHouse);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const sendDeletionRequest = async (printingHouse: PrintingHouse) => {
  try {
    const uri = `${printingHouseUris.DELETE}/${printingHouse.id}`;
    const data = await apiClient.delete<PrintingHouse[]>(uri);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

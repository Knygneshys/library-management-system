import { apiClient } from "../apiClient";
import { parcelLockerUris } from "../../utils/apiUriUtils";
import { type ParcelLocker } from "../../entities/ParcelLocker";

export const getAllParcelLockers = async () => {
  try {
    const data = await apiClient.get<ParcelLocker[]>(parcelLockerUris.GET_ALL);

    return data.data;
  } catch (error) {
    console.error("Error fetching parcel lockers:", error);

    return [];
  }
};

export const createParcelLocker = async (parcelLocker: ParcelLocker) => {
  try {
    const data = await apiClient.post<ParcelLocker[]>(
      parcelLockerUris.CREATE,
      parcelLocker,
    );

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const updateParcelLocker = async (parcelLocker: ParcelLocker) => {
  try {
    const uri = `${parcelLockerUris.UPDATE}/${parcelLocker.id}`;
    const data = await apiClient.put<ParcelLocker[]>(uri, parcelLocker);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const deleteParcelLocker = async (parcelLocker: ParcelLocker) => {
  try {
    const uri = `${parcelLockerUris.DELETE}/${parcelLocker.id}`;
    const data = await apiClient.delete<ParcelLocker[]>(uri);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

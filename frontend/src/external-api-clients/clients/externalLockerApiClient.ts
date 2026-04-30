import { apiClient } from "../apiClient";
import type { Locker } from "../../entities/Locker";
import type { ParcelLocker } from "../../entities/ParcelLocker";
import type { LockerCreationFormContent } from "../../views/pages/ParcelLockerDetailsPage/Dialogs/LockerCreationForm";
import type { Guid } from "guid-typescript";
import { lockerUris } from "../../utils/apiUriUtils";

export const getAllLockers = async () => {
  try {
    const data = await apiClient.get<Locker[]>(lockerUris.GET_ALL);

    return data.data;
  } catch (error) {
    console.error("Error fetching lockers:", error);

    return [];
  }
};

export const getLockersByParcelLocker = async (parcelLocker: ParcelLocker) => {
  try {
    const data = await apiClient.get<Locker[]>(
      `${lockerUris.GET_BY_PARCEL_LOCKER}/${parcelLocker.id}`,
    );

    return data.data;
  } catch (error) {
    console.error("Error fetching lockers:", error);

    return [];
  }
};

export const createLocker = async (
  parcelLockerId: Guid,
  locker: LockerCreationFormContent,
) => {
  try {
    const uri = `${lockerUris.CREATE}/${parcelLockerId}`;
    const data = await apiClient.post<Locker>(uri, locker);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const updateLocker = async (locker: Locker) => {
  try {
    const uri = `${lockerUris.UPDATE}/${locker.id}`;
    const data = await apiClient.put<Locker>(uri, locker);

    return data.data;
  } catch (error) {
    console.error(error);

    throw error;
  }
};

export const deleteLocker = async (locker: Locker) => {
  try {
    const uri = `${lockerUris.DELETE}/${locker.id}`;
    await apiClient.delete(uri);
  } catch (error) {
    console.error(error);

    throw error;
  }
};

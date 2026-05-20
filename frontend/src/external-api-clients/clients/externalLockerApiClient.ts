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

export const resetLocker = async (lockerId: string, pinCode: string) => {
  try {
    const uri = `/locker/${lockerId}/reset`;
    const response = await apiClient.post<{ success: boolean }>(uri, { pinCode });
    return response.data;
  } catch (error) {
    console.error("Error resetting locker:", error);
    throw error;
  }
};

export const insertBook = async (lockerId: string, pinCode: string) => {
  try {
    const uri = `/locker/${lockerId}/insert`;
    const response = await apiClient.post<{ success: boolean }>(uri, { pinCode });
    return response.data;
  } catch (error) {
    console.error("Error inserting book:", error);
    throw error;
  }
};

export const submitPin = async (pinCode: string) => {
  try {
    // Jei įsidėsi į utils, pakeisk eilutę į: const uri = lockerUris.SUBMIT_PIN;
    const uri = "/locker/submit-pin"; 
    
    // Tikimės gauti objektą su lockerId
    const response = await apiClient.post<{ success: boolean; lockerId: string }>(uri, { pinCode });
    return response.data;
  } catch (error) {
    console.error("Error submitting PIN:", error);
    throw error;
  }
};

export const isLockerClosed = async (lockerId: string) => {
  try {
    const uri = `/locker/${lockerId}/is-closed`;
    const response = await apiClient.get<{ closed: boolean }>(uri);
    return response.data;
  } catch (error) {
    console.error("Error checking locker status:", error);
    throw error;
  }
};

export const closeLocker = async (lockerId: string) => {
  try {
    const uri = `/locker/${lockerId}/close`;
    const response = await apiClient.post<{ success: boolean }>(uri, {});
    return response.data;
  } catch (error) {
    console.error("Error closing locker:", error);
    throw error;
  }
};

import { type Task } from "../../entities/Task";
import { taskUris } from "../../utils/apiUriUtils";
import { apiClient } from "../apiClient";

export const getAllTasks = async (): Promise<Task[]> => {
  try {
    const response = await apiClient.get<Task[]>(taskUris.GET_ALL);
    return response.data;
  } catch (error) {
    console.error("Error fetching tasks:", error);
    throw error;
  }
};

import axios from "axios";
import toast from "react-hot-toast";

export const successfullCreateMessage = (name: string) => {
  return `${name} has been created successfully!`;
};

export const successfullUpdateMessage = (name: string) => {
  return `${name} has been updated successfully!`;
};

export const successfullDeleteMessage = (name: string) => {
  return `${name} has been deleted successfully!`;
};

export const unexpectedErrorMessage = "An unexpected error occurred!";

export function handleErrorToast(error: unknown) {
  let toastOccurred = false;
  if (axios.isAxiosError(error)) {
    const backendMessage = error?.response?.data;
    if (typeof backendMessage === "string") {
      toastOccurred = true;
      toast.error(backendMessage);
    }
  }

  if (!toastOccurred) {
    toast.error(unexpectedErrorMessage);
  }
}

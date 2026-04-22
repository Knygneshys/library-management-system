import { Box, Button } from "@mui/material";
import {
  createParcelLocker,
  deleteParcelLocker,
  getAllParcelLockers,
  updateParcelLocker,
} from "../../../external-api-clients/clients/externalParcelLockerApiClient";
import { useEffect, useState } from "react";
import type { ParcelLocker } from "../../../entities/ParcelLocker";
import ParcelLockerTable from "./ParcelLockerTable/ParcelLockerTable";
import {
  primaryColor,
  secondaryColor,
} from "../../../constants/colorConstants";
import ParcelLockerCreationDialog from "./ParcelLockerCreationDialog/ParcelLockerCreationDialog";
import toast from "react-hot-toast";
import {
  handleErrorToast,
  successfullCreateMessage,
  successfullDeleteMessage,
  successfullUpdateMessage,
} from "../../../utils/toastUtils";
import ParcelLockerUpdateDialog from "./ParcelLockerUpdateDialog/ParcelLockerUpdateDialog";

export default function ParcelLockerPage() {
  const [parcelLockers, setParcelLockers] = useState<ParcelLocker[]>([]);

  const [creationDialogIsOpen, setCreationDialogIsOpen] =
    useState<boolean>(false);
  const [updateDialogIsOpen, setUpdateDialogIsOpen] = useState<boolean>(false);
  const [parcelLockerThatIsBeingUpdated, setParcelLockerThatIsBeingUpdated] =
    useState<ParcelLocker | null>(null);

  useEffect(() => {
    const fetchParcelLockers = async () => {
      const data = await getAllParcelLockers();

      setParcelLockers(data);
    };

    fetchParcelLockers();
  }, []);

  const handleCreationDialogOpen = () => {
    setCreationDialogIsOpen(true);
  };

  const handleCreationDialogClose = () => {
    setCreationDialogIsOpen(false);
  };

  const handleCreationFormSubmit = async (parcelLocker: ParcelLocker) => {
    try {
      const parcelLockers = await createParcelLocker(parcelLocker);

      setParcelLockers(parcelLockers);
      toast.success(successfullCreateMessage("Parcel Locker"));
    } catch (error) {
      handleErrorToast(error);
    }

    handleCreationDialogClose();
  };

  const handleUpdateDialogOpen = (parcelLocker: ParcelLocker) => {
    setParcelLockerThatIsBeingUpdated(parcelLocker);
    setUpdateDialogIsOpen(true);
  };

  const handleUpdateDialogClose = () => {
    setParcelLockerThatIsBeingUpdated(null);
    setUpdateDialogIsOpen(false);
  };

  const handleUpdateFormSubmit = async (parcelLocker: ParcelLocker) => {
    try {
      const parcelLockers = await updateParcelLocker(parcelLocker);

      setParcelLockers(parcelLockers);
      toast.success(successfullUpdateMessage("Parcel Locker"));
    } catch (error) {
      handleErrorToast(error);
    }

    handleUpdateDialogClose();
  };

  const handleParcelLockerDelete = async (parcelLocker: ParcelLocker) => {
    try {
      const parcelLockers = await deleteParcelLocker(parcelLocker);

      setParcelLockers(parcelLockers);
      toast.success(successfullDeleteMessage("Parcel Locker"));
    } catch (error) {
      handleErrorToast(error);
    }
  };

  return (
    <Box>
      <Box sx={{ float: "right" }}>
        <Button
          onClick={handleCreationDialogOpen}
          sx={{
            margin: "20px",
            marginTop: "70px",
            background: primaryColor,
            color: secondaryColor,
            width: "200px",
          }}
        >
          Create Parcel Locker
        </Button>
      </Box>
      <ParcelLockerTable
        parcelLockers={parcelLockers}
        onUpdateButtonClick={handleUpdateDialogOpen}
        onDeleteButtonClick={handleParcelLockerDelete}
      />
      <ParcelLockerCreationDialog
        isOpen={creationDialogIsOpen}
        handleClose={handleCreationDialogClose}
        onSubmit={handleCreationFormSubmit}
      />
      {updateDialogIsOpen && parcelLockerThatIsBeingUpdated && (
        <ParcelLockerUpdateDialog
          isOpen={updateDialogIsOpen}
          parcelLocker={parcelLockerThatIsBeingUpdated}
          handleClose={handleUpdateDialogClose}
          handleSubmit={handleUpdateFormSubmit}
        />
      )}
    </Box>
  );
}

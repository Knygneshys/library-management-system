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
import ParcelLockerDeletionDialog from "./ParcelLockerDeletionDialog/ParcelLockerDeletionDialog";

export default function ParcelLockerPage() {
  const [parcelLockers, setParcelLockers] = useState<ParcelLocker[]>([]);

  const [creationDialogIsOpen, setCreationDialogIsOpen] =
    useState<boolean>(false);
  const [updateDialogIsOpen, setUpdateDialogIsOpen] = useState<boolean>(false);
  const [parcelLockerThatIsBeingUpdated, setParcelLockerThatIsBeingUpdated] =
    useState<ParcelLocker | null>(null);
  const [deletionDialogIsOpen, setDeletionDialogIsOpen] =
    useState<boolean>(false);
  const [parcelLockerThatIsBeingDeleted, setParcelLockerThatIsBeingDeleted] =
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

  const handleDeletionDialogOpen = (parcelLocker: ParcelLocker) => {
    setParcelLockerThatIsBeingDeleted(parcelLocker);
    setDeletionDialogIsOpen(true);
  };

  const handleDeletionDialogClose = () => {
    setParcelLockerThatIsBeingDeleted(null);
    setDeletionDialogIsOpen(false);
  };

  const handleParcelLockerDelete = async (parcelLocker: ParcelLocker) => {
    try {
      const parcelLockers = await deleteParcelLocker(parcelLocker);
      setParcelLockers(parcelLockers);
      toast.success(successfullDeleteMessage("Parcel Locker"));
    } catch (error) {
      handleErrorToast(error);
    }
    handleDeletionDialogClose();
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
        onDeleteButtonClick={handleDeletionDialogOpen}
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
      {deletionDialogIsOpen && parcelLockerThatIsBeingDeleted && (
        <ParcelLockerDeletionDialog
          isOpen={deletionDialogIsOpen}
          parcelLocker={parcelLockerThatIsBeingDeleted}
          handleClose={handleDeletionDialogClose}
          handleDelete={() =>
            handleParcelLockerDelete(parcelLockerThatIsBeingDeleted)
          }
        />
      )}
    </Box>
  );
}

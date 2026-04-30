import { Box, Button, Stack } from "@mui/material";
import { useEffect, useState } from "react";
import type { ParcelLocker } from "../../../entities/ParcelLocker";
import {
  primaryColor,
  secondaryColor,
} from "../../../constants/colorConstants";
import toast from "react-hot-toast";
import {
  handleErrorToast,
  successfullCreateMessage,
  successfullDeleteMessage,
  successfullUpdateMessage,
} from "../../../utils/toastUtils";
import {
  createLocker,
  deleteLocker,
  getLockersByParcelLocker,
  updateLocker,
} from "../../../external-api-clients/clients/externalLockerApiClient";
import type { Locker } from "../../../entities/Locker";
import { LockerTable } from "./LockerTable";
import LockerCreationDialog from "./Dialogs/LockerCreationDialog";
import LockerUpdateDialog from "./Dialogs/LockerUpdateDialog";
import LockerDeletionDialog from "./Dialogs/LockerDeletionDialog";
import type { LockerCreationFormContent } from "./Dialogs/LockerCreationForm";

interface ParcelListProps {
  parcelLocker: ParcelLocker;
}

export default function LockerList({ parcelLocker }: ParcelListProps) {
  const [lockers, setLockers] = useState<Locker[]>([]);

  const [creationDialogIsOpen, setCreationDialogIsOpen] =
    useState<boolean>(false);
  const [updateDialogIsOpen, setUpdateDialogIsOpen] = useState<boolean>(false);
  const [lockerThatIsBeingUpdated, setLockerThatIsBeingUpdated] =
    useState<Locker>();
  const [deletionDialogIsOpen, setDeletionDialogIsOpen] =
    useState<boolean>(false);
  const [lockerThatIsBeingDeleted, setLockerThatIsBeingDeleted] =
    useState<Locker>();

  useEffect(() => {
    const fetchLockers = async () => {
      const data = await getLockersByParcelLocker(parcelLocker);

      setLockers(data);
    };

    fetchLockers();
  }, []);

  const handleCreationDialogOpen = () => {
    setCreationDialogIsOpen(true);
  };

  const handleCreationDialogClose = () => {
    setCreationDialogIsOpen(false);
  };

  const handleCreationFormSubmit = async (
    locker: LockerCreationFormContent,
  ) => {
    try {
      const newLocker = await createLocker(parcelLocker.id, locker);
      setLockers((prev) => [...prev, newLocker]);
      toast.success(successfullCreateMessage("Locker"));
    } catch (error) {
      handleErrorToast(error);
    }

    handleCreationDialogClose();
  };

  const handleUpdateDialogOpen = (locker: Locker) => {
    setLockerThatIsBeingUpdated(locker);
    setUpdateDialogIsOpen(true);
  };

  const handleUpdateDialogClose = () => {
    setLockerThatIsBeingUpdated(undefined);
    setUpdateDialogIsOpen(false);
  };

  const handleUpdateFormSubmit = async (locker: Locker) => {
    try {
      const updatedLocker = await updateLocker(locker);
      setLockers((prev) =>
        prev.map((pl) => (pl.id === updatedLocker.id ? updatedLocker : pl)),
      );
      toast.success(successfullUpdateMessage("Locker"));
    } catch (error) {
      handleErrorToast(error);
    }

    handleUpdateDialogClose();
  };

  const handleDeletionDialogOpen = (locker: Locker) => {
    setLockerThatIsBeingDeleted(locker);
    setDeletionDialogIsOpen(true);
  };

  const handleDeletionDialogClose = () => {
    setLockerThatIsBeingDeleted(undefined);
    setDeletionDialogIsOpen(false);
  };

  const handleLockerDelete = async (locker: Locker) => {
    try {
      await deleteLocker(locker);
      setLockers((prev) => prev.filter((pl) => pl.id !== locker.id));
      toast.success(successfullDeleteMessage("Locker"));
    } catch (error) {
      handleErrorToast(error);
    }
    handleDeletionDialogClose();
  };

  return (
    <Box>
      <Stack spacing={2}>
        <Button
          onClick={handleCreationDialogOpen}
          sx={{
            background: primaryColor,
            color: secondaryColor,
            width: "200px",
          }}
        >
          Create Locker
        </Button>
        <LockerTable
          lockers={lockers}
          onUpdateButtonClick={handleUpdateDialogOpen}
          onDeleteButtonClick={handleDeletionDialogOpen}
        />
      </Stack>
      <LockerCreationDialog
        isOpen={creationDialogIsOpen}
        handleClose={handleCreationDialogClose}
        onSubmit={handleCreationFormSubmit}
      />
      {updateDialogIsOpen && lockerThatIsBeingUpdated && (
        <LockerUpdateDialog
          isOpen={updateDialogIsOpen}
          locker={lockerThatIsBeingUpdated}
          handleClose={handleUpdateDialogClose}
          handleSubmit={handleUpdateFormSubmit}
        />
      )}
      {deletionDialogIsOpen && lockerThatIsBeingDeleted && (
        <LockerDeletionDialog
          isOpen={deletionDialogIsOpen}
          locker={lockerThatIsBeingDeleted}
          handleClose={handleDeletionDialogClose}
          handleDelete={() => handleLockerDelete(lockerThatIsBeingDeleted)}
        />
      )}
    </Box>
  );
}

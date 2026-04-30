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
import { Route, Routes, useNavigate } from "react-router";
import ParcelLockerDetailsPage from "../ParcelLockerDetailsPage/ParcelLockerDetailsPage";
import { useParcelLockers } from "../../layout/ParcelLockerLayout";

export default function ParcelLockerListPage() {
  const { parcelLockers, setParcelLockers } = useParcelLockers()
  const navigate = useNavigate();

  const [creationDialogIsOpen, setCreationDialogIsOpen] =
    useState<boolean>(false);
  const [updateDialogIsOpen, setUpdateDialogIsOpen] = useState<boolean>(false);
  const [parcelLockerThatIsBeingUpdated, setParcelLockerThatIsBeingUpdated] =
    useState<ParcelLocker | null>(null);
  const [deletionDialogIsOpen, setDeletionDialogIsOpen] =
    useState<boolean>(false);
  const [parcelLockerThatIsBeingDeleted, setParcelLockerThatIsBeingDeleted] =
    useState<ParcelLocker | null>(null);

  const handleCreationDialogOpen = () => {
    setCreationDialogIsOpen(true);
  };

  const handleCreationDialogClose = () => {
    setCreationDialogIsOpen(false);
  };

  const handleCreationFormSubmit = async (parcelLocker: ParcelLocker) => {
    try {
      const newParcelLocker = await createParcelLocker(parcelLocker);
      setParcelLockers(prev => [...prev, newParcelLocker]);
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
      const updatedParcelLocker = await updateParcelLocker(parcelLocker);
      setParcelLockers(prev => prev.map(pl => pl.id === updatedParcelLocker.id ? updatedParcelLocker : pl));
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
      await deleteParcelLocker(parcelLocker);
      setParcelLockers(prev => prev.filter(pl => pl.id !== parcelLocker.id));
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

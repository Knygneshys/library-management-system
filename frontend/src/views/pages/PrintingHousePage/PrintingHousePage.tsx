import { Box, Button } from "@mui/material";
import {
  sendCreateRequest,
  sendDeletionRequest,
  getAll,
  sendUpdateRequest,
} from "../../../external-api-clients/clients/externalPrintingHouseApiClient";
import { useEffect, useState } from "react";
import type { PrintingHouse } from "../../../entities/PrintingHouse";
import PrintingHouseTable from "./PrintingHouseTable/PrintingHouseTable";
import {
  primaryColor,
  secondaryColor,
} from "../../../constants/colorConstants";
import PrintingHouseCreationDialog from "./PrintingHouseCreationDialog/PrintingHouseCreationDialog";
import toast from "react-hot-toast";
import {
  handleErrorToast,
  successfullCreateMessage,
  successfullDeleteMessage,
  successfullUpdateMessage,
} from "../../../utils/toastUtils";
import PrintingHouseUpdateDialog from "./PrintingHouseUpdateDialog/PrintingHouseUpdateDialog";
import PrintingHouseDeletionDialog from "./PrintingHouseDeletionDialog/PrintingHouseDeletionDialog";

export default function PrintingHousePage() {
  const [printingHouses, setPrintingHouses] = useState<PrintingHouse[]>([]);

  const [creationDialogIsOpen, setCreationDialogIsOpen] =
    useState<boolean>(false);
  const [updateDialogIsOpen, setUpdateDialogIsOpen] = useState<boolean>(false);
  const [printingHouseThatIsBeingUpdated, setPrintingHouseThatIsBeingUpdated] =
    useState<PrintingHouse | null>(null);
  const [deletionDialogIsOpen, setDeletionDialogIsOpen] =
    useState<boolean>(false);
  const [printingHouseThatIsBeingDeleted, setPrintingHouseThatIsBeingDeleted] =
    useState<PrintingHouse | null>(null);

  useEffect(() => {
    const fetchPrintingHouses = async () => {
      const data = await getAll();

      setPrintingHouses(data);
    };

    fetchPrintingHouses();
  }, []);

  const handleCreationDialogOpen = () => {
    setCreationDialogIsOpen(true);
  };

  const handleCreationDialogClose = () => {
    setCreationDialogIsOpen(false);
  };

  const handleCreationFormSubmit = async (printingHouse: PrintingHouse) => {
    try {
      const printingHouses = await sendCreateRequest(printingHouse);

      setPrintingHouses(printingHouses);
      toast.success(successfullCreateMessage("PrintingHouse"));
    } catch (error) {
      handleErrorToast(error);
    }

    handleCreationDialogClose();
  };

  const handleUpdateDialogOpen = (printingHouse: PrintingHouse) => {
    setPrintingHouseThatIsBeingUpdated(printingHouse);
    setUpdateDialogIsOpen(true);
  };

  const handleUpdateDialogClose = () => {
    setPrintingHouseThatIsBeingUpdated(null);
    setUpdateDialogIsOpen(false);
  };

  const handleUpdateFormSubmit = async (printingHouse: PrintingHouse) => {
    try {
      const printingHouses = await sendUpdateRequest(printingHouse);

      setPrintingHouses(printingHouses);
      toast.success(successfullUpdateMessage("PrintingHouse"));
    } catch (error) {
      handleErrorToast(error);
    }

    handleUpdateDialogClose();
  };

  const handleDeletionDialogOpen = (printingHouse: PrintingHouse) => {
    setPrintingHouseThatIsBeingDeleted(printingHouse);
    setDeletionDialogIsOpen(true);
  };

  const handleDeletionDialogClose = () => {
    setPrintingHouseThatIsBeingDeleted(null);
    setDeletionDialogIsOpen(false);
  };

  const handlePrintingHouseDelete = async (printingHouse: PrintingHouse) => {
    try {
      const printingHouses = await sendDeletionRequest(printingHouse);
      setPrintingHouses(printingHouses);
      toast.success(successfullDeleteMessage("PrintingHouse"));
    } catch (error) {
      handleErrorToast(error);
    }
    handleDeletionDialogClose()
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
          Create printing house
        </Button>
      </Box>
      <PrintingHouseTable
        printingHouses={printingHouses}
        onUpdateButtonClick={handleUpdateDialogOpen}
        onDeleteButtonClick={handleDeletionDialogOpen}
      />
      <PrintingHouseCreationDialog
        isOpen={creationDialogIsOpen}
        handleClose={handleCreationDialogClose}
        onSubmit={handleCreationFormSubmit}
      />
      {updateDialogIsOpen && printingHouseThatIsBeingUpdated && (
        <PrintingHouseUpdateDialog
          isOpen={updateDialogIsOpen}
          printingHouse={printingHouseThatIsBeingUpdated}
          handleClose={handleUpdateDialogClose}
          handleSubmit={handleUpdateFormSubmit}
        />
      )}
      {deletionDialogIsOpen && printingHouseThatIsBeingDeleted && (
        <PrintingHouseDeletionDialog
          isOpen={deletionDialogIsOpen}
          printingHouse={printingHouseThatIsBeingDeleted}
          handleClose={handleDeletionDialogClose}
          handleDelete={() => handlePrintingHouseDelete(printingHouseThatIsBeingDeleted)}
        />
      )}
    </Box>
  );
}

import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import type { PrintingHouse } from "../../../../entities/PrintingHouse";
import PrintingHouseCreationForm from "./PrintingHouseCreationForm/PrintingHouseCreationForm";

interface Props {
  isOpen: boolean;
  handleClose: () => void;
  onSubmit: (printingHouse: PrintingHouse) => void;
}

export default function PrintingHouseCreationDialog({
  isOpen,
  handleClose,
  onSubmit,
}: Props) {
  const dialogTitle = "Create printing house";

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <PrintingHouseCreationForm onSubmit={onSubmit} />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

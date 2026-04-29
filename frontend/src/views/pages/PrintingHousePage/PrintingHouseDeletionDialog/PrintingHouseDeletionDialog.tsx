import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Typography,
} from "@mui/material";
import type { PrintingHouse } from "../../../../entities/PrintingHouse";

type Props = {
  printingHouse: PrintingHouse;
  handleClose: () => void;
  handleDelete: () => Promise<void>;
  isOpen: boolean;
};

export default function PrintingHouseDeletionConfirmationPrompt({
  printingHouse,
  handleClose,
  handleDelete,
  isOpen,
}: Props) {
  const dialogTitle = `Delete "${printingHouse.name}"?`;

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <Typography>
          Are you sure you wish to delete the printing house: {printingHouse.name}?
        </Typography>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleDelete}>Delete</Button>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

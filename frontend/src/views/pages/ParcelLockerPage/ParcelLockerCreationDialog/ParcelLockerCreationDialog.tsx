import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import type { ParcelLocker } from "../../../../entities/ParcelLocker";
import ParcelLockerCreationForm from "./ParcelLockerCreationForm/ParcelLockerCreationForm";

interface Props {
  isOpen: boolean;
  handleClose: () => void;
  onSubmit: (parcelLocker: ParcelLocker) => void;
}

export default function ParcelLockerCreationDialog({
  isOpen,
  handleClose,
  onSubmit,
}: Props) {
  const dialogTitle = "Create Parcel Locker";

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <ParcelLockerCreationForm onSubmit={onSubmit} />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

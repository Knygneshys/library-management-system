import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Typography,
} from "@mui/material";
import type { ParcelLocker } from "../../../../entities/ParcelLocker";

type Props = {
  parcelLocker: ParcelLocker;
  handleClose: () => void;
  handleDelete: () => Promise<void>;
  isOpen: boolean;
};

export default function ParcelLockerDeletionConfirmationPrompt({
  parcelLocker,
  handleClose,
  handleDelete,
  isOpen,
}: Props) {
  const dialogTitle = `Delete "${parcelLocker.address}"?`;

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <Typography>
          Are you sure you wish to delete the parcel locker:{" "}
          {parcelLocker.address}?
        </Typography>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleDelete}>Delete</Button>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Typography,
} from "@mui/material";
import type { Locker } from "../../../../entities/Locker";

type Props = {
  locker: Locker;
  handleClose: () => void;
  handleDelete: () => Promise<void>;
  isOpen: boolean;
};

export default function LockerDeletionDialog({
  locker,
  handleClose,
  handleDelete,
  isOpen,
}: Props) {
  const dialogTitle = `Delete "${locker.locationCode}"?`;

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <Typography>
          Are you sure you wish to delete the parcel locker:{" "}
          {locker.locationCode}?
        </Typography>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleDelete}>Delete</Button>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

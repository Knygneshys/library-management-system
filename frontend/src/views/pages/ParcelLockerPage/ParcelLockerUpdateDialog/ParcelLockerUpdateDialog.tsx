import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import type { ParcelLocker } from "../../../../entities/ParcelLocker";
import ParcelLockerUpdateForm from "./ParcelLockerUpdateForm/ParcelLockerUpdateForm";

type Props = {
  parcelLocker: ParcelLocker;
  isOpen: boolean;
  handleClose: () => void;
  handleSubmit: (parcelLocker: ParcelLocker) => void;
};

export default function ParcelLockerUpdateDialog({
  parcelLocker,
  isOpen,
  handleClose,
  handleSubmit,
}: Props) {
  const dialogTitle = "Update Parcel Locker";

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <ParcelLockerUpdateForm
          parcelLocker={parcelLocker}
          handleSubmit={handleSubmit}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

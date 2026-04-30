import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import type { Locker } from "../../../../entities/Locker";
import LockerUpdateForm from "./LockerUpdateForm";

type Props = {
  locker: Locker;
  isOpen: boolean;
  handleClose: () => void;
  handleSubmit: (locker: Locker) => void;
};

export default function LockerUpdateDialog({
  locker,
  isOpen,
  handleClose,
  handleSubmit,
}: Props) {
  const dialogTitle = "Update Locker";

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <LockerUpdateForm
          locker={locker}
          handleSubmit={handleSubmit}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

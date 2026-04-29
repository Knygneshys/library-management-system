import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import type { PrintingHouse } from "../../../../entities/PrintingHouse";
import PrintingHouseUpdateForm from "./PrintingHouseUpdateForm/PrintingHouseUpdateForm";

type Props = {
  printingHouse: PrintingHouse;
  isOpen: boolean;
  handleClose: () => void;
  handleSubmit: (printingHouse: PrintingHouse) => void;
};

export default function PrintingHouseUpdateDialog({
  printingHouse,
  isOpen,
  handleClose,
  handleSubmit,
}: Props) {
  const dialogTitle = "Update printing house";

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <PrintingHouseUpdateForm printingHouse={printingHouse} handleSubmit={handleSubmit} />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

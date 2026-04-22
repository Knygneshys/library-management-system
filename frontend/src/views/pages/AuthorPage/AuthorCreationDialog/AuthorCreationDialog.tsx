import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import type { Author } from "../../../../entities/Author";
import AuthorCreationForm from "./AuthorCreationForm/AuthorCreationForm";

interface Props {
  isOpen: boolean;
  handleClose: () => void;
  onSubmit: (author: Author) => void;
}

export default function AuthorCreationDialog({
  isOpen,
  handleClose,
  onSubmit,
}: Props) {
  const dialogTitle = "Create Author";

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <AuthorCreationForm onSubmit={onSubmit} />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

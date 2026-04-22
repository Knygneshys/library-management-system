import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import type { Author } from "../../../../entities/Author";
import AuthorUpdateForm from "./AuthorUpdateForm/AuthorUpdateForm";

type Props = {
  author: Author;
  isOpen: boolean;
  handleClose: () => void;
  handleSubmit: (author: Author) => void;
};

export default function AuthorUpdateDialog({
  author,
  isOpen,
  handleClose,
  handleSubmit,
}: Props) {
  const dialogTitle = "Update Author";

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <AuthorUpdateForm author={author} handleSubmit={handleSubmit} />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

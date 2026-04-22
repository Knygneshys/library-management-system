import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Typography,
} from "@mui/material";
import type { Author } from "../../../../entities/Author";

type Props = {
  author: Author;
  handleClose: () => void;
  handleDelete: () => Promise<void>;
  isOpen: boolean;
};

export default function AuthorDeletionConfirmationPrompt({
  author,
  handleClose,
  handleDelete,
  isOpen,
}: Props) {
  const dialogTitle = `Delete "${author.fullName}"?`;

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      <DialogContent>
        <Typography>
          Are you sure you wish to delete the author: {author.fullName}?
        </Typography>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleDelete}>Delete</Button>
        <Button onClick={handleClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
}

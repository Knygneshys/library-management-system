import {
  Dialog,
  DialogContent,
  DialogTitle,
  Box,
  Typography,
  Chip,
  Stack,
} from "@mui/material";
import type { Book } from "../../../../entities/Book";

interface Props {
  isOpen: boolean;
  book: Book | null;
  handleClose: () => void;
}

export default function BookDetailsModal({
  isOpen,
  book,
  handleClose,
}: Props) {
  if (!book) return null;

  return (
    <Dialog open={isOpen} onClose={handleClose} maxWidth="sm" fullWidth>
      <DialogTitle sx={{ textAlign: "center" }}>Book Details</DialogTitle>
      <DialogContent>
        <Box sx={{ display: "flex", flexDirection: "column", gap: 2, mt: 2 }}>
          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              Title
            </Typography>
            <Typography variant="body1">{book.title}</Typography>
          </Box>

          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              Author
            </Typography>
            <Typography variant="body1">{book.author}</Typography>
          </Box>

          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              ISBN
            </Typography>
            <Typography variant="body1">{book.isbn}</Typography>
          </Box>

          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              Language
            </Typography>
            <Typography variant="body1">{book.language}</Typography>
          </Box>

          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              Published Date
            </Typography>
            <Typography variant="body1">
              {new Date(book.publishedAt).toLocaleDateString()}
            </Typography>
          </Box>

          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              Printing House
            </Typography>
            <Typography variant="body1">{book.printingHouse}</Typography>
          </Box>

          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              Publisher
            </Typography>
            <Typography variant="body1">{book.publisher}</Typography>
          </Box>

          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              Genres
            </Typography>
            <Stack direction="row" spacing={1} sx={{ mt: 1, flexWrap: "wrap" }}>
              {book.genres && book.genres.length > 0 ? (
                book.genres.map((genre, index) => (
                  <Chip key={index} label={genre} variant="outlined" />
                ))
              ) : (
                <Typography variant="body2" color="textSecondary">
                  No genres available
                </Typography>
              )}
            </Stack>
          </Box>

          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              Summary
            </Typography>
            <Typography variant="body2" sx={{ mt: 1 }}>
              {book.summary}
            </Typography>
          </Box>
        </Box>
      </DialogContent>
    </Dialog>
  );
}

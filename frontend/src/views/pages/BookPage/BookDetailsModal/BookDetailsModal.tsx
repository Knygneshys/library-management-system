import {
  Dialog,
  DialogContent,
  DialogTitle,
  Box,
  Typography,
  Chip,
  Stack,
  Button,
} from "@mui/material";
import type { Book } from "../../../../entities/Book";
import { ReservationState } from "../../../../entities/enums/ReservationState";
import {
  returnBook,
  reserveBook,
  goToQueue,
} from "../../../../external-api-clients/clients/externalReservationApiClient";
import { useState } from "react";
import toast from "react-hot-toast";
import {
  handleErrorToast,
  successfullUpdateMessage,
} from "../../../../utils/toastUtils";

interface Props {
  isOpen: boolean;
  book: Book | null;
  handleClose: () => void;
  onBookReturned: () => void;
}

export default function BookDetailsModal({
  isOpen,
  book,
  handleClose,
  onBookReturned,
}: Props) {
  const [isReturning, setIsReturning] = useState(false);
  const [isReserving, setIsReserving] = useState(false);
  const [isQueueing, setIsQueueing] = useState(false);

  if (!book) return null;

  const canReturn = book.activeReservation?.state === ReservationState.NotLate ||
                    book.activeReservation?.state === ReservationState.Late;

  const canReserve = book.freeCopyCount > 0;

  const canGoToQueue = book.freeCopyCount === 0 && !canReturn;

  const handleReturnBook = async () => {
    if (!book.activeReservation) return;

    try {
      setIsReturning(true);
      await returnBook(book.activeReservation.id);
      toast.success(successfullUpdateMessage("Book returned"));
      onBookReturned();
      handleClose();
    } catch (error) {
      handleErrorToast(error);
    } finally {
      setIsReturning(false);
    }
  };

  const handleReserveBook = async () => {
    try {
      setIsReserving(true);

      const wasReserved = await reserveBook(book.id.toString());

      if (!wasReserved) {
        toast.error("No free copies available");
        return;
      }

      toast.success(successfullUpdateMessage("Book reserved"));
      await onBookReturned();
      handleClose();
    } catch (error) {
      handleErrorToast(error);
    } finally {
      setIsReserving(false);
    }
  };

  const handleGoToQueue = async () => {
    try {
      setIsQueueing(true);

      await goToQueue(book.id.toString());

      toast.success(successfullUpdateMessage("Added to queue"));
      await onBookReturned();
      handleClose();
    } catch (error) {
      handleErrorToast(error);
    } finally {
      setIsQueueing(false);
    }
  };

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

          <Box>
            <Typography variant="subtitle2" color="textSecondary">
              Copies
            </Typography>

            <Typography variant="body2" sx={{ mt: 1 }}>
              Available copies: {book.freeCopyCount} /{" "}
              {book.copies?.length ?? 0}
            </Typography>

            <Stack spacing={1} sx={{ mt: 1 }}>
              {book.copies && book.copies.length > 0 ? (
                book.copies.map((copy) => (
                  <Box
                    key={copy.id.toString()}
                    sx={{
                      display: "flex",
                      justifyContent: "space-between",
                      alignItems: "center",
                    }}
                  >
                    <Typography variant="body2">{copy.code}</Typography>

                    <Chip
                      label={copy.isTaken ? "Taken" : "Available"}
                      color={copy.isTaken ? "error" : "success"}
                      size="small"
                      variant="outlined"
                    />
                  </Box>
                ))
              ) : (
                <Typography variant="body2" color="textSecondary">
                  No copies available
                </Typography>
              )}
            </Stack>
          </Box>

          {book.activeReservation !== null && (
            <Box>
              <Typography variant="subtitle2" color="textSecondary">
                Reservation State
              </Typography>
              <Typography variant="body1">
                {ReservationState[book.activeReservation.state]}
              </Typography>
            </Box>
          )}

          <Stack direction="row" spacing={2} sx={{ mt: 2 }}>
            {canReserve && (
              <Button
                variant="contained"
                color="primary"
                onClick={handleReserveBook}
                disabled={isReturning || isReserving || isQueueing}
                fullWidth
              >
                Reserve
              </Button>
            )}

            {canGoToQueue && (
              <Button
                variant="contained"
                color="primary"
                onClick={handleGoToQueue}
                disabled={isReturning || isReserving || isQueueing}
                fullWidth
              >
                Join Queue
              </Button>
            )}

            {canReturn && (
              <Button
                variant="contained"
                color="primary"
                onClick={handleReturnBook}
                disabled={isReturning || isReserving || isQueueing}
                fullWidth
              >
                Return
              </Button>
            )}

            <Button
              variant="outlined"
              onClick={handleClose}
              disabled={isReturning || isReserving || isQueueing}
              fullWidth
            >
              Close
            </Button>
          </Stack>
        </Box>
      </DialogContent>
    </Dialog>
  );
}

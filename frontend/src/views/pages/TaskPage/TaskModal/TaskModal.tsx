import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Typography,
  Stack,
  Divider,
} from "@mui/material";
import type { Task } from "../../../../entities/Task";

interface Props {
  task: Task | null;
  onClose: () => void;
}

export default function TaskModal({ task, onClose }: Props) {
  if (!task) return null;

  return (
    <Dialog open={!!task} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>Task details</DialogTitle>

      <DialogContent>
        <Stack spacing={2} sx={{ mt: 1 }}>
          <Typography>
            <strong>Type:</strong> {task.type}
          </Typography>

          <Divider />

          <Typography>
            <strong>Locker location:</strong> {task.lockerLocationCode ?? "—"}
          </Typography>
          <Typography>
            <strong>Librarian PIN:</strong> {task.pinCodeLibrarian ?? "—"}
          </Typography>
          <Typography>
            <strong>Reader PIN:</strong> {task.pinCodeReader ?? "—"}
          </Typography>

          <Divider />

          <Typography variant="body2" color="text.secondary">
            Reservation ID: {task.reservationId?.toString() ?? "—"}
          </Typography>
        </Stack>
      </DialogContent>

      <DialogActions>
        <Button onClick={onClose} variant="outlined">
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
}

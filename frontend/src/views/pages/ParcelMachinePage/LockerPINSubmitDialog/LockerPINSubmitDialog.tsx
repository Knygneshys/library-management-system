import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Box, Typography } from "@mui/material";
import LockerPINSubmitForm from "./LockerPINSubmitForm/LockerPINSubmitForm";

interface Props {
  isOpen: boolean;
  body: string;
  handleClose: () => void;
  onSubmitPin: (pin: string) => void;
  isDoorOpened: boolean;
  onCloseDoor: () => void;
}

export default function LockerPINSubmitDialog({ isOpen, body, handleClose, onSubmitPin, isDoorOpened, onCloseDoor }: Props) {
  const dialogTitle = isDoorOpened ? "Spintelė atidaryta" : "Įvesti PIN kodą";

  return (
    <Dialog open={isOpen} onClose={!isDoorOpened ? handleClose : undefined}>
      <DialogTitle sx={{ textAlign: "center" }}>{dialogTitle}</DialogTitle>
      
      <DialogContent sx={{ textAlign: "center", minWidth: "300px" }}>
        {!isDoorOpened ? (
          <LockerPINSubmitForm onSubmit={onSubmitPin} />
        ) : (
          <Box sx={{ mt: 2, p: 2, backgroundColor: "#e8f5e9", borderRadius: 2 }}>
            <Typography sx={{ mb: 3 }}>{body}</Typography>
            <Button variant="outlined" color="error" onClick={onCloseDoor} fullWidth>
              Uždaryti dureles
            </Button>
          </Box>
        )}
      </DialogContent>
      
      <DialogActions>
        {!isDoorOpened && <Button onClick={handleClose}>Atšaukti</Button>}
      </DialogActions>
    </Dialog>
  );
}
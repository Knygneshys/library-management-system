import { Box, Button, Typography } from "@mui/material";
import { useState } from "react";
import toast from "react-hot-toast";
import { primaryColor, secondaryColor } from "../../../constants/colorConstants";
import { handleErrorToast } from "../../../utils/toastUtils";
import { submitPin, resetLocker, insertBook } from "../../../external-api-clients/clients/externalLockerApiClient";
import LockerPINSubmitDialog from "./LockerPINSubmitDialog/LockerPINSubmitDialog";

export default function TakeBookPage() {
  const [pinDialogIsOpen, setPinDialogIsOpen] = useState<boolean>(false);
  const [isDoorOpened, setIsDoorOpened] = useState<boolean>(false);
  const [activePin, setActivePin] = useState<string>("");
  const [openedLockerId, setOpenedLockerId] = useState<string | null>(null);
  const [isIssue, setIsIssue] = useState<boolean>();

  const handleIssueBookEvent = () => {
    setPinDialogIsOpen(true);
    setIsIssue(true)
  };

  const handleInsertBookEvent = () => {
    setPinDialogIsOpen(true);
    setIsIssue(false)
  };

  const handlePinDialogClose = () => {
    setPinDialogIsOpen(false);
    setIsDoorOpened(false);
    setActivePin("");
    setOpenedLockerId(null);
    setIsIssue(undefined);
  };

  const handlePINSubmitEvent = async (pin: string) => {
    try {
      const result = await submitPin(pin);
      setOpenedLockerId(result.lockerId);
      setActivePin(pin);
      setIsDoorOpened(true);
      toast.success("Spintelė atidaryta!");
    } catch (error: any) {
      const backendMessage = error.response?.data?.message || error.response?.data?.title;

      if (backendMessage) {
        toast.error(backendMessage);
      } else {
        handleErrorToast(error);
      }
    }
  };

  const handleCloseDoor = async () => {
    if (!openedLockerId) return;

    try {
      if(isIssue){
        await resetLocker(openedLockerId, activePin);
        toast.success("Operacija sėkmingai baigta! Knyga atsiimta.");
      }else{
        await insertBook(openedLockerId, activePin);
        toast.success("Operacija sėkmingai baigta! Knyga įdėta.");
      }
      handlePinDialogClose();
    } catch (error) {
      handleErrorToast(error);
    }
  };

  return (
    <Box sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", minHeight: "70vh", textAlign: "center" }}>
      <Typography variant="h3" gutterBottom sx={{ fontWeight: "bold" }}>
        Paštomato Terminalas
      </Typography>
      <Typography variant="h6" color="textSecondary" sx={{ mb: 6 }}>
        Pasirinkite norimą atlikti veiksmą ekrane
      </Typography>

      <Box sx={{ display: "flex", gap: 4 }}>
        <Button
          onClick={handleIssueBookEvent}
          sx={{ width: "120px", height: "80px", background: primaryColor, color: secondaryColor, fontSize: "1rem", borderRadius: "16px" }}
        >
          Pasiimti knygą
        </Button>
        <Button
          onClick={handleInsertBookEvent}
          sx={{ width: "120px", height: "80px", background: primaryColor, color: secondaryColor, fontSize: "1rem", borderRadius: "16px" }}
        >
          Įdėti knygą
        </Button>
      </Box>

      <LockerPINSubmitDialog
        isOpen={pinDialogIsOpen}
        body={isIssue ? "Pasiimkitę knygą. Būtinai uždarykite dureles." : "Įdėkitę knygą. Būtinai uždarykite dureles."}
        handleClose={handlePinDialogClose}
        onSubmitPin={handlePINSubmitEvent}
        isDoorOpened={isDoorOpened}
        onCloseDoor={handleCloseDoor}
      />
    </Box>
  );
}
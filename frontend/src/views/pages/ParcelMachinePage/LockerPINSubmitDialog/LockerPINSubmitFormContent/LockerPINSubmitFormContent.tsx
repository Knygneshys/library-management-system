import { Grid, Box } from "@mui/material";
import FormInputField from "../../../../shared/form-components/FormInputField/FormInputField";
import FormSubmitButton from "../../../../shared/form-components/FormSubmitButton/FormSubmitButton";

export default function LockerPINSubmitFormContent() {
  return (
    <Grid
      container
      spacing={2}
      sx={{ justifyContent: "center", marginTop: "1em", display: "flex", flexDirection: "column", alignItems: "center" }}
    >
      <FormInputField required label={"PIN kodas"} name={"pinCode"} />
      <Box sx={{ mt: 3 }}>
        <FormSubmitButton label={"Atidaryti"} />
      </Box>
    </Grid>
  );
}
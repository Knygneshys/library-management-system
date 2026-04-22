import { Grid } from "@mui/material";
import FormInputField from "../../../../shared/form-components/FormInputField/FormInputField";
import FormSubmitButton from "../../../../shared/form-components/FormSubmitButton/FormSubmitButton";

export default function ParcelLockerCreationFormContent() {
  return (
    <Grid
      container
      spacing={2}
      sx={{ justifyContent: "center", marginTop: "1em" }}
    >
      <FormInputField required label={"Address"} name={"address"} />
      <FormInputField required label={"Locker state"} name={"lockerState"} />
      <FormSubmitButton label={"Create"} />
    </Grid>
  );
}

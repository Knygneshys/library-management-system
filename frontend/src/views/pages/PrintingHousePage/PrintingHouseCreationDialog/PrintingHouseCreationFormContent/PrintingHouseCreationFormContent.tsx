import { Grid } from "@mui/material";
import FormInputField from "../../../../shared/form-components/FormInputField/FormInputField";
import FormSubmitButton from "../../../../shared/form-components/FormSubmitButton/FormSubmitButton";

export default function PrintingHouseCreationFormContent() {
  return (
    <Grid
      container
      spacing={2}
      sx={{ justifyContent: "center", marginTop: "1em" }}
    >
      <FormInputField required label={"Name"} name={"name"} />
      <FormInputField required label={"Address"} name={"address"} />
      <FormInputField required label={"Website"} name={"website"} />
      <FormInputField required label={"Phone"} name={"phone"} />
      <FormSubmitButton label={"Create"} />
    </Grid>
  );
}

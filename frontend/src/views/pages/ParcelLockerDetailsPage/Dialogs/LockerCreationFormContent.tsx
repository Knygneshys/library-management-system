import { Grid } from "@mui/material";
import FormInputField from "../../../shared/form-components/FormInputField/FormInputField";
import FormSubmitButton from "../../../shared/form-components/FormSubmitButton/FormSubmitButton";

export default function LockerCreationFormContent() {
  return (
    <Grid
      container
      spacing={2}
      sx={{ justifyContent: "center", marginTop: "1em" }}
    >
      <FormInputField required label={"Location code"} name={"locationCode"}/>
      <FormInputField required label={"Height"} name={"height"} type="number"/>
      <FormInputField required label={"Width"} name={"width"} type="number"/>
      <FormInputField required label={"Length"} name={"length"} type="number"/>
      <FormSubmitButton label={"Create"} />
    </Grid>
  );
}
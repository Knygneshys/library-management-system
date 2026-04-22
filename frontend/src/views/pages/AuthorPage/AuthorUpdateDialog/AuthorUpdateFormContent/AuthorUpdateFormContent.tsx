import { Grid } from "@mui/material";

import FormInputField from "../../../../shared/form-components/FormInputField/FormInputField";
import FormSubmitButton from "../../../../shared/form-components/FormSubmitButton/FormSubmitButton";

export default function AuthorUpdateFormContent() {
  return (
    <Grid
      container
      spacing={2}
      sx={{ justifyContent: "center", marginTop: "1em" }}
    >
      <FormInputField required label={"Full name"} name={"fullName"} />
      <FormInputField required label={"Nationality"} name={"nationality"} />
      <FormInputField required label={"Biography"} name={"biography"} />
      <FormSubmitButton label={"Update"} />
    </Grid>
  );
}

import {
  Grid,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  FormHelperText,
} from "@mui/material";
import { useFormikContext } from "formik";
import type { LockerUpdateDto } from "../../../../entities/Locker";
import FormInputField from "../../../shared/form-components/FormInputField/FormInputField";
import FormSubmitButton from "../../../shared/form-components/FormSubmitButton/FormSubmitButton";
import { LockerState } from "../../../../entities/LockerState";

export default function LockerUpdateFormContent() {
  const { values, errors, touched, setFieldValue } = useFormikContext<LockerUpdateDto>();

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

      <Grid size={12}>
        <FormControl
          fullWidth
          error={touched.lockerState && Boolean(errors.lockerState)}
        >
          <InputLabel id="locker-state-label">Locker State *</InputLabel>
          <Select
            labelId="locker-state-label"
            value={values.lockerState}
            label="Locker State *"
            onChange={(e) => setFieldValue("lockerState", e.target.value)}
          >
            {Object.entries(LockerState)
              .filter(([key]) => isNaN(Number(key)))
              .map(([key, value]) => (
                <MenuItem key={key} value={value}>
                  {key}
                </MenuItem>
              ))}
          </Select>
          {touched.lockerState && errors.lockerState && (
            <FormHelperText>{String(errors.lockerState)}</FormHelperText>
          )}
        </FormControl>
      </Grid>

      <FormSubmitButton label={"Update"} />
    </Grid>
  );
}

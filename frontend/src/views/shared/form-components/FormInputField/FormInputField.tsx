import { TextField } from "@mui/material";
import { useField } from "formik";

type Props = {
  label: string;
  name: string;
  type?: string;
  required?: boolean;
};

export default function FormInputField({ label, name, ...props }: Props) {
  const [field, meta] = useField(name);

  return (
    <>
      <TextField
        {...field}
        {...props}
        label={label}
        fullWidth
        error={meta.touched && Boolean(meta.error)}
        helperText={meta.touched && meta.error ? meta.error : " "}
      />
    </>
  );
}

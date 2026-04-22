import { Button } from "@mui/material";

interface Props {
  label: string;
  disabled?: boolean;
}

export default function FormSubmitButton({ label }: Props) {
  return (
    <Button type="submit" variant="contained">
      {label}
    </Button>
  );
}

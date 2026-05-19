import { Form, Formik } from "formik";
import LockerPINSubmitFormContent from "../LockerPINSubmitFormContent/LockerPINSubmitFormContent";

interface Props {
  onSubmit: (pin: string) => void;
}

interface PINFormContent {
  pinCode: string;
}

export default function LockerPINSubmitForm({ onSubmit }: Props) {
  const initialValues: PINFormContent = {
    pinCode: "",
  };

  const handleFormSubmit = (values: PINFormContent) => {
    onSubmit(values.pinCode);
  };

  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleFormSubmit}
    >
      <Form>
        <LockerPINSubmitFormContent />
      </Form>
    </Formik>
  );
}
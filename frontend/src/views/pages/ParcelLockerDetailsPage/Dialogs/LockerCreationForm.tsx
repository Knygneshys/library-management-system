import { Form, Formik } from "formik";
import type { Locker } from "../../../../entities/Locker";
import LockerCreationFormContent from "./LockerCreationFormContent";
import { lockerCreationValidation } from "../../../../validation/locker/lockerCreationValidation";

export type LockerCreationFormContent = Omit<
  Locker,
  "id" | "parcelLockerId" | "lockerState"
>;

interface Props {
  onSubmit: (locker: LockerCreationFormContent) => void;
}

export default function LockerCreationForm({ onSubmit }: Props) {
  const initialValues: LockerCreationFormContent = {
    locationCode: "",
    height: 0,
    width: 0,
    length: 0,
  };

  const handleFormSubmit = (values: LockerCreationFormContent) => {
    const locker: LockerCreationFormContent = {
      locationCode: values.locationCode,
      height: values.height,
      width: values.width,
      length: values.length,
    };

    onSubmit(locker);
  };

  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleFormSubmit}
      validationSchema={lockerCreationValidation}
    >
      <Form>
        <LockerCreationFormContent />
      </Form>
    </Formik>
  );
}

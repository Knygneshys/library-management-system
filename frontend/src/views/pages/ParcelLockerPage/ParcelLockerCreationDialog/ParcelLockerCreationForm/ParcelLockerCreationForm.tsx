import { Form, Formik } from "formik";
import type { ParcelLocker } from "../../../../../entities/ParcelLocker";
import { Guid } from "guid-typescript";
import { parcelLockerCreationValidation } from "../../../../../validation/parcelLocker/parcelLockerCreationValidation";
import ParcelLockerCreationFormContent from "../ParcelLockerCreationFormContent/ParcelLockerCreationFormContent";

interface Props {
  onSubmit: (parcelLocker: ParcelLocker) => void;
}

interface ParcelLockerCreationFormContent {
  address: string;
  lockerState: ParcelLockerState;
}

export default function ParcelLockerCreationForm({ onSubmit }: Props) {
  const initialValues: ParcelLockerCreationFormContent = {
    address: "",
    lockerState: ParcelLockerState.Available,
  };

  const handleFormSubmit = (values: ParcelLockerCreationFormContent) => {
    const parcelLocker: ParcelLocker = {
      id: Guid.create(),
      address: values.address,
      lockerState: values.lockerState,
    };

    onSubmit(parcelLocker);
  };

  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleFormSubmit}
      validationSchema={parcelLockerCreationValidation}
    >
      <Form>
        <ParcelLockerCreationForm />
      </Form>
    </Formik>
  );
}

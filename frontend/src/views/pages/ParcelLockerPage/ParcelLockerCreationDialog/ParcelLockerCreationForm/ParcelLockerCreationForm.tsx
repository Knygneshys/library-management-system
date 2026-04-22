import { Form, Formik } from "formik";
import type { ParcelLocker } from "../../../../../entities/ParcelLocker";
import { Guid } from "guid-typescript";
import { parcelLockerCreationValidation } from "../../../../../validation/parcelLocker/parcelLockerCreationValidation";
import ParcelLockerCreationFormContent from "../ParcelLockerCreationFormContent/ParcelLockerCreationFormContent";
import { ParcelLockerState } from "../../../../../entities/ParcelLockerState";

interface Props {
  onSubmit: (parcelLocker: ParcelLocker) => void;
}

interface ParcelLockerCreationFormContent {
  address: string;
}

export default function ParcelLockerCreationForm({ onSubmit }: Props) {
  const initialValues: ParcelLockerCreationFormContent = {
    address: "",
  };

  const handleFormSubmit = (values: ParcelLockerCreationFormContent) => {
    const parcelLocker: ParcelLocker = {
      id: Guid.create(),
      address: values.address,
      lockerState: ParcelLockerState.Active,
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
        <ParcelLockerCreationFormContent />
      </Form>
    </Formik>
  );
}

import { Form, Formik } from "formik";
import type { ParcelLocker } from "../../../../../entities/ParcelLocker";
import { parcelLockerUpdateValidation } from "../../../../../validation/parcelLocker/parcelLockerUpdateValidation";
import ParcelLockerUpdateFormContent from "../ParcelLockerUpdateFormContent/ParcelLockerUpdateFormContent";
import type { ParcelLockerState } from "../../../../../entities/ParcelLockerState";

type Props = {
  parcelLocker: ParcelLocker;
  handleSubmit: (parcelLocker: ParcelLocker) => void;
};

interface ParcelLockerUpdateFormContent {
  address: string;
  lockerState: ParcelLockerState;
}

export default function ParcelLockerUpdateForm({
  parcelLocker,
  handleSubmit,
}: Props) {
  const initialValues: ParcelLockerUpdateFormContent = {
    address: parcelLocker.address,
    lockerState: parcelLocker.lockerState,
  };

  const handleFormSubmit = (values: ParcelLockerUpdateFormContent) => {
    const updatedParcelLocker: ParcelLocker = {
      id: parcelLocker.id,
      address: values.address,
      lockerState: values.lockerState,
    };

    handleSubmit(updatedParcelLocker);
  };
  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleFormSubmit}
      validationSchema={parcelLockerUpdateValidation}
    >
      <Form>
        <ParcelLockerUpdateFormContent />
      </Form>
    </Formik>
  );
}

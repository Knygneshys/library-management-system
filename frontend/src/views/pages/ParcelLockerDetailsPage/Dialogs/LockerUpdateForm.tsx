import { Form, Formik } from "formik";
import type { Locker, LockerUpdateDto } from "../../../../entities/Locker";
import LockerUpdateFormContent from "./LockerUpdateFormContent";
import { lockerUpdateValidation } from "../../../../validation/locker/lockerUpdateValidation";

type Props = {
  locker: Locker;
  handleSubmit: (locker: Locker) => void;
};


export default function LockerUpdateForm({
  locker,
  handleSubmit,
}: Props) {
  const initialValues: LockerUpdateDto = {
    locationCode: locker.locationCode,
    height: locker.height,
    length: locker.length,
    width: locker.width,
    lockerState: locker.lockerState,
  };

  const handleFormSubmit = (values: LockerUpdateDto) => {
    // Senas locker atnaujintas su nauju values
    const updatedLocker: Locker = {...locker, ...values}

    handleSubmit(updatedLocker);
  };
  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleFormSubmit}
      validationSchema={lockerUpdateValidation}
    >
      <Form>
        <LockerUpdateFormContent />
      </Form>
    </Formik>
  );
}

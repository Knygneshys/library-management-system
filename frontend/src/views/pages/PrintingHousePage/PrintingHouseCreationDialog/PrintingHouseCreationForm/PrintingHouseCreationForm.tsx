import { Form, Formik } from "formik";
import type { PrintingHouse } from "../../../../../entities/PrintingHouse";
import { Guid } from "guid-typescript";
import { printingHouseCreationValidation } from "../../../../../validation/printingHouse/printingHouseCreationValidation";
import PrintingHouseCreationFormContent from "../PrintingHouseCreationFormContent/PrintingHouseCreationFormContent";

interface Props {
  onSubmit: (printingHouse: PrintingHouse) => void;
}

interface PrintingHouseCreationFormContent {
  name: string,
  address: string,
  website: string,
  phone: string,
}

export default function PrintingHouseCreationForm({ onSubmit }: Props) {
  const initialValues: PrintingHouseCreationFormContent = {
    name: "",
    address: "",
    website: "",
    phone: ""
  };

  const handleFormSubmit = (values: PrintingHouseCreationFormContent) => {
    const printingHouse: PrintingHouse = {
      id: Guid.create(),
      name: values.name,
      address: values.address,
      website: values.website,
      phone: values.phone
    };

    onSubmit(printingHouse);
  };

  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleFormSubmit}
      validationSchema={printingHouseCreationValidation}
    >
      <Form>
        <PrintingHouseCreationFormContent />
      </Form>
    </Formik>
  );
}

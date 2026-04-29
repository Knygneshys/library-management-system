import { Form, Formik } from "formik";
import type { PrintingHouse } from "../../../../../entities/PrintingHouse";
import { printingHouseUpdateValidation } from "../../../../../validation/printingHouse/printingHouseUpdateValidation";
import PrintingHouseUpdateFormContent from "../PrintingHouseUpdateFormContent/PrintingHouseUpdateFormContent";

type Props = {
  printingHouse: PrintingHouse;
  handleSubmit: (printingHouse: PrintingHouse) => void;
};

interface PrintingHouseUpdateFormContent {
  name: string;
  address: string,
  website: string;
  phone: string;
}

export default function PrintingHouseUpdateForm({ printingHouse, handleSubmit }: Props) {
  const initialValues: PrintingHouseUpdateFormContent = {
    name: printingHouse.name,
    address: printingHouse.address,
    website: printingHouse.website,
    phone: printingHouse.phone,
  };

  const handleFormSubmit = (values: PrintingHouseUpdateFormContent) => {
    const updatedPrintingHouse: PrintingHouse = {
      id: printingHouse.id,
      name: values.name,
      address: values.address,
      website: values.website,
      phone: values.phone,
    };

    handleSubmit(updatedPrintingHouse);
  };
  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleFormSubmit}
      validationSchema={printingHouseUpdateValidation}
    >
      <Form>
        <PrintingHouseUpdateFormContent />
      </Form>
    </Formik>
  );
}

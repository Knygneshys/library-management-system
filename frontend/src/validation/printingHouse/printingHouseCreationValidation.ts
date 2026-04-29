import * as Yup from "yup";

import { requiredError } from "../../utils/errorUtils";

export const printingHouseCreationValidation = Yup.object().shape({
  name: Yup.string().required(requiredError("Name")),
  address: Yup.string().required(requiredError("Address")),
  website: Yup.string().required(requiredError("Website")),
  phone: Yup.string().required(requiredError("Phone")),
});

import * as Yup from "yup";

import { requiredError } from "../../utils/errorUtils";

export const parcelLockerCreationValidation = Yup.object().shape({
  address: Yup.string().required(requiredError("Address")),
});

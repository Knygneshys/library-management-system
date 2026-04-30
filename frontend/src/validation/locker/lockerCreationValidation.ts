import * as Yup from "yup";

import { requiredError } from "../../utils/errorUtils";

export const lockerCreationValidation = Yup.object().shape({
  locationCode: Yup.string().required(requiredError("Location code")),
  height: Yup.number().positive(),
  width: Yup.number().positive(),
  length: Yup.number().positive()
});



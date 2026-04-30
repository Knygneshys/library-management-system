import * as Yup from "yup";

import { requiredError } from "../../utils/errorUtils";

export const lockerUpdateValidation = Yup.object().shape({
  locationCode: Yup.string().required(requiredError("Location code")),
  height: Yup.number().positive().required(requiredError("Height")),
  width: Yup.number().positive().required(requiredError("Width")),
  length: Yup.number().positive().required(requiredError("Length")),
  lockerState: Yup.string().required(requiredError("Locker state")),
});

import * as Yup from "yup";

import { requiredError } from "../../utils/errorUtils";

export const parcelLockerUpdateValidation = Yup.object().shape({
  address: Yup.string().required(requiredError("Address")),
  lockerState: Yup.string().required(requiredError("Locker State")),
});

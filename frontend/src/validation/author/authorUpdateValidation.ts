import * as Yup from "yup";

import { requiredError } from "../../utils/errorUtils";

export const authorUpdateValidation = Yup.object().shape({
  fullName: Yup.string().required(requiredError("Full Name")),
  nationality: Yup.string().required(requiredError("Nationality")),
  biography: Yup.string().required(requiredError("Biography")),
});

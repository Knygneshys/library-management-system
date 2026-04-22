import { Form, Formik } from "formik";
import type { Author } from "../../../../../entities/Author";
import { Guid } from "guid-typescript";
import { authorCreationValidation } from "../../../../../validation/author/authorCreationValidation";
import AuthorCreationFormContent from "../AuthorCreationFormContent/AuthorCreationFormContent";

interface Props {
  onSubmit: (author: Author) => void;
}

interface AuthorCreationFormContent {
  fullName: string;
  nationality: string;
  biography: string;
}

export default function AuthorCreationForm({ onSubmit }: Props) {
  const initialValues: AuthorCreationFormContent = {
    fullName: "",
    nationality: "",
    biography: "",
  };

  const handleFormSubmit = (values: AuthorCreationFormContent) => {
    const author: Author = {
      id: Guid.create(),
      fullName: values.fullName,
      nationality: values.nationality,
      biography: values.biography,
    };

    onSubmit(author);
  };

  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleFormSubmit}
      validationSchema={authorCreationValidation}
    >
      <Form>
        <AuthorCreationFormContent />
      </Form>
    </Formik>
  );
}

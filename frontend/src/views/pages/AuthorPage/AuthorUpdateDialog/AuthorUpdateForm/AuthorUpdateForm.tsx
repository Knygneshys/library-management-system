import { Form, Formik } from "formik";
import type { Author } from "../../../../../entities/Author";
import { authorUpdateValidation } from "../../../../../validation/author/authorUpdateValidation";
import AuthorUpdateFormContent from "../AuthorUpdateFormContent/AuthorUpdateFormContent";

type Props = {
  author: Author;
  handleSubmit: (author: Author) => void;
};

interface AuthorUpdateFormContent {
  fullName: string;
  nationality: string;
  biography: string;
}

export default function AuthorUpdateForm({ author, handleSubmit }: Props) {
  const initialValues: AuthorUpdateFormContent = {
    fullName: author.fullName,
    nationality: author.nationality,
    biography: author.biography,
  };

  const handleFormSubmit = (values: AuthorUpdateFormContent) => {
    const updatedAuthor: Author = {
      id: author.id,
      fullName: values.fullName,
      nationality: values.nationality,
      biography: values.biography,
    };

    handleSubmit(updatedAuthor);
  };
  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleFormSubmit}
      validationSchema={authorUpdateValidation}
    >
      <Form>
        <AuthorUpdateFormContent />
      </Form>
    </Formik>
  );
}

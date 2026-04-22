import { Box, Button } from "@mui/material";
import {
  createAuthor,
  deleteAuthor,
  getAllAuthors,
  updateAuthor,
} from "../../../external-api-clients/clients/externalAuthorApiClient";
import { useEffect, useState } from "react";
import type { Author } from "../../../entities/Author";
import AuthorTable from "./AuthorTable/AuthorTable";
import {
  primaryColor,
  secondaryColor,
} from "../../../constants/colorConstants";
import AuthorCreationDialog from "./AuthorCreationDialog/AuthorCreationDialog";
import toast from "react-hot-toast";
import {
  handleErrorToast,
  successfullCreateMessage,
  successfullDeleteMessage,
  successfullUpdateMessage,
} from "../../../utils/toastUtils";
import AuthorUpdateDialog from "./AuthorUpdateDialog/AuthorUpdateDialog";

export default function AuthorPage() {
  const [authors, setAuthors] = useState<Author[]>([]);

  const [creationDialogIsOpen, setCreationDialogIsOpen] =
    useState<boolean>(false);
  const [updateDialogIsOpen, setUpdateDialogIsOpen] = useState<boolean>(false);
  const [authorThatIsBeingUpdated, setAuthorThatIsBeingUpdated] =
    useState<Author | null>(null);

  useEffect(() => {
    const fetchAuthors = async () => {
      const data = await getAllAuthors();

      setAuthors(data);
    };

    fetchAuthors();
  }, []);

  const handleCreationDialogOpen = () => {
    setCreationDialogIsOpen(true);
  };

  const handleCreationDialogClose = () => {
    setCreationDialogIsOpen(false);
  };

  const handleCreationFormSubmit = async (author: Author) => {
    try {
      const authors = await createAuthor(author);

      setAuthors(authors);
      toast.success(successfullCreateMessage("Author"));
    } catch (error) {
      handleErrorToast(error);
    }

    handleCreationDialogClose();
  };

  const handleUpdateDialogOpen = (author: Author) => {
    setAuthorThatIsBeingUpdated(author);
    setUpdateDialogIsOpen(true);
  };

  const handleUpdateDialogClose = () => {
    setAuthorThatIsBeingUpdated(null);
    setUpdateDialogIsOpen(false);
  };

  const handleUpdateFormSubmit = async (author: Author) => {
    try {
      const authors = await updateAuthor(author);

      setAuthors(authors);
      toast.success(successfullUpdateMessage("Author"));
    } catch (error) {
      handleErrorToast(error);
    }

    handleUpdateDialogClose();
  };

  const handleAuthorDelete = async (author: Author) => {
    try {
      const authors = await deleteAuthor(author);

      setAuthors(authors);
      toast.success(successfullDeleteMessage("Author"));
    } catch (error) {
      handleErrorToast(error);
    }
  };

  return (
    <Box>
      <Box sx={{ float: "right" }}>
        <Button
          onClick={handleCreationDialogOpen}
          sx={{
            margin: "20px",
            marginTop: "70px",
            background: primaryColor,
            color: secondaryColor,
            width: "200px",
          }}
        >
          Create author
        </Button>
      </Box>
      <AuthorTable
        authors={authors}
        onUpdateButtonClick={handleUpdateDialogOpen}
        onDeleteButtonClick={handleAuthorDelete}
      />
      <AuthorCreationDialog
        isOpen={creationDialogIsOpen}
        handleClose={handleCreationDialogClose}
        onSubmit={handleCreationFormSubmit}
      />
      {updateDialogIsOpen && authorThatIsBeingUpdated && (
        <AuthorUpdateDialog
          isOpen={updateDialogIsOpen}
          author={authorThatIsBeingUpdated}
          handleClose={handleUpdateDialogClose}
          handleSubmit={handleUpdateFormSubmit}
        />
      )}
    </Box>
  );
}

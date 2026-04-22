import { Box, Button } from "@mui/material";
import {
  createAuthor,
  getAllAuthors,
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
} from "../../../utils/toastUtils";

export default function AuthorPage() {
  const [authors, setAuthors] = useState<Author[]>([]);

  const [creationDialogIsOpen, setCreationDialogIsOpen] =
    useState<boolean>(false);

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

  useEffect(() => {
    const fetchAuthors = async () => {
      const data = await getAllAuthors();

      setAuthors(data);
    };

    fetchAuthors();
  }, []);

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
      <AuthorTable authors={authors} />
      <AuthorCreationDialog
        isOpen={creationDialogIsOpen}
        handleClose={handleCreationDialogClose}
        onSubmit={handleCreationFormSubmit}
      />
    </Box>
  );
}

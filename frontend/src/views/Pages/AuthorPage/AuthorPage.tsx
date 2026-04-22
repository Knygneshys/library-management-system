import { Box, Button } from "@mui/material";
import { getAllAuthors } from "../../../external-api-clients/clients/externalAuthorApiClient";
import { useEffect, useState } from "react";
import type { Author } from "../../../entities/Author";
import AuthorTable from "./AuthorTable/AuthorTable";
import {
  primaryColor,
  secondaryColor,
} from "../../../constants/colorConstants";

export default function AuthorPage() {
  const [authors, setAuthors] = useState<Author[]>([]);

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
          sx={{
            margin: "20px",
            background: primaryColor,
            color: secondaryColor,
            width: "200px",
          }}
        >
          Create author
        </Button>
      </Box>
      <AuthorTable authors={authors} />
    </Box>
  );
}

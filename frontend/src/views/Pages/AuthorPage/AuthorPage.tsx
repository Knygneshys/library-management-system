import { Box } from "@mui/material";
import { getAllAuthors } from "../../../externalApiClients/clients/externalAuthorApiClient";
import { useEffect, useState } from "react";
import type { Author } from "../../../entities/Author";
import AuthorTable from "./AuthorTable/AuthorTable";

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
      <AuthorTable authors={authors} />
    </Box>
  );
}

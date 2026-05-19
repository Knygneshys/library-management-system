import { Box } from "@mui/material";
import { getAllBooks } from "../../../external-api-clients/clients/externalBookApiClient";
import { useEffect, useState } from "react";
import type { Book } from "../../../entities/Book";
import BookTable from "./BookTable/BookTable";
import BookDetailsModal from "./BookDetailsModal/BookDetailsModal";

export default function BookPage() {
  const [books, setBooks] = useState<Book[]>([]);
  const [detailsModalIsOpen, setDetailsModalIsOpen] = useState<boolean>(false);
  const [bookBeingViewed, setBookBeingViewed] = useState<Book | null>(null);

  useEffect(() => {
    const fetchBooks = async () => {
      const data = await getAllBooks();
      setBooks(data);
    };

    fetchBooks();
  }, []);

  const handleDetailsDialogOpen = (book: Book) => {
    setBookBeingViewed(book);
    setDetailsModalIsOpen(true);
  };

  const handleDetailsDialogClose = () => {
    setBookBeingViewed(null);
    setDetailsModalIsOpen(false);
  };

  return (
    <Box>
      <BookTable books={books} onDetailsButtonClick={handleDetailsDialogOpen} />
      {detailsModalIsOpen && bookBeingViewed && (
        <BookDetailsModal
          isOpen={detailsModalIsOpen}
          book={bookBeingViewed}
          handleClose={handleDetailsDialogClose}
        />
      )}
    </Box>
  );
}

import {
  Button,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import type { Book } from "../../../../entities/Book";
import { tableHeaderFontSize } from "../../../../constants/fontSizeConstants";

interface Props {
  books: Book[];
  onDetailsButtonClick: (book: Book) => void;
}

export default function BookTable({ books, onDetailsButtonClick }: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "30%" }}>
            Title
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "20%" }}>
            Author
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "20%" }}>
            ISBN
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "30%" }}>
            Free Copies
          </TableCell>
          <TableCell>Actions</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {books.map((book, index) => (
          <TableRow key={index}>
            <TableCell>{book.title}</TableCell>
            <TableCell>{book.author}</TableCell>
            <TableCell>{book.isbn}</TableCell>
            <TableCell>{book.freeCopyCount}</TableCell>
            <TableCell>
              <Stack direction={"row"} spacing={2}>
                <Button onClick={() => onDetailsButtonClick(book)}>
                  View Details
                </Button>
              </Stack>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import type { Author } from "../../../../entities/Author";
import { tableHeaderFontSize } from "../../../../constants/fontSizeConstants";

interface Props {
  authors: Author[];
  onUpdateButtonClick: (author: Author) => void;
}

export default function AuthorTable({ authors, onUpdateButtonClick }: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "70%" }}>
            Full name
          </TableCell>
          <TableCell>Actions</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {authors.map((author, index) => (
          <TableRow key={index}>
            <TableCell>{author.fullName}</TableCell>
            <TableCell>
              <Button onClick={() => onUpdateButtonClick(author)}>
                Update
              </Button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

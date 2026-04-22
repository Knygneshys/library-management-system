import {
  Button,
  Stack,
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
  onDeleteButtonClick: (author: Author) => void;
}

export default function AuthorTable({
  authors,
  onUpdateButtonClick,
  onDeleteButtonClick,
}: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "10%" }}>
            Full name
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "10%" }}>
            Nationality
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "70%" }}>
            Biography
          </TableCell>
          <TableCell>Actions</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {authors.map((author, index) => (
          <TableRow key={index}>
            <TableCell>{author.fullName}</TableCell>
            <TableCell>{author.nationality}</TableCell>
            <TableCell>{author.biography}</TableCell>
            <TableCell>
              <Stack direction={"row"} spacing={2}>
                <Button onClick={() => onUpdateButtonClick(author)}>
                  Update
                </Button>
                <Button onClick={() => onDeleteButtonClick(author)}>
                  Delete
                </Button>
              </Stack>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

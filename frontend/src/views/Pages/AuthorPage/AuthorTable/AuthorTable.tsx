import {
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
}

export default function AuthorTable({ authors }: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell sx={{ fontSize: tableHeaderFontSize }}>
            Full name
          </TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {authors.map((author, index) => (
          <TableRow key={index}>
            <TableCell>{author.fullName}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

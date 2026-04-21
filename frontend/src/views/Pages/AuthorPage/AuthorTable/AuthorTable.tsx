import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import type { Author } from "../../../../entities/Author";

interface Props {
  authors: Author[];
}

export default function AuthorTable({ authors }: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Full name</TableCell>
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

import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import type { Task } from "../../../../entities/Task";
import { tableHeaderFontSize } from "../../../../constants/fontSizeConstants";

interface Props {
  tasks: Task[];
}

export default function TaskTable({ tasks }: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "25%" }}>
            Task type
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "20%" }}>
            Librarian PIN
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "20%" }}>
            Reader PIN
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "20%" }}>
            Locker
          </TableCell>
        </TableRow>
      </TableHead>

      <TableBody>
        {tasks.map((task) => (
          <TableRow key={task.id.toString()}>
            <TableCell>{task.type}</TableCell>
            <TableCell>{task.pinCodeLibrarian ?? "—"}</TableCell>
            <TableCell>{task.pinCodeReader ?? "—"}</TableCell>
            <TableCell>{task.lockerLocationCode ?? "—"}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

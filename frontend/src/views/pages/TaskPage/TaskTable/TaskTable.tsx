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
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "30%" }}>
            Task type
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "20%" }}>
            PIN code
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "20%" }}>
            Locker
          </TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {tasks.map((task, index) => (
          <TableRow key={index}>
            <TableCell>{task.type}</TableCell>
            <TableCell>{task.pin ?? "—"}</TableCell>
            <TableCell>{task.lockerLocationCode ?? "—"}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

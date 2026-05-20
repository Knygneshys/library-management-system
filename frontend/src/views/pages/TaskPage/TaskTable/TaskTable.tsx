// pages/TasksPage/TaskTable/TaskTable.tsx
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Button,
} from "@mui/material";
import type { Task } from "../../../../entities/Task";
import { tableHeaderFontSize } from "../../../../constants/fontSizeConstants";

interface Props {
  tasks: Task[];
  onStart: (task: Task) => void;
}

export default function TaskTable({ tasks, onStart }: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell sx={{ fontSize: tableHeaderFontSize }}>
            Task type
          </TableCell>
          <TableCell />
        </TableRow>
      </TableHead>
      <TableBody>
        {tasks.map((task) => (
          <TableRow key={task.id.toString()}>
            <TableCell>{task.type}</TableCell>
            <TableCell align="right">
              <Button
                variant="contained"
                size="small"
                onClick={() => onStart(task)}
              >
                Start
              </Button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

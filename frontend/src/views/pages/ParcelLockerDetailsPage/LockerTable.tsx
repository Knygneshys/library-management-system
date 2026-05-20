import {
  Button,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import type { Locker } from "../../../entities/Locker";
import { tableHeaderFontSize } from "../../../constants/fontSizeConstants";
import { LockerState } from "../../../entities/LockerState";

interface Props {
  lockers: Locker[];
  onUpdateButtonClick: (locker: Locker) => void;
  onDeleteButtonClick: (locker: Locker) => void;
}

export function LockerTable({
  lockers,
  onUpdateButtonClick,
  onDeleteButtonClick,
}: Props) {
  return (
     <Table>
      <TableHead>
        <TableRow>
          <TableCell sx={{ fontSize: tableHeaderFontSize}}>
            Location Code
          </TableCell>

          <TableCell sx={{ fontSize: tableHeaderFontSize}}>
            Height
          </TableCell>

          <TableCell sx={{ fontSize: tableHeaderFontSize}}>
            Width
          </TableCell>

          <TableCell sx={{ fontSize: tableHeaderFontSize}}>
            Length
          </TableCell>

          <TableCell sx={{ fontSize: tableHeaderFontSize}}>
            State
          </TableCell>

          <TableCell sx={{ width: "20%" }}>
            Actions
          </TableCell>
        </TableRow>
      </TableHead>

      <TableBody>
        {lockers.map((locker, index) => (
          <TableRow key={index}>
            <TableCell>{locker.locationCode}</TableCell>
            <TableCell>{locker.height}</TableCell>
            <TableCell>{locker.width}</TableCell>
            <TableCell>{locker.length}</TableCell>
            <TableCell>{LockerState[locker.lockerState]}</TableCell>

            <TableCell>
              <Stack direction="row" spacing={2}>
                <Button onClick={() => onUpdateButtonClick(locker)}>
                  Update
                </Button>

                <Button onClick={() => onDeleteButtonClick(locker)}>
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

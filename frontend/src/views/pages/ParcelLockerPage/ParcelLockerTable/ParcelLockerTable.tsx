import {
  Button,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import type { ParcelLocker } from "../../../../entities/ParcelLocker";
import { tableHeaderFontSize } from "../../../../constants/fontSizeConstants";
import { ParcelLockerState } from "../../../../entities/ParcelLockerState";

interface Props {
  parcelLockers: ParcelLocker[];
  onUpdateButtonClick: (parcelLocker: ParcelLocker) => void;
  onDeleteButtonClick: (parcelLocker: ParcelLocker) => void;
}

export default function ParcelLockerTable({
  parcelLockers,
  onUpdateButtonClick,
  onDeleteButtonClick,
}: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "10%" }}>
            Address
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "70%" }}>
            Locker state
          </TableCell>
          <TableCell>Actions</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {parcelLockers.map((parcelLocker, index) => (
          <TableRow key={index}>
            <TableCell>{parcelLocker.address}</TableCell>
            <TableCell>{ParcelLockerState[parcelLocker.lockerState]}</TableCell>
            <TableCell>
              <Stack direction={"row"} spacing={2}>
                <Button onClick={() => onUpdateButtonClick(parcelLocker)}>
                  Update
                </Button>
                <Button onClick={() => onDeleteButtonClick(parcelLocker)}>
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

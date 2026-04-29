import {
  Button,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import type { PrintingHouse } from "../../../../entities/PrintingHouse";
import { tableHeaderFontSize } from "../../../../constants/fontSizeConstants";

interface Props {
  printingHouses: PrintingHouse[];
  onUpdateButtonClick: (printingHouse: PrintingHouse) => void;
  onDeleteButtonClick: (printingHouse: PrintingHouse) => void;
}

export default function PrintingHouseTable({
  printingHouses,
  onUpdateButtonClick,
  onDeleteButtonClick,
}: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "10%" }}>
            Name
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "10%" }}>
            Address
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "10%" }}>
            Website
          </TableCell>
          <TableCell sx={{ fontSize: tableHeaderFontSize, width: "10%" }}>
            Phone
          </TableCell>
          <TableCell>Actions</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {printingHouses.map((printingHouse, index) => (
          <TableRow key={index}>
            <TableCell>{printingHouse.name}</TableCell>
            <TableCell>{printingHouse.address}</TableCell>
            <TableCell>{printingHouse.website}</TableCell>
            <TableCell>{printingHouse.phone}</TableCell>
            <TableCell>
              <Stack direction={"row"} spacing={2}>
                <Button onClick={() => onUpdateButtonClick(printingHouse)}>
                  Update
                </Button>
                <Button onClick={() => onDeleteButtonClick(printingHouse)}>
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

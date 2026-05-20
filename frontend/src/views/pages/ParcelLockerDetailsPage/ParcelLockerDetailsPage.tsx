import { useParams } from "react-router";

import {
  Container,
  Card,
  CardContent,
  Typography,
  Stack,
  Chip,
  Divider,
} from "@mui/material";
import { ParcelLockerState } from "../../../entities/ParcelLockerState";
import LockerList from "./LockerList";
import { useParcelLockers } from "../../../hooks/useParcelLockers";

const ParcelLockerPage = () => {
  const { id } = useParams();
  const { parcelLockers } = useParcelLockers();

  const parcelLocker = parcelLockers.find((pl) => pl.id.toString() === id);

  if (!parcelLocker) {
    return (
      <Container sx={{ mt: 4 }}>
        <Typography variant="h5">Parcel locker not found</Typography>
      </Container>
    );
  }

  return (
    <Container maxWidth="lg" sx={{ mt: 6 }}>
      <Card elevation={3} sx={{ borderRadius: 3 }}>
        <CardContent>
          <Stack spacing={2} useFlexGap={true}>
            <Typography variant="h4" sx={{ fontWeight: 600 }}>
              Parcel Locker
            </Typography>

            <Divider />

            <Stack
              spacing={2}
              direction={"row"}
              useFlexGap={true}
              sx={{ justifyContent: "space-between", alignItems: "flex-end" }}
            >
              <div>
                <Typography variant="subtitle2" color="text.secondary">
                  Address
                </Typography>
                <Typography variant="body1">{parcelLocker.address}</Typography>
              </div>
              <Chip label={ParcelLockerState[parcelLocker.lockerState]} />
            </Stack>

            <Divider />

            <LockerList parcelLocker={parcelLocker} />
          </Stack>
        </CardContent>
      </Card>
    </Container>
  );
};

export default ParcelLockerPage;

import { Outlet } from "react-router";
import { useEffect, useState } from "react";
import type { ParcelLocker } from "../../entities/ParcelLocker";
import { getAllParcelLockers } from "../../external-api-clients/clients/externalParcelLockerApiClient";

export type ParcelLockerContextType = {
  parcelLockers: ParcelLocker[];
  setParcelLockers: React.Dispatch<React.SetStateAction<ParcelLocker[]>>;
};

export const ParcelLockerLayout = () => {
  const [parcelLockers, setParcelLockers] = useState<ParcelLocker[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      setParcelLockers(await getAllParcelLockers());
    };

    fetchData();
  }, []);

  return (
    <Outlet
      context={
        { parcelLockers, setParcelLockers } satisfies ParcelLockerContextType
      }
    />
  );
};

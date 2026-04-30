import { useOutletContext } from "react-router";
import type { ParcelLockerContextType } from "../views/layout/ParcelLockerLayout";

export const useParcelLockers = () =>
  useOutletContext<ParcelLockerContextType>();

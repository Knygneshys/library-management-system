import { sidebarIconSize } from "../constants/fontSizeConstants";
import AuthorPage from "./pages/AuthorPage/AuthorPage";
import PrintingHousePage from "./pages/PrintingHousePage/PrintingHousePage";
import HomePage from "./pages/HomePage/HomePage";
import ParcelLockerPage from "./pages/ParcelLockerPage/ParcelLockerPage";
import HistoryEduIcon from "@mui/icons-material/HistoryEdu";
import HomeIcon from "@mui/icons-material/Home";
import AllInboxOutlinedIcon from "@mui/icons-material/AllInboxOutlined";

export const routes = [
  {
    route: "/",
    component: HomePage,
    name: "Home Page",
    icon: <HomeIcon sx={{ fontSize: sidebarIconSize }} />,
  },
  {
    route: "/authors",
    component: AuthorPage,
    name: "Authors",
    icon: <HistoryEduIcon sx={{ fontSize: sidebarIconSize }} />,
  },
  {
    route: "/parcelLockers",
    component: ParcelLockerPage,
    name: "Parcel Lockers",
    icon: <AllInboxOutlinedIcon sx={{ fontSize: sidebarIconSize }} />,
  },
  {
    route: "/printingHouses",
    component: PrintingHousePage,
    name: "Printing Houses",
    icon: <HomeIcon sx={{ fontSize: sidebarIconSize }} />,
  }
];

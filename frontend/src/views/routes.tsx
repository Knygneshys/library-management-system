import { sidebarIconSize } from "../constants/fontSizeConstants";
import AuthorPage from "./Pages/AuthorPage/AuthorPage";
import HomePage from "./Pages/HomePage/HomePage";
import HistoryEduIcon from "@mui/icons-material/HistoryEdu";
import HomeIcon from "@mui/icons-material/Home";

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
];

import { sidebarIconSize } from "../constants/fontSizeConstants";
import AuthorPage from "./pages/AuthorPage/AuthorPage";
import PrintingHousePage from "./pages/PrintingHousePage/PrintingHousePage";
import HomePage from "./pages/HomePage/HomePage";
import HistoryEduIcon from "@mui/icons-material/HistoryEdu";
import HomeIcon from "@mui/icons-material/Home";
import BookPage from "./pages/BookPage/BookPage";
import BookIcon from "@mui/icons-material/Book";
<<<<<<< HEAD
import TaskPage from "./pages/TaskPage/TaskPage";
import TaskIcon from "@mui/icons-material/Task";
=======
import ParcelMachinePage from "./pages/ParcelMachinePage/ParcelMachinePage";
>>>>>>> 036d491a749b0fda6677b5c4ccd177652cfdaae5

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
  // {
  //   route: "/parcelLockers",
  //   component: ParcelLockerPage,
  //   name: "Parcel Lockers",
  //   icon: <AllInboxOutlinedIcon sx={{ fontSize: sidebarIconSize }} />
  // },
  {
    route: "/printingHouses",
    component: PrintingHousePage,
    name: "Printing Houses",
    icon: <HomeIcon sx={{ fontSize: sidebarIconSize }} />,
  },
  {
    route: "/books",
    component: BookPage,
    name: "Books",
    icon: <BookIcon sx={{ fontSize: sidebarIconSize }} />,
  },
  {
<<<<<<< HEAD
    route: "/tasks",
    component: TaskPage,
    name: "Tasks",
    icon: <TaskIcon sx={{ fontSize: sidebarIconSize }} />,
=======
    route: "/parcelmachine",
    component: ParcelMachinePage,
    name: "Parcel Machine",
    icon: <BookIcon sx={{ fontSize: sidebarIconSize }} />,
>>>>>>> 036d491a749b0fda6677b5c4ccd177652cfdaae5
  },
];

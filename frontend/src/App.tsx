import Sidebar from "./views/Sidebar/Sidebar";
import { Box } from "@mui/material";
import { Route, Routes } from "react-router";
import { secondaryColor } from "./constants/colorConstants";
import { routes } from "./views/routes";
import "./style/global.css";
import ParcelLockerPage from "./views/pages/ParcelLockerDetailsPage/ParcelLockerDetailsPage";
import ParcelLockerListPage from "./views/pages/ParcelLockerPage/ParcelLockerListPage";
import { ParcelLockerLayout } from "./views/layout/ParcelLockerLayout";

export default function App() {
  return (
    <>
      <Box sx={{ display: "flex", height: "100vh" }}>
        <Sidebar />
        <Box sx={{ backgroundColor: secondaryColor, width: "100%" }}>
          <Routes>
            {routes.map((route) => (
              <Route
                key={route.name}
                path={route.route}
                element={<route.component />}
              />
            ))}
            <Route path="/parcelLockers" element={<ParcelLockerLayout />}>
              <Route index element={<ParcelLockerListPage />} />
              <Route path=":id" element={<ParcelLockerPage />} />
            </Route>
          </Routes>
        </Box>
      </Box>
    </>
  );
}

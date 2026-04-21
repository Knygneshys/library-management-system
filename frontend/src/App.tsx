import Sidebar from "./views/Sidebar/Sidebar";
import { Box } from "@mui/material";
import { Route, Routes } from "react-router";
import { secondaryColor } from "./constants/colorConstants";
import { routes } from "./views/routes";

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
                element={route.component()}
              />
            ))}
          </Routes>
        </Box>
      </Box>
    </>
  );
}

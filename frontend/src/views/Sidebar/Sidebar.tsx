import { Box, Stack, Typography } from "@mui/material";
import { primaryColor } from "../../constants/colorConstants";
import { routes } from "../routes";
import { Link } from "react-router";

export default function Sidebar() {
  const fontSize = 22;
  const leftPadding = "15%";

  return (
    <Box
      sx={{
        backgroundColor: primaryColor,
        height: "100vh",
        width: "20%",
        display: "flex",
        flexDirection: "column",
      }}
    >
      <Box>
        <Box sx={{ paddingLeft: leftPadding }}>
          <Stack spacing={3} sx={{ marginTop: "20%" }}>
            {routes.map((route) => (
              <Link
                key={route.name}
                to={route.route}
                style={{ textDecoration: "none", color: "black" }}
              >
                <Stack direction="row" sx={{ alignItems: "center", gap: 1 }}>
                  {route.icon}
                  <Typography sx={{ fontSize: fontSize }}>
                    {route.name}
                  </Typography>
                </Stack>
              </Link>
            ))}
          </Stack>
        </Box>
      </Box>
    </Box>
  );
}

import { createTheme } from "@mui/material/styles";
import { primaryColor, secondaryColor } from "../constants/colorConstants";

export const libraryTheme = createTheme({
  typography: {
    fontSize: 18,
  },
  components: {
    MuiTableCell: {
      styleOverrides: {
        root: {
          borderColor: "rgba(0, 0, 0, 45%)",
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          width: "120px",
          color: primaryColor,
          backgroundColor: secondaryColor,
        },
      },
    },
  },
});

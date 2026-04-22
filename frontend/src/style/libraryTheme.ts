import { createTheme } from "@mui/material/styles";
import {
  entityTableBackgroundColor,
  primaryColor,
  secondaryColor,
} from "../constants/colorConstants";

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
    MuiTable: {
      styleOverrides: {
        root: {
          backgroundColor: entityTableBackgroundColor,
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

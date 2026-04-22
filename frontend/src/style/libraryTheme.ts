import { createTheme } from "@mui/material/styles";

export const libraryTheme = createTheme({
  components: {
    MuiTableCell: {
      styleOverrides: {
        root: {
          borderColor: "rgba(0, 0, 0, 45%)",
        },
      },
    },
  },
});

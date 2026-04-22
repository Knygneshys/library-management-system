import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router";
import { ThemeProvider } from "@mui/material/styles";
import App from "./App.tsx";
import { libraryTheme } from "./style/libraryTheme";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ThemeProvider theme={libraryTheme}>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </ThemeProvider>
  </StrictMode>,
);

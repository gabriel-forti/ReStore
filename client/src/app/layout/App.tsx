import { Container, CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import Header from "./Header";
import { useState } from "react";
import { Outlet } from "react-router-dom";

function App() {   
    const [darkMode, setDarkMode] = useState<boolean>(false);
    const paletteType = darkMode ? 'dark' : 'light';
    const theme = createTheme({
        palette: {
            mode: paletteType,
            background: {
                default: paletteType === 'light' ? '#EAEAEA' : '#121212'
            }
        }
    });

    // updates darkMode value, trigger component re-render
    function handleThemeChange() {
        setDarkMode(!darkMode)
    }

    return (
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
            <Container>
                <Outlet />
            </Container>
        </ThemeProvider>
    );
}

export default App;

import {ThemeProvider} from "@emotion/react";
import {createTheme, CssBaseline} from "@mui/material";
import {QueryClientProvider, QueryClient} from "react-query";
import ApplicationRouter from "./Router/ApplicationRouter";

const theme = createTheme({
    typography: {
        fontFamily: 'Oxygen'
    }
});
const queryClient = new QueryClient()

function App() {
    return (
        <ThemeProvider theme={theme}>
            <QueryClientProvider client={queryClient}>
                <CssBaseline/>
                <ApplicationRouter/>
            </QueryClientProvider>
        </ThemeProvider>
    )
}

export default App

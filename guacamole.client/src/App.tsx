import {ThemeProvider, StyledEngineProvider, CssBaseline} from "@mui/material";
import { BrowserRouter, Route, Switch} from "react-router-dom";
import theme from "./theme";
import WeatherPage from "./pages/weather.tsx";
import {Fragment, Suspense} from "react";

function App() {
    return (
        <BrowserRouter>
            <StyledEngineProvider injectFirst>
                <ThemeProvider theme={theme}>
                    <CssBaseline />
                    <Suspense fallback={<Fragment />}>
                        <Switch>
                            <Route path="/">
                                <WeatherPage />
                            </Route>
                        </Switch>
                    </Suspense>
                </ThemeProvider>
            </StyledEngineProvider>
        </BrowserRouter>
    );
}

export default App;
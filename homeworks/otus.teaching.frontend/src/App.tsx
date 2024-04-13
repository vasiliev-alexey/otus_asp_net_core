
import React from "react";
import './App.scss';
import {BrowserRouter} from "react-router-dom";
import {AppRouter} from "./components/appRouter/AppRouter";
import Header from "./components/header/Header";
import {store} from "./store/store";
import {Provider} from "react-redux";
import './App.scss';

export default function App() {
    return (
         <Provider store={store}>
            <BrowserRouter>
                <Header/>
                <AppRouter/>
            </BrowserRouter>
        </Provider>
    );
}

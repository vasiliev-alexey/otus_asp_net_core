import LoginForm from "./components/Auth/login/LoginForm";
import React from "react";
import {Route, Routes} from "react-router";
import {BrowserRouter} from "react-router-dom";
import {AppRouter} from "./components/appRouter/AppRouter";
import Header from "./components/header/Header";

export default function App() {
  return (
    // <main>
    //   <p>Otus Simple  Frontend</p>
    //   <Logout />
    // </main>
    <BrowserRouter>
        <Header/>
        <AppRouter />

    </BrowserRouter>

  );
}

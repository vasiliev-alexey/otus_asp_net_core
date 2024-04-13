import {BrowserRouter} from "react-router-dom";
import {Navigate, Route, Routes} from "react-router";
import LoginForm from "../Auth/login/LoginForm";
import React from "react";
import {RegisterForm} from "../Auth/register/RegisterForm";
import {HomePage} from "../home/HomePage";
import Logout from "../Auth/logout/Logout";


export const AppRouter: React.FC = () => {
    return (

        <Routes>
            <Route path="*" element={
                <ProtectedRoute>
                    <HomePage/>
                </ProtectedRoute>
            }></Route>
            <Route path="/login" element={<LoginForm/>}/>
            <Route path="/register" element={<RegisterForm/>}/>
            <Route path="/logout" element={<Logout/>}/>
        </Routes>

    );
};


// @ts-ignore
const ProtectedRoute = ({children}) => {
    const user = true;
    if (!user) {
        // user is not authenticated
        return <Navigate to="/login"/>;
    }
    return children;
};
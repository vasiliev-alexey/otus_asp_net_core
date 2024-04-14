import React from "react";
import {useAppSelector} from "@hooks/reducs";
import {NavLink} from "react-router-dom";


export const HomePage = () => {

    const isAuth = useAppSelector((state) => state.auth.isAuthenticated)

    return (
        <>
            {isAuth && <NavLink to="/admin">Admin</NavLink>}
            <div>
                Welcome to Simple Site
            </div>
        </>
    );
}
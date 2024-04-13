import React, {useMemo, useState} from 'react';
import authService from "../../../api/authService";
import "./Logout.scss";
import {useNavigate} from "react-router";
import {NavLink} from "react-router-dom";
import {useAppDispatch} from "../../../hooks/reducs";
import {logout} from "../../../store/authSlice";

export const Logout = () => {
    const dispatch = useAppDispatch()
    dispatch(logout())
    return (
        <div className="container">

            <div className="input-group">
                <h2>Bue!</h2>

                <nav id="sidebar">
                    <NavLink className="nav-link" to="/">Home</NavLink>
                </nav>

            </div>
            )
        </div>
    );
};


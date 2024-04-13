import React, {useMemo, useState} from 'react';
import authService from "../../../api/authService";
import "./Logout.scss";
import {useNavigate} from "react-router";
import {NavLink} from "react-router-dom";

const Logout = () => {

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

export default Logout;
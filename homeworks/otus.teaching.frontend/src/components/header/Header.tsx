import React from 'react';
import "./Header.scss";
import login from '@assets/login.svg';
import logout from '@assets/logout.svg';
import register from '@assets/register.svg';
import {useNavigate} from "react-router";
import {useAppSelector} from "@hooks/reducs";

const Header = () => {


    const navigate = useNavigate();
    const isAuth = useAppSelector((state) => state.auth.isAuthenticated)
    return (
        <header className="siteHeader">

            <div className="sign" data-testid="welcome-label">
                <span className="fast-flicker">Otus </span>
                <span className="flicker"> Simple </span>
                <span className="fast-flicker"> Frontend</span>
            </div>


            <div className="Footer-Toolbar">

          <span>

          </span>


            </div>


            {isAuth && (


                <img
                    alt="octocat login"
                    data-testid="btn-login-form"
                    className="octoCatLogo"
                    src={logout.toString()}
                    onClick={() => navigate('/logout')}
                />

            )}
            {!isAuth && (


                <img
                    alt="octocat login"
                    data-testid="btn-login-form"
                    className="octoCatLogo"
                    src={register.toString()}
                    onClick={() => navigate('/register')}
                />

            )}
            {!isAuth && (


                <img
                    alt="octocat login"
                    data-testid="btn-login-form"
                    className="octoCatLogo"
                    src={login.toString()}
                    onClick={() => navigate('/login')}
                />

            )}
        </header>
    );
};

export default Header;

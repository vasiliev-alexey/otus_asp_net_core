import React, {useCallback, useMemo, useState} from 'react';
import "./Header.scss";
import login from '../../../assets/login.svg';
import logout from '../../../assets/logout.svg';
import register from '../../../assets/register.svg';
import {useNavigate} from "react-router";

const Header: React.FC = () => {
    const [isAuth, _] = useState(true);

    const  navigate = useNavigate();

    return (
        <header className="siteHeader">
            {/*<img*/}
            {/*  alt="game play"*/}
            {/*  className="btn-go-to-game"*/}
            {/*  src={tetris.toString()}*/}
            {/*  onClick={tetrisRoute}*/}
            {/*  data-testid="btn-go-to-game"*/}
            {/*/>*/}

            <div className="sign" data-testid="welcome-label">
                <span className="fast-flicker">Otus </span>
                <span className="flicker"> Simple </span>
                <span className="fast-flicker"> Frontend</span>
            </div>

            {/*{isAuth && <p className="user-name-label"> Player: {userName}</p>}*/}

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
                    onClick={ () => navigate('/logout')}
                />

            )}
            {isAuth && (


                <img
                    alt="octocat login"
                    data-testid="btn-login-form"
                    className="octoCatLogo"
                    src={register.toString()}
                    onClick={ () => navigate('/register')}
                />

            )}
            {isAuth && (


                <img
                    alt="octocat login"
                    data-testid="btn-login-form"
                    className="octoCatLogo"
                    src={login.toString()}
                     onClick={ () => navigate('/login')}
                />

            )}
        </header>
    );
};

export default Header;

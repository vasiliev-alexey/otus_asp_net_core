import React, {useState} from 'react';
import authService from "@api/authService";
import "./RegisterForm.scss";
import {useNavigate} from "react-router";

export const RegisterForm = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [passwordConf, setPasswordConf] = useState('');
    const [loggedIn, setRegisterIn] = useState(false);

    const navigate = useNavigate();

    const handleRegister = async () => {

        if (passwordConf !== password) {
            alert('Passwords do not match!');

            return;
        }

        if (username && password && password === passwordConf) {


            const res = await authService.RegisterUser(username, password);
            if (res.IsRegistered) {
                alert('Register in successfully!');
                navigate('/login');
            } else {
                alert('Login failed!');
                setRegisterIn(false);
                setUsername(res.userName ?? '');
                setPassword(res.userID ?? '');
                setPasswordConf(res.userID ?? '');
            }

        } else {
            alert('Please fill in both fields.');
        }
    };

    const handleLogout = async () => {

        await authService.logout();
        setRegisterIn(false);
        setUsername('');
        setPassword('');
    };
    return (
        <div className="container">
            {loggedIn ? (
                <div className="input-group">
                    <h2>Welcome, {username}!</h2>
                    <button className="login_btn" onClick={() => handleLogout()}>Logout</button>
                </div>
            ) : (
                <div className="form-border">
                    <div className="input-group">
                        <label className="input-group__label" htmlFor="userNameInput">Имя пользователя</label>
                        <input type="text" id="userNameInput" className="input-group__input"
                               value={username}
                               onChange={(e) => setUsername(e.target.value)}/>
                    </div>

                    <div className="input-group">
                        <label className="input-group__label" htmlFor="userPassInput">Пароль</label>
                        <input type="password" id="userPassInput" className="input-group__input"
                               value={password}
                               onChange={(e) => setPassword(e.target.value)}/>
                    </div>

                    <div className="input-group">
                        <label className="input-group__label" htmlFor="userPassInput">Повторите Пароль</label>
                        <input type="password" id="userPassInput" className="input-group__input"
                               value={passwordConf}
                               onChange={(e) => setPasswordConf(e.target.value)}/>
                    </div>

                    <div className="input-group">
                        <button type="submit" className="login_btn" onClick={handleRegister}>Зарегистрироваться</button>
                    </div>

                </div>
            )}
        </div>
    );
};


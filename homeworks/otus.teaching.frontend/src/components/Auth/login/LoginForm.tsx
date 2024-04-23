import React, {useMemo, useState} from 'react';
import authService from "../../../api/authService";
import "./LoginForm.scss";

const LoginForm = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [loggedIn, setLoggedIn] = useState(false);


    const handleLogin = async () => {

        if (username && password) {
            setLoggedIn(false);
            const res = await authService.LoginUser(username, password);
            if (res.isLoggedIn) {
                alert('Logged in successfully!');
                setLoggedIn(true);
            } else {
                alert('Login failed!');
                setLoggedIn(false);
                setUsername(res.userName ?? '');
                setPassword(res.userID ?? '');
            }

        } else {
            alert('Please fill in both fields.');
        }
    };

    const handleLogout = async () => {

        await authService.logout();
        setLoggedIn(false);
        setUsername( '');
        setPassword( '');
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
                    <button type="submit" className="login_btn" onClick={handleLogin}>Войти </button>
                    </div>
                    {/*<input*/}
                </div>
            )}
        </div>
    );
};

export default LoginForm;
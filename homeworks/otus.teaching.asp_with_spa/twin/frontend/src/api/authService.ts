import axios from "axios";

class AuthService {
    constructor(private authHost: string) {
    }

    LoginUser = async (
        username: string,
        password: string
    ): Promise<{ isLoggedIn: boolean; userID?: string; userName?: string }> => {

        try {

            const response = await axios.post(
                `${this.authHost}/login`,
                {
                    username,
                    password,
                },
                {
                    withCredentials: true,
                }
            );
            if (response.data.isLoggedIn) {
                return {isLoggedIn: true, userID: response.data.userID, userName: username};
            }

            return {isLoggedIn: false};
        } catch (e: any) {
            return {isLoggedIn: false}
        }
    };
    logout = async (): Promise<void> => {

        await axios.post(`${this.authHost}/logout`, {});
    };
}

const authService = new AuthService(`${window.location.origin}/auth`);
export default authService;
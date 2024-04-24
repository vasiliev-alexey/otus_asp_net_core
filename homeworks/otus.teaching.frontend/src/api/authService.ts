import axios from "axios";

class AuthService {
    constructor(private authHost: string) {
    }

    LoginUser = async (
        username: string,
        password: string
    ): Promise<{ isLoggedIn: boolean; userID: string; userName: string }> => {

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

            return {isLoggedIn: false, userID: "", userName: ""};
        } catch (e: any) {
            return {isLoggedIn: false, userID: "", userName: ""};
        }
    };


    RegisterUser = async (
        username: string,
        password: string
    ): Promise<{ isRegistered: boolean } | any> => {
        try {
            const response = await axios.post(
                `${this.authHost}/register`,
                {
                    username,
                    password,
                },
                {
                    withCredentials: true,
                }
            );
            console.log("response", response.data);
            return response.data;
        } catch (e: any) {
            if (e.response.status === 401) {
                throw e.response.data;
            }
        }
    };

    logout = async (): Promise<void> => {

        await axios.post(`${this.authHost}/logout`, {});
    };
}

const authService = new AuthService(`${window.location.origin}/auth`);
export default authService;
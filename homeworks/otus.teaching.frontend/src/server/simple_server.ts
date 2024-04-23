import express, {Application} from "express";
import cors, {CorsOptions} from "cors";


export default class Server {
    constructor(app: Application) {
        console.log("Starting Server");
        this.config(app);
    }

    private config(app: Application): void {
        const corsOptions: CorsOptions = {
            origin: "http://localhost:3000"
        };

        app.use(cors(corsOptions));
        app.use(express.json());
        app.use(express.urlencoded({extended: true}));
        const port = process.env.PORT || 3000;

        app.post("/auth/login", (req, res) => {
            const {username, password} = req.body;


            if (username === "admin" && password === username) {
                res.send({isLoggedIn: true, userID: "123", userName: "admin"});
            } else {
                res.send({isLoggedIn: false});
            }
        });

        app.post("/auth/logout", (_, res) => {
            res.send({isLoggedIn: false});
        })

        app.listen(port, () => {
            console.log(`Server is Fire at http://localhost:${port}`);
        });

    }
}

const srv = new Server(express());

import path from "path";
import {Configuration} from "webpack";
import CopyWebpackPlugin from "copy-webpack-plugin";


const config: Configuration = {
    mode:
        (process.env.NODE_ENV as "production" | "development" | undefined) ??
        "development",
    entry: "./src/entrypoint.tsx",
// @ts-ignore
    devServer: {
        historyApiFallback: true,
        hot: true,
        headers: {
            "Access-Control-Allow-Origin": "http://localhost",
            "Access-Control-Allow-Headers":
                "Origin, X-Requested-With, Content-Type, Accept",
        },

        proxy: [{
            context: ['/auth/**', '/api/**'],
            target: 'http://localhost:3000',
        }]

    },
    infrastructureLogging: {
        debug: [name => name.includes('webpack-dev-server')],
    },
    module: {
        rules: [
            {
                test: /.tsx?$/,
                use: "ts-loader",
                exclude: /node_modules/,
            },
            {
                test: /\.s[ac]ss$/i,
                use: ["style-loader", "css-loader", "sass-loader"],
            },
        ],
    },
    resolve: {
        extensions: [".tsx", ".ts", ".js"],
    },
    output: {
        filename: "bundle.js",
        path: path.resolve(__dirname, "dist"),
    },
    plugins: [
        new CopyWebpackPlugin({
            patterns: [{from: "public"},     { from: 'public/favicon.ico', to: 'favicon.ico' },],
        }),
    ],
};

export default config;

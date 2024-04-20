import {useEffect, useState} from 'react';
import "./WeatherList.scss";

interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

export function WeatherList() {
    const [forecasts, setForecasts] = useState<Forecast[]>();

    useEffect(() => {
        populateWeatherData().then(r => {
        });
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... </em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Temp. (F)</th>
                <th>Summary</th>
            </tr>
            </thead>
            <tbody>
            {forecasts.map(forecast =>
                <tr key={forecast.date}>
                    <td>{forecast.date}</td>
                    <td>{forecast.temperatureC}</td>
                    <td>{forecast.temperatureF}</td>
                    <td>{forecast.summary}</td>
                </tr>
            )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            {contents}
        </div>
    );

    async function populateWeatherData() {
        const response = await fetch('http://localhost:5000/weatherforecast');
        const data = await response.json();
        setForecasts(data);
    }
}

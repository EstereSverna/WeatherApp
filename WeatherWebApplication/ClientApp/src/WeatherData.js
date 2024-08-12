import React, { useState, useEffect } from 'react';
import axios from 'axios';
import ChartComponent from './components/ChartComponent';

const WeatherData = () => {
    const [minTempData, setMinTempData] = useState(null);
    const [maxTempData, setMaxTempData] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get('/api/weather');
                const data = response.data;

                const groupedData = data.reduce((acc, item) => {
                    const country = item.country;
                    const city = item.city;
                    if (!acc[country]) {
                        acc[country] = {};
                    }
                    if (!acc[country][city]) {
                        acc[country][city] = {
                            minTemperature: [],
                            maxTemperature: []
                        };
                    }
                    acc[country][city].minTemperature.push(item.minTemperature);
                    acc[country][city].maxTemperature.push(item.maxTemperature);
                    return acc;
                }, {});

                const minTempLabels = [];
                const minTemperatures = [];

                Object.keys(groupedData).forEach(country => {
                    Object.keys(groupedData[country]).forEach(city => {
                        minTempLabels.push(`${city}, ${country}`);
                        minTemperatures.push(Math.min(...groupedData[country][city].minTemperature));
                    });
                });

                const minTempFormattedData = {
                    labels: minTempLabels,
                    datasets: [
                        {
                            label: 'Min Temperature',
                            backgroundColor: 'rgba(75,192,192,0.2)',
                            borderColor: 'rgba(75,192,192,1)',
                            data: minTemperatures,
                        },
                    ],
                };

                const maxTempLabels = [];
                const maxTemperatures = [];

                Object.keys(groupedData).forEach(country => {
                    Object.keys(groupedData[country]).forEach(city => {
                        maxTempLabels.push(`${city}, ${country}`);
                        maxTemperatures.push(Math.max(...groupedData[country][city].maxTemperature));
                    });
                });

                const maxTempFormattedData = {
                    labels: maxTempLabels,
                    datasets: [
                        {
                            label: 'Max Temperature',
                            backgroundColor: 'rgba(255,99,132,0.2)',
                            borderColor: 'rgba(255,99,132,1)',
                            data: maxTemperatures,
                        },
                    ],
                };

                setMinTempData(minTempFormattedData);
                setMaxTempData(maxTempFormattedData);
            } catch (error) {
                console.error('Error fetching weather data:', error);
            }
        };

        fetchData();
    }, []);

    const minTempOptions = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Min Temperature',
            },
        },
    };

    const maxTempOptions = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Max Temperature',
            },
        },
    };

    return (
        <div>
            {minTempData ? <ChartComponent data={minTempData} options={minTempOptions} /> : <p>Loading Min Temperature Chart...</p>}
            {maxTempData ? <ChartComponent data={maxTempData} options={maxTempOptions} /> : <p>Loading Max Temperature Chart...</p>}
        </div>
    );
};

export default WeatherData;

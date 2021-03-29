import React, {useState, useEffect} from 'react';
import {HubConnect} from '../../../services/HubService';

import {Region, Status} from '../../../core/Enum';
import {GetEvents} from '../../../services/EventService';
import {ConvertTimeStampToDateTime, OnlyUnique} from '../../../core/Util';

import Table from 'react-bootstrap/Table';
import HeaderCard from './HeaderCard';

let state = [];

export default function EventHub() {
    const [events, setEvents] = useState([]);
    const [connection, setConnection] = useState(null);

    useEffect(() => {
        GetEvents().then((response) => {
            state = [...response.data];
            setEvents(state);
        });

        const newConnection = HubConnect('event');
        setConnection(newConnection);
    }, []);

    const receiveEvent = (event) => {
        state = [event, ...state];
        setEvents(state);
    };

    useEffect(() => {
        if (connection) {
            connection
                .start()
                .then(() => {
                    console.log('Connected!');
                    connection.on('ReceiveEvent', receiveEvent);
                })
                .catch((e) => console.log('Connection failed: ', e));
        }
    }, [connection]);

    const getSensorNamesDistinct = () => {
        const allSensors = events.map((e) => e.sensorName);
        const sensorNames = allSensors.filter(OnlyUnique);

        const quantitys = sensorNames.map((name) => {
            return {
                name: name,
                quantity: events.filter((e) => e.sensorName === name).length,
            };
        });

        return quantitys;
    };

    return (
        <div className="container card form-custom">
            <div className="row div-info">
                {Object.keys(Region).map((r) => (
                    <HeaderCard
                        key={r}
                        title={Region[r]}
                        text={events.filter((e) => e.region == r).length}
                    />
                ))}
            </div>

            <div className="mb-3 h5">
                <b>Número de Eventos Por Sensor</b>
            </div>

            <Table responsive hover>
                <thead>
                    <tr>
                        <th>Nome do Sensor</th>
                        <th>Quantidade de leituras</th>
                    </tr>
                </thead>

                <tbody>
                    {getSensorNamesDistinct().map((sensor) => (
                        <tr key={sensor.name}>
                            <td>{sensor.name}</td>
                            <td>{sensor.quantity}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <div className="mb-3 h5">
                <b>Leitura dos Eventos</b>
            </div>

            <Table responsive hover>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>País</th>
                        <th>Região</th>
                        <th>Nome do Sensor</th>
                        <th>Valor do Sensor</th>
                        <th>Data da Leitura</th>
                        <th>Status</th>
                    </tr>
                </thead>

                <tbody>
                    {events.map((event) => (
                        <tr key={event.id}>
                            <td>{event.id}</td>
                            <td>{event.country}</td>
                            <td>{Region[event.region]}</td>
                            <td>{event.sensorName}</td>
                            <td>{event.value}</td>
                            <td>
                                {ConvertTimeStampToDateTime(event.timeStamp)}
                            </td>
                            <td className="">{Status[event.status]}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
}

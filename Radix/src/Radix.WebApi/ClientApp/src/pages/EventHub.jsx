import React, {useState, useEffect} from 'react';
import {HubConnectionBuilder} from '@microsoft/signalr';

import Table from 'react-bootstrap/Table';

export default function EventHub(props) {
    let state = [];
    const [events, setEvents] = useState([]);
    const [connection, setConnection] = useState(null);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:44380/hubs/event')
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection
                .start()
                .then(() => {
                    console.log('Connected!');

                    connection.on('ReceiveEvent', (event) => {
                        state.push(event);
                        setEvents([...state]);
                    });
                })
                .catch((e) => console.log('Connection failed: ', e));
        }
    }, [connection]);

    return (
        <Table striped bordered hover>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>País</th>
                    <th>Região</th>
                    <th>Nome do Sensor</th>
                    <th>Valor do Sensor</th>
                    <th>TimeStamp</th>
                </tr>
            </thead>

            <tbody>
                {events.map((event) => (
                    <tr key={event.id}>
                        <td>{event.id}</td>
                        <td>{event.country}</td>
                        <td>{event.region}</td>
                        <td>{event.sensorName}</td>
                        <td>{event.value}</td>
                        <td>{event.timeStamp}</td>
                    </tr>
                ))}
            </tbody>
        </Table>
    );
}

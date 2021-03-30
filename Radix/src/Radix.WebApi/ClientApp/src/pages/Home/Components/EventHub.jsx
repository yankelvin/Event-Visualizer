import React, {useState, useEffect} from 'react';

import HeaderCard from './HeaderCard';
import Badge from 'react-bootstrap/Badge';
import Table from 'react-bootstrap/Table';
import TablePagination from '../../Shared/TablePagination';

import {Region, Status} from '../../../core/Enum';
import {HubConnect} from '../../../services/HubService';
import {GetEvents} from '../../../services/EventService';
import {ConvertTimeStampToDateTime, OnlyUnique} from '../../../core/Util';

import {
    BarChart,
    Bar,
    CartesianGrid,
    XAxis,
    YAxis,
    Tooltip,
    Legend,
    ResponsiveContainer,
} from 'recharts';

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

    const GetNumericEvents = () => {
        const eventsWithNumber = events.filter(
            (e) => !Number.isNaN(parseFloat(e.value)),
        );

        return eventsWithNumber;
    };

    const GetChartData = () => {
        const eventsWithNumber = GetNumericEvents();

        const data = eventsWithNumber.map((e) => {
            return {
                sensorName: e.sensorName,
                value: parseFloat(e.value),
            };
        });

        let groupBy = [];

        const uniqueEvents = data.map((d) => d.sensorName).filter(OnlyUnique);

        uniqueEvents.forEach((e) => {
            let sum = 0;

            data.filter((d) => d.sensorName === e).forEach((d) => {
                sum += d.value;
            });

            groupBy.push({sensorName: e, value: sum});
        });

        return groupBy;
    };

    const eventsBySensorColumns = [
        {dataField: 'name', text: 'Nome do Sensor'},
        {dataField: 'quantity', text: 'Quantidade de Leituras'},
    ];

    return (
        <div className="container card form-custom">
            <div className="mt-3 h5">
                <b>Número de Eventos por Região</b>
            </div>

            <div className="row div-info">
                {Object.keys(Region).map((r) => (
                    <HeaderCard
                        key={r}
                        title={Region[r]}
                        text={
                            events.filter((e) => e.region.toString() === r)
                                .length
                        }
                    />
                ))}
            </div>

            <div className="mb-3 h5">
                <b>Número de Eventos por Sensor</b>
            </div>

            <TablePagination
                columns={eventsBySensorColumns}
                data={getSensorNamesDistinct()}
            />

            <div className="mb-3 h5">
                <b>Gráfico de Eventos com Valores Númericos</b>
            </div>

            <BarChart width={1050} height={250} data={GetChartData()}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="sensorName" />
                <YAxis />
                <Tooltip />
                <Legend />
                <ResponsiveContainer />
                <Bar dataKey="value" fill="#8884d8" />
            </BarChart>

            <div className="mt-3 mb-3 h5">
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
                            <td className="">
                                <Badge
                                    variant={
                                        event.status === 0
                                            ? 'success'
                                            : 'danger'
                                    }
                                >
                                    {Status[event.status]}
                                </Badge>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
}

import {HubUrl} from '../core/Constraints';
import {HubConnectionBuilder} from '@microsoft/signalr';

export const HubConnect = (hubName) => {
    const url = `${HubUrl}/${hubName}`;

    const newConnection = new HubConnectionBuilder()
        .withUrl(url)
        .withAutomaticReconnect()
        .build();

    return newConnection;
};

import api from '../infra/Api';

export const GetEvents = async () => {
    const response = await api.get('event');

    return response.data;
};

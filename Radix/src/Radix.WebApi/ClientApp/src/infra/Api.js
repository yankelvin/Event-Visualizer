import axios from 'axios';
import {ApiUrl} from '../core/Constraints';

const api = axios.create({
    baseURL: ApiUrl,
});

export default api;

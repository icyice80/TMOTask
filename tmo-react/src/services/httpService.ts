import axios from 'axios';
import { settings } from '../config/settings';

const http = axios.create({
  baseURL: settings.apiBaseUrl,
  headers: {
    'Content-Type': 'application/json',
  },
});

export default http;
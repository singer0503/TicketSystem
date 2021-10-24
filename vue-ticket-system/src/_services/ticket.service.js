import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const ticketService = {
    getTicket
};

function getTicket() {
    return fetch(`${config.apiUrl}/ticket`, requestOptions.get())
        .then(handleResponse);
}

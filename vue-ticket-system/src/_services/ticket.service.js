import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const ticketService = {
    getTicket,
    postTicket
};

function getTicket() {
    return fetch(`${config.apiUrl}/ticket`, requestOptions.get())
        .then(handleResponse);
}

function postTicket(Summary, Description, Type) {
    return fetch(`${config.apiUrl}/ticket`, requestOptions.post({ Summary, Description, Type }))
        .then(handleResponse);
}
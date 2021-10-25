import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const ticketService = {
    getTicket,
    postTicket,
    putTicket,
    deleteTicket
};

function getTicket() {
    return fetch(`${config.apiUrl}/ticket`, requestOptions.get())
        .then(handleResponse);
}

function postTicket(Summary, Description, Type) {
    return fetch(`${config.apiUrl}/ticket`, requestOptions.post({ Summary, Description, Type }))
        .then(handleResponse);
}
function putTicket(Summary, Description, Id) {
    return fetch(`${config.apiUrl}/ticket`, requestOptions.put({ Summary, Description, Id }))
        .then(handleResponse);
}
function deleteTicket(Id) {
    return fetch(`${config.apiUrl}/ticket/` + Id, requestOptions.delete())
        .then(handleResponse);
}
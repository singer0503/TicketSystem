import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const userService = {
    getAll,
    getById,
    postUser,
    putUser,
    deleteUser
};

function getAll() {
    return fetch(`${config.apiUrl}/admin`, requestOptions.get())
        .then(handleResponse);
}
function getById(id) {
    return fetch(`${config.apiUrl}/admin/${id}`, requestOptions.get())
        .then(handleResponse);
}
function postUser(Username, Password, Role, FirstName, LastName) {
    return fetch(`${config.apiUrl}/admin`, requestOptions.post({ Username, Password, Role, FirstName, LastName }))
        .then(handleResponse);
}
function putUser(Username, Password, Role, FirstName, LastName, Id) {
    return fetch(`${config.apiUrl}/admin`, requestOptions.put({ Username, Password, Role, FirstName, LastName, Id }))
        .then(handleResponse);
}
function deleteUser(Id) {
    return fetch(`${config.apiUrl}/admin/` + Id, requestOptions.delete())
        .then(handleResponse);
}
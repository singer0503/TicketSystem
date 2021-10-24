import { BehaviorSubject } from 'rxjs';

import config from 'config';
import { requestOptions, handleResponse } from '@/_helpers';

const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

export const authenticationService = {
    login,
    logout,
    currentUser: currentUserSubject.asObservable(),  // 以 currentUserSubject 這個 Subject 作為來源創建一個新的 Observable
    get currentUserValue () { return currentUserSubject.value }
};

function login(username, password) {
    return fetch(`${config.apiUrl}/users/authenticate`, requestOptions.post({ username, password }))
        .then(handleResponse)
        .then(user => {
            // 將用戶詳細資料和 jwt Token 儲存在 localStorage 中，以保持使用者在頁面刷新之間登錄
            localStorage.setItem('currentUser', JSON.stringify(user));
            currentUserSubject.next(user);

            return user;
        });
}

function logout() {
    // 從 localStorage 中刪除用戶以註銷用戶
    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
}

<template>
    <div>
        <h1>Admin</h1>
        <p>此頁面僅供管理員訪問</p>
        <div>
            所有使用者:
            <ul v-if="users.length">
                <li v-for="user in users" :key="user.id">
                    {{user.firstName + ' ' + user.lastName}}
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
import { authenticationService, userService } from '@/_services';

export default {
    data () {
        return {
            user: authenticationService.currentUserValue,
            users: []
        };
    },
    created () {
        userService.getAll().then(users => this.users = users);
    }
};
</script>
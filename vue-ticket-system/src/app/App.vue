<template>
    <div>
        <nav v-if="currentUser" class="navbar navbar-expand navbar-dark bg-dark">
            <div class="navbar-nav">
                <router-link to="/" class="nav-item nav-link">首頁</router-link>
                <router-link v-if="isAdmin" to="/admin" class="nav-item nav-link">管理使用者</router-link>
                <a @click="logout" class="nav-item nav-link">登出</a>
            </div>
        </nav>
        <div class="jumbotron">
            <div class="container">
                    <router-view></router-view>  <!--vue router-link / router-view-->
            </div>
        </div>
    </div>
</template>

<script>
import { authenticationService } from '@/_services';
import { router, Role } from '@/_helpers';

export default {
    name: 'app',
    data () {
        return {
            currentUser: null
        };
    },
    computed: {
        isAdmin () { //檢查必須是 Admin
            console.log(this.currentUser) // 必須要有資料
            console.log(this.currentUser.role) // 角色
            return this.currentUser && this.currentUser.role === Role.Admin;
        }
    },
    created () {
        authenticationService.currentUser.subscribe(x => this.currentUser = x); // subscribe
    },
    methods: {
        logout () {
            authenticationService.logout();
            router.push('/login');
        }
    }
};
</script>
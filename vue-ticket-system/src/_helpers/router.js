import Vue from 'vue';
import Router from 'vue-router';

import { authenticationService } from '@/_services';
import { Role } from '@/_helpers';
import HomePage from '@/home/HomePage';
import AdminPage from '@/admin/AdminPage';
import LoginPage from '@/login/LoginPage';

Vue.use(Router);

// 路由表及角色設定 (* 要放最下面)
export const router = new Router({
    mode: 'history',
    routes: [
        { 
            path: '/', 
            component: HomePage, 
            meta: { authorize: [] } 
        },
        { 
            path: '/admin', 
            component: AdminPage, 
            meta: { authorize: [Role.Admin] } 
        },
        { 
            path: '/login', 
            component: LoginPage 
        },

        // 不符合以上條件全部到 /
        { path: '*', redirect: '/' }
    ]
});

router.beforeEach((to, from, next) => {
    // 如果未登錄並嘗試訪問受限頁面，則重定向到登錄頁面
    const { authorize } = to.meta;
    const currentUser = authenticationService.currentUserValue;

    if (authorize) {
        if (!currentUser) {
            // 未登錄，因此使用返回 url 重定向到登錄頁面
            return next({ path: '/login', query: { returnUrl: to.path } });
        }

        // 檢查該頁面的路由，是否有 角色限制
        if (authorize.length && !authorize.includes(currentUser.role)) {
            // 角色未授權 所以重定向到主頁
            return next({ path: '/' });
        }
    }

    next();
})
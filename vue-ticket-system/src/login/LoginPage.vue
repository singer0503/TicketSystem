<template>
    <div>
        <div class="alert alert-info">
            <strong>角色</strong> ：使用者/密碼<br />
            <strong>Admin</strong> ：admin/admin<br />
            <strong>RD</strong> ：rd/rd<br />
            <strong>QA</strong> ：qa/qa<br />
            <strong>PM</strong> ：pm/pm
        </div>
        <h2>Ticket System 登入</h2>
        <form @submit.prevent="onSubmit">
            <div class="form-group">
                <label for="username">使用者</label>
                <input type="text" v-model.trim="$v.username.$model" name="username" class="form-control" :class="{ 'is-invalid': submitted && $v.username.$error }" />
                <div v-if="submitted && !$v.username.required" class="invalid-feedback">使用者 is required</div>
            </div>
            <div class="form-group">
                <label htmlFor="password">密碼</label>
                <input type="password" v-model.trim="$v.password.$model" name="password" class="form-control" :class="{ 'is-invalid': submitted && $v.password.$error }" />
                <div v-if="submitted && !$v.password.required" class="invalid-feedback">密碼 is required</div>
            </div>
            <div class="form-group">
                <button class="btn btn-primary" :disabled="loading">
                    <span class="spinner-border spinner-border-sm" v-show="loading"></span>
                    <span>登入</span>
                </button>
            </div>
            <div v-if="error" class="alert alert-danger">{{error}}</div>
        </form>
    </div>
</template>

<script>
import { required } from 'vuelidate/lib/validators';

import { router } from '@/_helpers';
import { authenticationService } from '@/_services';

export default {
    data () {
        return {
            username: '',
            password: '',
            submitted: false,
            loading: false,
            returnUrl: '',
            error: ''
        };
    },
    validations: {
      username: { required },
      password: { required }
    },
    created () {
        // 如果已經登錄，則使用度由重定向到主頁 home
        if (authenticationService.currentUserValue) { 
            return router.push('/');
        }

        // 從路由參數中獲取返回 url 或默認為 '/'
        this.returnUrl = this.$route.query.returnUrl || '/';
    },
    methods: {
        onSubmit () {
            this.submitted = true;
            // 如果表單無效, 停在這 debug
            this.$v.$touch();
            if (this.$v.$invalid) {
                return;
            }
            this.loading = true;
            authenticationService.login(this.username, this.password)
                .then(
                    user => router.push(this.returnUrl),
                    error => {
                        this.error = error;
                        this.loading = false;
                    }
                );
        }
    }
};
</script>
<template>
    <div>
        <h1>管理使用者</h1>
        <p>此頁面僅供管理員訪問</p>
        <button type="button"
        class="btn btn-primary m-2 fload-end"
        data-bs-toggle="modal"
        data-bs-target="#exampleModal"
        @click="addClick()">
        新增使用者
        </button>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>
                        Id
                    </th>
                    <th>
                        Username
                    </th>
                    <th>
                        Password (future hash sha256)
                    </th>
                    <th>
                        Role
                    </th>
                    <th>
                        FirstName
                    </th>
                    <th>
                        LastName
                    </th>
                    <th>
                        Options
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(user) in users" :key="user.id">
                    <td>{{user.id}}</td>
                    <td>{{user.username}}</td>
                    <td>{{user.password}}</td>
                    <td>{{user.role}}</td>
                    <td>{{user.firstName}}</td>
                    <td>{{user.lastName}}</td>
                    <td>
                        <button type="button"
                        class="btn btn-light mr-1"
                        data-bs-toggle="modal"
                        data-bs-target="#exampleModal"
                        @click="editClick(user)">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                                <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                                </svg>
                            </button>
                        <button type="button" @click="deleteClick(user.id)"
                        class="btn btn-light mr-1">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                            <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                            </svg>
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>

        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">{{modalTitle}}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="input-group mb-3">
                            <span class="input-group-text">Username</span>
                            <input type="text" class="form-control" v-model="Username" >
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">Password</span>
                            <input type="text" class="form-control" v-model="Password" >
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">Role</span>
                            <input type="text" class="form-control" v-model="Role" >
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">FirstName</span>
                            <input type="text" class="form-control" v-model="FirstName" >
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">LastName</span>
                            <input type="text" class="form-control" v-model="LastName" >
                        </div>
                        <button type="button" @click="createClick()" 
                        v-if="Id==0" class="btn btn-primary" data-bs-dismiss="modal" ria-label="Close">
                        Create
                        </button>
                        <button type="button" @click="updateClick()"
                        v-if="Id!=0" class="btn btn-primary" data-bs-dismiss="modal" ria-label="Close">
                        Update
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <!-- <div>
            所有使用者:
            <ul v-if="users.length">
                <li v-for="user in users" :key="user.id">
                    {{user.firstName + ' ' + user.lastName}}
                </li>
            </ul>
        </div> -->
    </div>
</template>

<script>
import { authenticationService, userService } from '@/_services';

export default {
    data () {
        return {
            user: authenticationService.currentUserValue,
            users: [],
            Id:0,
            modalTitle:"",
            Username:"",
            Password:"",
            Role:"",
            FirstName:"",
            LastName:""
        };
    },
    created () {
        userService.getAll().then(users => this.users = users);
    },
    methods: {
        refreshData(){
            userService.getAll().then(users => this.users = users);
        },addClick(){
            this.modalTitle="Add User";
            this.Id=0;
            this.Username="";
            this.Password="";
            this.Role="";
            this.FirstName="";
            this.LastName="";
        },
        editClick(user){
            this.modalTitle="Edit User";
            this.Id=user.id;
            this.Username=user.username;
            this.Password=user.password;
            this.Role=user.role;
            this.FirstName=user.firstName;
            this.LastName=user.lastName;
        },
        createClick(){
            if(this.Username =="" || this.Password=="" || this.Role=="" || this.FirstName ==""|| this.LastName ==""){
                alert("Username / Password / FirstName / LastName, is required")
                return
            }
            userService.postUser(this.Username, this.Password, this.Role, this.FirstName, this.LastName).then(response => {
                alert(response);
                this.refreshData();
            });
        },
        updateClick(){
            userService.putUser(this.Username, this.Password, this.Role, this.FirstName, this.LastName, this.Id).then(response => {
                alert(response);
                this.refreshData();
            });
        },
        deleteClick(id){
            if(!confirm("[Delete] Are you sure?")){
                return;
            }
            userService.deleteUser(id).then(response => {
                alert(response);
                this.refreshData();
            });
        },
    }
};
</script>
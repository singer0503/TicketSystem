<template>
    <div>
        <h1>Ticket</h1>
        <button type="button"
        class="btn btn-primary m-2 fload-end"
        data-bs-toggle="modal"
        data-bs-target="#exampleModal"
        v-if="isAddUpdateDelete"
        @click="addClick()">
        新增 Ticket
        </button>

        <table class="table table-striped">
            <thead>
                <tr>
                    <th>
                        Id
                    </th>
                    <th>
                        Summary
                    </th>
                    <th>
                        Description
                    </th>
                    <th>
                        Type
                    </th>
                    <th>
                        Status
                    </th>
                    <th>
                        Options
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(ticket) in ticketData" :key="ticket.id">
                    <td>{{ticket.id}}</td>
                    <td>{{ticket.summary}}</td>
                    <td>{{ticket.description}}</td>
                    <td>{{ticket.type}}</td>
                    <td>{{ticket.status}}</td>
                    <td>
                        <button type="button"
                        class="btn btn-light mr-1"
                        data-bs-toggle="modal"
                        data-bs-target="#exampleModal"
                        v-if="isAddUpdateDelete"
                        @click="editClick(ticket)">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                                <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                                </svg>
                            </button>
                        <button type="button" @click="deleteClick(ticket.id)" v-if="isAddUpdateDelete"
                        class="btn btn-light mr-1">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                            <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                            </svg>
                        </button>
                        <template v-if="ticket.status !== 'Resolve'">
                            <button  type="button" @click="resolveClick(ticket.id)" v-if="isResolve"
                            class="btn btn-light mr-1" ><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check-square" viewBox="0 0 16 16">
                                <path d="M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"/>
                                <path d="M10.97 4.97a.75.75 0 0 1 1.071 1.05l-3.992 4.99a.75.75 0 0 1-1.08.02L4.324 8.384a.75.75 0 1 1 1.06-1.06l2.094 2.093 3.473-4.425a.235.235 0 0 1 .02-.022z"/>
                                </svg>
                            </button>
                        </template>
                    </td>
                </tr>
            </tbody>
        </table>

        <p>目前角色: <strong>{{currentUser.role}}</strong></p>
        
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">{{modalTitle}}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="input-group mb-3">
                            <span class="input-group-text">Summary</span>
                            <input type="text" class="form-control" v-model="Summary" >
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">Description</span>
                            <input type="text" class="form-control" v-model="Description" >
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">Type</span>
                            <input type="text" class="form-control" v-model="Type" >
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">Status</span>
                            <input type="text" class="form-control" v-model="Status" :disabled="TypeDisabled == 1">
                        </div>
                        <button type="button" @click="createClick()" 
                        v-if="ticketId==0" class="btn btn-primary" data-bs-dismiss="modal" ria-label="Close">
                        Create
                        </button>
                        <button type="button" @click="updateClick()"
                        v-if="ticketId!=0" class="btn btn-primary" data-bs-dismiss="modal" ria-label="Close">
                        Update
                        </button>
                    </div>
                </div>
            </div>
        </div>

    </div>
</template>

<script>
import { userService, authenticationService, ticketService } from '@/_services';
import { Role } from '@/_helpers';

export default {
    data () {
        return {
            currentUser: authenticationService.currentUserValue,
            // userFromApi: null,
            ticketData: [], 
            modalTitle:"",
            Summary:"",
            Description:"",
            Type:"",
            Status:"",
            ticketId:0,
            TypeDisabled:0
        };
    },computed: { // 計算屬性
        isAddUpdateDelete () { //檢查 Add Update Delete 權限
            console.log(this.currentUser) // 必須要有資料
            console.log(this.currentUser.role) // 角色
            return this.currentUser.role === Role.QA || this.currentUser.role === Role.PM || this.currentUser.role === Role.Admin;
        },
        isResolve(){
            return this.currentUser.role === Role.RD || this.currentUser.role === Role.Admin;
        }
    },
    created() {
        //userService.getById(this.currentUser.id).then(user => this.userFromApi = user);
        ticketService.getTicket().then(Ticket => this.ticketData = Ticket);

    },methods: {
        refreshData(){
            ticketService.getTicket().then(Ticket => this.ticketData = Ticket);
        },
        addClick(){
            this.modalTitle="Add Ticket";
            this.ticketId=0;
            this.Summary="";
            this.Description="";
            this.Type="";
            this.Status="";
            this.TypeDisabled=0;
        },
        editClick(ticket){
            this.modalTitle="Edit Ticket";
            this.ticketId=ticket.id;
            this.Summary=ticket.summary;
            this.Description=ticket.description;
            this.Type=ticket.type;
            this.Status=ticket.status;
            this.TypeDisabled=1;
        },
        createClick(){
            if(this.Summary =="" || this.Description=="" || this.Type ==""){
                alert("Summary / Description / Type is required")
                return
            }
            ticketService.postTicket(this.Summary, this.Description, this.Type).then(response => {
                alert(response);
                this.refreshData();
            });
        },
        updateClick(){
            ticketService.putTicket(this.Summary, this.Description,this.ticketId).then(response => {
                alert(response);
                this.refreshData();
            });
        },
        deleteClick(id){
            if(!confirm("[Delete] Are you sure?")){
                return;
            }
            ticketService.deleteTicket(id).then(response => {
                alert(response);
                this.refreshData();
            });
        },
        resolveClick(id){
            console.log(id)
            if(!confirm("[Resolve] Are you sure?")){
                return;
            }
            ticketService.resolveTicket(id).then(response => {
                alert(response);
                this.refreshData();
            });
        }
    }, mounted:function(){
        //this.refreshData();
    }
};
</script>
<template>
    <div>
        <h1>Ticket</h1>
        <button type="button"
        class="btn btn-primary m-2 fload-end"
        data-bs-toggle="modal"
        data-bs-target="#exampleModal"
        @click="addClick()">
        新增 Ticket
        </button>


        <table class="table table-striped">
            <thead>
                <tr>
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
                        Options
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(ticket) in ticketData" :key="ticket.id">
                    <td>{{ticket.summary}}</td>
                    <td>{{ticket.description}}</td>
                    <td>{{ticket.type}}</td>
                    <td>
                    <button type="button"
                    class="btn btn-light mr-1"
                    data-bs-toggle="modal"
                    data-bs-target="#exampleModal"
                    @click="editClick(dep)">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                            <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                            </svg>
                        </button>
                        <button type="button" @click="deleteClick(dep.DepartmentId)"
                        class="btn btn-light mr-1">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                            <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                            </svg>
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>

        <p>目前角色: <strong>{{currentUser.role}}</strong></p>

    </div>
</template>

<script>
import { userService, authenticationService, ticketService } from '@/_services';
import config from 'config';
// import { handleResponse, requestOptions } from '@/_helpers';

export default {
    data () {
        return {
            currentUser: authenticationService.currentUserValue,
            userFromApi: null,
            ticketData: [] // 
        };
    },
    created () {
        userService.getById(this.currentUser.id).then(user => this.userFromApi = user);
        ticketService.getTicket().then(Ticket => this.ticketData = Ticket);

    },methods: {
        refreshData: function () {
            console.log('refreshData call = '+`${config.apiUrl}/ticket`)
            // axios.get(`${config.apiUrl}/ticket`)
            // .then((response)=>{
            //     this.TicketData=response.data;
            //     console.log(response.data)
            // });
            // fetch(`${config.apiUrl}/ticket`, requestOptions.get())
            // .then(handleResponse).then(ticket => {
            //     this.TicketData = ticket;
            //     console.log(ticket);
            // });
        }
    }, mounted:function(){
        //this.refreshData();
    }
};
</script>
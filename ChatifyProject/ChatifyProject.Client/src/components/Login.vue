<script setup>
import axios from 'axios';
import signalRService from '../services/SignalRService.js';
</script>

<template>
    <div class="login">
        <template v-if="!username">
            <label>Username:</label>
            <input type="text" v-model="loginModel.username" />
            <label>Password:</label>
            <input type="password" v-model="loginModel.password" />
            <button class="btn btn-primary btn-sm" v-on:click="login()">Login</button>
        </template>
        <template v-else> User {{ username }} logged in. <span class="logoutLink" v-on:click="logout()">Logout</span> </template>
    </div>
</template>
<style scoped>
.login {
    display: flex;
    gap: 0.5rem;
    align-items: center;
    font-size: 80%;
}

.login input {
    width: 10em;
}
.logoutLink {
    cursor: pointer;
    font-weight: bolder;
}
</style>
<script>
export default {
    data() {
        return {
            loginModel: {
                username: '',
                password: '',
            },
        };
    },
    methods: {
        async login() {
            try {
                const userdata = (await axios.post('user/login', this.loginModel)).data;
                axios.defaults.headers.common['Authorization'] = `Bearer ${userdata.token}`;
                this.$store.commit('authenticate', userdata);
                signalRService.configureConnection(userdata.token);
                await signalRService.connect();                
            } catch (e) {
                if (e.response.status == 401) {
                    alert('Login failed. Invalid credentials.');
                }
            }
        },
        logout() {
            delete axios.defaults.headers.common['Authorization'];
            this.$store.commit('authenticate', null);
        },
    },
    computed: {
        authenticated() {
            return this.$store.state.userdata.username ? true : false;
        },
        username() {
            return this.$store.state.userdata.username;
        },
    },
};
</script>
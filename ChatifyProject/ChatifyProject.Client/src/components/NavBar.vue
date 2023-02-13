<script setup>
import Login from '../components/Login.vue';
</script>
<template>
    <div class="navBar">
        <ul>
            <li><router-link class="btn btn-outline-dark" to="/">Home</router-link></li>
            <li><router-link class="btn btn-outline-dark" to="/chatdemo">Chat Demo</router-link></li>
            <li><router-link class="btn btn-outline-dark" to="/waitingroom">Warteraum</router-link></li>
            <li><router-link class="btn btn-outline-dark" to="/rtcchat">WebRTC Chat</router-link></li>
        </ul>
        <Login></Login>
    </div>
</template>

<style scoped>
.navBar {
    display: flex;
    margin-top: 5px;
    align-items: center;
}
.navBar ul {
    flex-grow: 1;
}
ul {
    display: flex;
    margin: 0;
    padding: 0;
    gap:0.5rem;
}
li {
    list-style-type: none;
}

</style>
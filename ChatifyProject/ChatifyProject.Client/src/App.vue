<script setup>
import { RouterLink, RouterView } from 'vue-router';
import Navbar from './components/NavBar.vue';
</script>

<template>
    <div class="appContainer">
        <nav>
            <Navbar></Navbar>
        </nav>
        <main>
            <router-view></router-view>
        </main>
    </div>
</template>

<style scoped>
.appContainer {
  display: flex;
  flex-direction: column;
  max-width: 70em;
  margin: auto;
}
.appContainer nav {
  flex: 0 0 3em;
}
.appContainer main {
  flex-grow: 1;
}
</style>

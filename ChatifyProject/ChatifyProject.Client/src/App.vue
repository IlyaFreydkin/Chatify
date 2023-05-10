<script setup>
import { RouterLink, RouterView } from 'vue-router';
import NavBar from './components/NavBar.vue';
import Footer from './components/Footer.vue';
import WaitingRoomViewVue from './views/WaitingRoomView.vue';


</script>

<template>
  	<nav>

         <NavBar></NavBar>
        </nav>
    <div class="appContainer">
        
        <main>
          
            
        </main>
    </div>

    <footer> <Footer></Footer> </footer> 

</template>

<style scoped>
html, body {
  background-color: #2be5bd;
}
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
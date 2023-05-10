<script setup>
import NavBar from '../components/NavBar.vue';
import Footer from '../components/Footer.vue';

</script>

<template>  
    <div id="page">
        <NavBar></NavBar>
        <div class="container">
            <h1>Welcome to our Video Chatting Platform!</h1>
            <p>Join our community and start chatting with people from around the world.</p>
            <router-link to="/webrtc">
				<button> Start Video Chatting </button>
			</router-link>
        </div>  
    </div>
        <Footer></Footer>
</template>

<script>
export default {
  methods: {
    
  }
}
</script>

<style scoped>
#page {
    text-align: center;
}
.container {
    margin-top: 70px;
    margin-bottom: 170px;
    
}
button {
  --color: #8167a9;
  font-family: inherit;
  display: inline-block;
  width: 10em; 
  height: 2.6em; 
  overflow: hidden;
  margin: 20px;
  font-size: 17px;
  z-index: 1;
  color: var(--color);
  border: 2px solid var(--color);
  border-radius: 6px;
  position: relative;
  font-size: 16px;
  background: white;
}
button::before {
  position: absolute;
  content: "";
  background: var(--color);
  width: 150px;
  height: 200px;
  z-index: -1;
  border-radius: 50%;
}
button:hover {
  background-color: #8167a9;
  color: white;
  cursor: pointer;
}
button:before {
  top: 100%;
  left: 100%;
  transition: .3s all;
}
button:hover::before {
  top: -30px;
  left: -30px;
}
</style>


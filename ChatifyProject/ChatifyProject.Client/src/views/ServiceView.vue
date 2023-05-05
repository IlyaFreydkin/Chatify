<script setup>
import NavBar from '../components/NavBar.vue';
import Footer from '../components/Footer.vue';
</script>

<template>
  <div id="page">
    <NavBar></NavBar>
    <div class="container">
      <h1>Our Services</h1>
      <p>We offer a variety of video chatting services to suit your needs.</p>
      <ul>
        <li>Service</li>
        <li>Service</li>
        <li>Service</li>
        <li>Service</li>
      </ul>
      <p>Sign up now to start using our platform!</p>
      <router-link to="/signup">
        <button>Sign Up</button>
      </router-link>
    </div>
    <Footer></Footer>
  </div>
</template>

<style scoped>
.container {
  margin-top: 70px;
  margin-bottom: 70px;
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
ul {
    list-style-type: decimal; 
    padding-left: 50px; 
}
</style>

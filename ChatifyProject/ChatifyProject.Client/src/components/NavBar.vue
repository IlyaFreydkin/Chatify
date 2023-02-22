<script setup>
import Login from '../components/Login.vue';
</script>
<template>
   <nav class="navbar navbar-expand-lg navbar-light" style="background: linear-gradient(to right, rgba(255, 255, 255, 0), rgba(255, 255, 255, 1));">
  <div class="container-fluid">
    <a class="navbar-brand" href="#">My Website</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav ms-auto w-100 justify-content-end">
        <li class="nav-item">
          <router-link class="nav-link" to="/">Home</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link" to="/chatdemo">Chat Demo</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link" to="/waitingroom">Warteraum</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link" to="/rtcchat">WebRTC Chat</router-link>
        </li>
      </ul>
    </div>
  </div>
</nav>


        
        <Login></Login>
   
</template>

<style scoped>
.navbar {
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: 100%;
}

.navbar-brand {
  font-size: 1.5rem;
  font-weight: bold;
  color: #333;
}

.nav-link {
  color: #333;
  font-weight: 500;
}

.nav-link:hover,
.nav-link:focus {
  color: #007bff;
}

.navbar-toggler-icon {
  background-color: #333;
}

@media (max-width: 991.98px) {
  .navbar-collapse {
    background-color: #fff;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }

  .nav-link {
    font-size: 1.2rem;
    border-bottom: 1px solid #e0e0e0;
  }

  .nav-link:last-child {
    border-bottom: none;
  }

  .nav-link:hover,
  .nav-link:focus {
    color: #007bff;
    background-color: transparent;
  }
}

</style>
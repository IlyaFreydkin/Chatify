<template>
  <nav class="navMenu">
    <router-link class="nav-link" to="/home">
      <span>Home</span>
    </router-link>
    <router-link class="nav-link" to="/blog">
      <span>ChatDemo</span>
    </router-link>
    <router-link class="nav-link" to="/work">
      <span>Warteraum</span>
    </router-link>
    <router-link class="nav-link" to="/about">
      <span>WebRTC Chat</span>
    </router-link>
    <div class="dot"></div>

    <button> Login
</button>
  </nav>
</template>

<style scoped>
  @import url("https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap");


  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }

  body {
   
    font-family: "Montserrat", sans-serif;
  }

  .navMenu {
    position: fixed;
    top: 0;
    left: 50%;
    transform: translateX(-50%);
    z-index: 1;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .nav-link {
    color: #725AC1;
    text-decoration: none;
    font-size: 1.2em;
    text-transform: uppercase;
    font-weight: 500;
    padding: 10px 20px;
    margin: 0 10px;
    transition: all 0.2s ease-in-out;
    position: relative;
  }

  .nav-link:hover {
    color: #150369;
  }

  .nav-link:hover .dot {
    opacity: 1;
    transform: scale(1);
  }

  .dot {
    width: 12px;
    height: 12px;
    background: #563cc7;
    border-radius: 50%;
    opacity: 0;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%) scale(0);
    transition: all 0.2s ease-in-out;
    z-index: -1;
  }

button {
  position: relative;
  display: inline-block;
  margin: 15px;
  padding: 15px 30px;
  text-align: center;
  font-size: 18px;
  letter-spacing: 1px;
  text-decoration: none;
  color: #725AC1;
  background: transparent;
  cursor: pointer;
  transition: ease-out 0.5s;
  border: 2px solid #725AC1;
  border-radius: 10px;
  box-shadow: inset 0 0 0 0 #725AC1;
  
}

button:hover {
  color: white;
  box-shadow: inset 0 -100px 0 0 #725AC1;
}

button:active {
  transform: scale(0.9);
}

  
</style>
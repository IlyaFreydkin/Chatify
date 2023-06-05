<script setup>
import NavBar from '../components/NavBar.vue';
import Footer from '../components/Footer.vue';
import WaitingRoomViewVue from '../views/WaitingRoomView.vue';
import signalRService from '../services/SignalRService.js';
</script>

<template>
  <div class="chat-room-view">
    <nav>
      <NavBar />
    </nav>
    <main>
      <section class="chat-room">
        <h2 class="chat-room-title">Allgemein</h2>
        <div class="chat-messages">
          <div v-for="(message, index) in messages" :key="index" class="chat-message">
            <div class="avatar">
              <img src="https://i.imgur.com/8F6yeT2.png" alt="Avatar">
            </div>
            <div class="chat-message-content">
              <div class="chat-message-header">
                <span class="chat-message-author">Max Mustermann</span>
                <span class="chat-message-timestamp">vor 5 Minuten</span>
              </div>
              <div class="chat-message-text">
                {{ message }}
              </div>
            </div>
          </div>
        </div>
        <div class="new-message">
         
<input type="text" name="text" class="input" placeholder="Schreib eine Nachricht">
          <button class="button" id="button" @click="sendMessage">Senden</button>
        </div>
      </section>
    </main>
    <footer>
      <Footer />
    </footer>
  </div>
</template>

<style scoped>

.chat-room-view {
  display: flex;
  flex-direction: column;
  height: 100vh;
   margin-bottom: 250px;
}


nav {
  margin-bottom: 0.5rem;
}

.chat-room {
  background-color: rgb(114,90,193);
  border-radius: 0.25rem;
  box-shadow: 0 1px 0 rgba(110, 98, 42, 0.2);
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  margin: 0.5rem;
  overflow-y: scroll;
  
  
}
.input {
 color: #8707ff;
 border: 2px solid #8707ff;
 border-radius: 10px;
 padding: 30px 25px;
 
 background: transparent;
 width: 450px;
 
}

.input:active {
 box-shadow: 2px 2px 15px #8707ff inset;
}


.chat-room-title {
  color: #fff;
  font-size: 1.5rem;
  font-weight: 500;
  margin: 0;
  padding: 1rem;
}

.chat-messages {
  display: flex;
  flex-direction: column;
  margin: 0 1rem 1rem 1rem;
}

.chat-message {
  display: flex;
  margin-bottom: 1rem;
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

button:active {
  transform: translateY(5px);
  box-shadow: 0px 0px 0px 0px #a29bfe;
}
.avatar {
  height: 36px;
  margin-right: 0.5rem;
  width: 36px;
}

.avatar img {
  border-radius: 50%;
  height: 100%;
  width: 100%;
}

.chat-message-content {
  display: flex;
  flex-direction: column;
}

.chat-message-header {
  align-items: center;
  display: flex;
  margin-bottom: 0.25rem;
}

.chat-message-author {
  color: #fff;
  font-size: 1rem;
  font-weight: 500;
  margin-right: 0.5rem;
}

.chat-message-timestamp {
  color: #8e9297;
  font-size: 0.875rem;
}

.chat-message-text {
  background-color: #40444b;
  border-radius: 0.25rem;
  color: #fff;
  font-size: 1rem;
  padding: 0.75rem;
}

footer {
  margin-top: auto; 
 
}

.new-message {
  background-color: #ffffff;
  border-radius: 0.25rem;
  box-shadow: 0 1px 0 rgba(4, 4, 5, 0.2);
  display: flex;
  margin: 100px 1rem 1rem 1rem;
  padding: 1rem;

}
</style>

<script>
export default {
  data() {
    return {
      messages: [],
      newMessage: "",
    };
  },
  async mounted() {
    try {
      signalRService.configureConnection(this.$store.state.userdata.token);
      signalRService.subscribeEvent("ReceiveMessage", this.onMessageReceive);
      await signalRService.connect();
    } catch (e) {
      alert(JSON.stringify(e));
    }
  },
  methods: {
    onMessageReceive(message) {
      this.messages.push(message);
    },
    sendMessage() {
      signalRService.sendMessage(this.newMessage);
    },
  },
};
</script>

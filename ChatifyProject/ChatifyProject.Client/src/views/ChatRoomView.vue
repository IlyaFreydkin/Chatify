<script setup>
import NavBar from '../components/NavBar.vue';
import Footer from '../components/Footer.vue';
import WaitingRoomViewVue from '../views/WaitingRoomView.vue';
import signalRService from '../services/SignalRService.js';
</script>

<template>
  <div class="chat-room-view">
    <NavBar></NavBar>
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
                <span class="chat-message-author">{{ username }}</span>
                <span class="chat-message-timestamp">{{ time }}</span>       
              </div>
              <div class="chat-message-text">
                {{ message }}
              </div>
            </div>
          </div>
        </div>
        <div class="new-message">
          <textarea v-model="newMessage" placeholder="Nachricht eingeben"></textarea>
          <button class="btn btn-primary" @click="sendMessage">Senden</button>
        </div>
      </section>
    </main>
    <Footer></Footer>
  </div>
</template>

<style scoped>

.chat-room-view {
  display: flex;
  flex-direction: column;
  height: 100vh;
}

nav {
  margin-bottom: 0.5rem;
}

.chat-room {
  background-color: #36393f;
  border-radius: 0.25rem;
  box-shadow: 0 1px 0 rgba(4, 4, 5, 0.2);
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  margin: 0.5rem;
  overflow-y: scroll;
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

.new-message {
  background-color: #2f3136;
  border-radius: 0.25rem;
  box-shadow: 0 1px 0 rgba(4, 4, 5, 0.2);
  display: flex;
  margin: 0
}
</style>

<script>
export default {
  data() {
    return {
      messages: [],
      newMessage: "",
      username: "",
      time: "",
    };
  },
  async mounted() {
    try {
      this.interval = setInterval(() => this.updateTimeStamps(), 10000);
      signalRService.configureConnection(this.$store.state.userdata.token);
      signalRService.subscribeEvent("ReceiveMessage", this.onMessageReceive);
      signalRService.subscribeEvent("GetUser", this.onGetUser);
      await signalRService.connect();
    } catch (e) {
      alert(JSON.stringify(e));
    }
  },
  methods: {
    onMessageReceive(message) {
      this.time = new Date().toLocaleTimeString();
      const lastMessageIndex = this.messages.length - 1;
      this.$set(this.messages[lastMessageIndex], "time", new Date().toLocaleTimeString());
      this.messages.push(message);
    },
    onGetUser(username) {
      this.username = username;
    },
    sendMessage() {
      signalRService.sendMessage(`${this.newMessage}`);
      this.newMessage = ""; // reset the input field
    },
  },
  beforeDestroy() {
    clearInterval(this.interval);
  },
};
</script>

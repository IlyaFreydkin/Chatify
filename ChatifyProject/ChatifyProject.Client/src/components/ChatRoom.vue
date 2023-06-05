<script setup>
import axios from 'axios';
import signalRService from '../services/SignalRService.js';
import NavBar from '../components/NavBar.vue';
import Footer from '../components/Footer.vue';
import { VueTextToSpeech } from 'vue-text-to-speech';
</script>

<template>
  <div class="chat-room-view">
    <NavBar></NavBar>
    <main class="main-container">
      <aside class="user-list">
        <h3>Active Users</h3>
        <ul>
          <div v-for="user in connectedUsers" :key="user" @click="selectUser(user)">
            <li v-if="user !== $store.state.userdata.username">
              {{ user }}
            </li>
          </div>
        </ul>
      </aside>
      <section class="chat-room">
        <h3 class="chat-room-title"> {{this.selectedUser}} </h3>
        <div class="chat-messages" ref="chatContainer">
          <div v-for="(message, index) in messages" :key="index" class="chat-message">
            <div class="avatar">
              <img src="@/assets/Avatar.jpg" alt="Avatar">
            </div>
            <div class="chat-message-content">
              <div class="chat-message-header">
                <span class="chat-message-user">{{ message.username }}</span>
              </div>
              <div class="chat-message-text">
                {{ message.text }}
                <span class="chat-message-timestamp">{{ message.time }}</span>
                <vue-text-to-speech :text="message.text"></vue-text-to-speech>
                 <button class="btn"><span class="fas fa-volume-up" @click="speakMessage(message.text)">READ</span></button>
              </div>
            </div>
          </div>
        </div>
        <div class="new-message">
          <textarea v-model="newMessage" placeholder="Nachricht eingeben" @keydown.enter.prevent="sendMessageToAll"></textarea>
        </div>
      </section>
    </main>
    <Footer></Footer>
    <div class="scroll-to-top" @click="scrollToTop">
      <i class="gg-arrow-long-up"></i>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      connectedUsers: [],
      messages: [],
      newMessage: "",
      selectedUser: null,
    };
  },
  async mounted() {
    try {
      signalRService.subscribeEvent("ReceiveMessage", this.onMessageReceive);
      signalRService.subscribeEvent("ReceiveConnectedUsers", this.onReceiveConnectedUsers);
      signalRService.requestConnectedUsers();
    } catch (e) {
      alert(JSON.stringify(e));
    }
  },
  unmounted() {
  },
  scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  },
  methods: {
    selectUser(user) {
      this.selectedUser = user;
      this.messages = [];
      console.log(user);
      this.$router.push(`/chatroom/${user}`);
    },
    //speak message
    speakMessage(text) {
  if ('speechSynthesis' in window) {
    const utterance = new SpeechSynthesisUtterance(text);
    speechSynthesis.speak(utterance);
  } else {
    console.log('Text-to-Speech wird nicht unterstützt.');
  }},
    // Message
    onMessageReceive(message) {
      const time = new Date().toLocaleTimeString();
      if (message.message == undefined) {
        this.messages.push({ text: message, time: time, username: "System" });
      }
      else {
        if (
          this.selectedUser === this.$store.state.userdata.username ||
          message.username === this.selectedUser ||
          message.username === this.$store.state.userdata.username
        ) {
          this.messages.push({ text: message.message, time: time, username: message.username });
        }
      }
    },
    onReceiveConnectedUsers(users) {
      this.connectedUsers = users;
    },
    sendMessageToAll() {
      if (this.newMessage.trim() !== '') { // check if the message is not empty
        if (this.selectedUser === this.$store.state.userdata.username) {
          signalRService.sendMessageToAll(this.newMessage);
        } else {
          signalRService.sendMessageToAll(this.newMessage, this.selectedUser);
          signalRService.sendMessageToAll(this.newMessage, this.$store.state.userdata.username);
        }
        this.newMessage = "";
      }
    },
    scrollToTop() {
      const chatContainer = this.$refs.chatContainer;
      chatContainer.scrollTop = 0;
    },
    scrollToBottom() {
      const chatContainer = this.$refs.chatContainer;
      chatContainer.scrollTop = chatContainer.scrollHeight;
    },
  },
  updated() {
    this.scrollToBottom();
  },
  created() {
    this.selectedUser = this.$route.params.user;
  },
};
</script>

<style scoped>
.chat-room-view {
  display: flex;
  flex-direction: column;
  height: 100vh;
}

.chat-room {
  background-color: #c6a7f3;
  border-radius: 0.25rem;
  box-shadow: 0 1px 0 rgba(4, 4, 5, 0.2);
  overflow-y: scroll;
  flex-grow: 1;
  height: fit-content;
}

.chat-room-title {
  background-color: #7f4ccc;
  font-size: 1.5rem;
  font-weight: 500;
  margin: 0;
  padding: 1rem;
}

.chat-messages {
  display: flex;
  flex-direction: column;
  margin: 0 1rem 1rem 1rem;
  margin-top: 1rem;
  max-height: 40vh;
  overflow-y: auto;
}

.chat-message {
  display: flex;
  margin-bottom: 1rem;
}

.avatar {
  height: 50px;
  width: 50px;
  margin-right: 0.5rem;
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

.chat-message-user {
  font-size: 1rem;
  font-weight: 500;
  margin-right: 0.5rem;
}

.chat-message-timestamp {
  color: #8e9297;
  font-size: 0.875rem;
}

.chat-message-text {
  background-color: #fff;
  border-radius: 0.25rem;
  font-size: 1rem;
  padding: 0.75rem;
}

.new-message {
  background-color: #8167a9;
  border-radius: 0.25rem;
  box-shadow: 0 1px 0 rgba(4, 4, 5, 0.2);
  display: flex;
  margin: 0;

}

textarea {
  background-color: #fff;
  border: none;
  border-radius: 0.25rem;
  flex-grow: 1;
  font-size: 1rem;
  margin: 0.2rem;
  outline: none;
  resize: none;
}

.main-container {
  display: flex;
}

.user-list {
  background-color: #f2f2f2;
  padding: 1rem;
  width: 250px;
}

.user-list h3 {
  margin-top: 0;
}

.user-list ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

.user-list li {
  padding: 0.5rem;
  cursor: pointer;
}

.user-list li:hover {
  background-color: #c6a7f3;
  color: white;
}

nav {
  margin-bottom: 0.5rem;
}

/* scrolling */
.scroll-to-top {
  position: fixed;
  bottom: 20px;
  right: 20px;
  background-color: #fff;
  border-radius: 50%;
  width: 40px;
  height: 40px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}

.scroll-to-top i {
  font-size: 1.5rem;
}

.gg-arrow-long-up,
.gg-arrow-long-up::after {
  display: block;
  box-sizing: border-box;
  width: 6px
}

.gg-arrow-long-up {
  position: relative;
  transform: scale(var(--ggs, 1));
  border-right: 2px solid transparent;
  border-left: 2px solid transparent;
  box-shadow: inset 0 0 0 2px;
  height: 24px
}

.gg-arrow-long-up::after {
  content: "";
  position: absolute;
  height: 6px;
  border-top: 2px solid;
  border-left: 2px solid;
  transform: rotate(45deg);
  top: 0;
  left: -2px
}
</style>

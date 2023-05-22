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
        <h2 class="chat-room-title">General</h2>
        <div class="chat-messages">
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
              </div>
            </div>
          </div>
        </div>
        <div class="new-message">
          <textarea v-model="newMessage" placeholder="Nachricht eingeben" @keydown.enter.prevent="sendMessage"></textarea>     
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
        messages: [],
        newMessage: "",
        username: ""
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
    scrollToTop() {
      window.scrollTo({ top: 0, behavior: 'smooth' });
    },
    methods: {
      onMessageReceive(message) {
        const time = new Date().toLocaleTimeString();
        if(message.message == undefined) {
          this.messages.push({text: message, time: time, username: ""});
        }
        else{
          this.messages.push({text: message.message, time: time, username: message.username});
        }
        
      },
      sendMessage() {
        if (this.newMessage.trim() !== '') 
        { // check if the message is not empty
          signalRService.sendMessage(`${this.newMessage}`);
          this.newMessage = ""; // reset the input field
        }
      },
      scrollToTop() {
        window.scrollTo({ top: 0, behavior: 'smooth' });
      },
    },
  };
</script>

<style scoped>

/* main {
  overflow-y: auto;
} */
.chat-room-view {
  display: flex;
  flex-direction: column;
  height: 100vh;
}
nav {
  margin-bottom: 0.5rem;
}
.chat-room {
  background-color: #c6a7f3;
  border-radius: 0.25rem;
  box-shadow: 0 1px 0 rgba(4, 4, 5, 0.2);
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  margin: 0.5rem;
  overflow-y: scroll;
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
    transform: scale(var(--ggs,1));
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
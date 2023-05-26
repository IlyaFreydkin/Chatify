<script setup>
import axios from 'axios';
import signalRService from '../services/SignalRService.js';
import NavBar from '../components/NavBar.vue';
import Footer from '../components/Footer.vue';
</script>

<template>
  <div class="waitingRoomView">
    <NavBar></NavBar>
    <main class="main-container">
      <aside class="user-list">
        <h3>Active Users</h3>
        <ul>
          <li v-for="user in connectedUsers" :key="user" @click="selectUser(user)">{{ user }}</li>
        </ul>
      </aside>
      <section class="waiting-room">
        <h2 class="waiting-room-title">Waiting Room</h2>
          <ul>Connected Users:
            <div v-for="user in connectedUsers" :key="user">
              <ul>{{ user }} <button @click="challenge(user)">Challenge</button></ul>
            </div>
          </ul>
          <!-- <div>
            Incoming Challenges:
          </div>
          <div v-if="activeChallenges !== undefined">
            <div v-for="challenge in activeChallenges" :key="challenge">
              <ul>
                {{ challenge }}
                <button @click="processChallenge('accepted', challenge)">Accept</button>
                <button @click="processChallenge('declined', challenge)">Decline</button>
              </ul>
            </div>
          </div> -->
      </section>
    </main>
    <Footer></Footer> 
  </div>  
</template>

<script>
export default {
  data() {
    return {
      connectedUsers: [],
    };
  },
  async mounted() {
    this.connect();
    try { 
        signalRService.configureConnection(this.$store.state.userdata.token);
        await signalRService.connect();
        await signalRService.enterWaitingroom();
        await this.sendConnectedUsers();
        console.log(this.connectedUsers);
    } catch (e) {
        alert(JSON.stringify(e));
    }
  },
  unmounted() {
  },
  methods: {
    async connect() {
      await signalRService.connect();
      signalRService.subscribeEvent("SetWaitingroomState", this.addUser);
      // Start the SignalR connection
      //await signalRService.sendConnectedUsers(); 
    },
    selectUser(user) {
      this.$router.push({ name: 'chatRoom', params: { username: user } });
    },
    enterWaitingroom() {
      signalRService.enterWaitingroom();
    },
    leaveWaitingroom() {
      signalRService.leaveWaitingroom();
    },
    async sendConnectedUsers() { 
      try {
        const users = await signalRService.sendConnectedUsers();
        console.log(users);
        this.connectedUsers = users;
        console.log(this.connectedUsers);
      } catch (error) {
        console.error("Error retrieving connected users:", error);
        throw error;
      }
    },
    addUser(names) {
      this.connectedUsers = names;
    },
  },
};
</script>

<style scoped>
.waitingRoomView {
  display: flex;
  flex-direction: column;
  height: 100vh;
}
.main-container {
  display: flex;
  flex-grow: 1;
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
.waiting-room {
  background-color: #c6a7f3;
  border-radius: 0.25rem;
  box-shadow: 0 1px 0 rgba(4, 4, 5, 0.2);
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  overflow-y: scroll;
}
.waiting-room-title {
  background-color: #7f4ccc;
  font-size: 1.5rem;
  font-weight: 500;
  margin: 0;
  padding: 1rem;
}
</style>

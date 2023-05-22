<script setup>
import axios from 'axios';
import signalRService from '../services/SignalRService.js';
import NavBar from '../components/NavBar.vue';
import Footer from '../components/Footer.vue';
</script>

<template>
  <div class="waitingRoomView">
    <NavBar></NavBar>
    <main>
      <section class="waiting-room">
        <h2 class="waiting-room-title">Waiting Room</h2>
        <div class="waitinguser" v-if="!joinedQueue">
          <button @click="connect()">Join Queue</button>
        </div>
        <div v-if="joinedQueue">
          <ul>Connected Users:
            <div v-for="user in connectedUsers" :key="user">
              <ul>{{ user }} <button @click="challenge(user)">Challenge</button></ul>
            </div>
          </ul>
          <div>
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
          </div>
        </div>
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
      joinedQueue: false,
      activeChallenges: [],
    };
  },
  mounted() {
  },
  unmounted() {
    signalRService.leaveWaitingroom();
  },
  methods: {
    async connect() {
      await signalRService.connectWithToken(this.$store.state.infos.token);
      signalRService.subscribeEvent("SetWaitingroomState", this.addUser);
      signalRService.subscribeEvent("GetChallenges", this.printChallenges);
      signalRService.enterWaitingroom();
      signalRService.subscribeEvent("GameStarted", this.pushRouter);
      this.joinedQueue = true;
    },
    enterWaitingroom() {
      signalRService.enterWaitingroom();
    },
    addUser(names) {
      this.connectedUsers = names;
    },
    challenge(username) {
      signalRService.subscribeEvent("GameStarted", this.pushRouter);
      signalRService.challenge(username);
    },
    printChallenges(challenges) {
      if (challenges[1] === this.$store.state.infos.username) {
        this.activeChallenges.push(challenges[0]);
      }
    },
    processChallenge(state, challenge) {
      if (state === "accepted") {
        const index = this.activeChallenges.indexOf(challenge);
        if (index > -1) {
          this.activeChallenges.splice(index, 1);
        }
        signalRService.startGame(challenge);
      } else {
        const index = this.activeChallenges.indexOf(challenge);
        if (index > -1) {
          this.activeChallenges.splice(index, 1);
        }
      }
    },
    async pushRouter(gameId) {
      this.$store.commit("joinGame", gameId);
      this.leaveWaitingroom();
      await this.$router.push("/home/");
    },
    leaveWaitingroom() {
      signalRService.leaveWaitingroom();
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
nav {
  margin-bottom: 0.5rem;
}
.waiting-room {
  background-color: #c6a7f3;
  border-radius: 0.25rem;
  box-shadow: 0 1px 0 rgba(4, 4, 5, 0.2);
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  margin: 0.5rem;
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
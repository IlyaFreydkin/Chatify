<script setup>
import signalRService from '../services/SignalRService.js';
</script>

<template>
    <div class="chatDemoView">
        <h5>Demo für einen Textchat mit SignalR</h5>
        <p>Mache einen 2. Tab auf und melde dich unter einem anderen User an. Wenn du in der
            Datei appsettings.Development.json der API deinen User als Suchuser hinterlegt hast,
            kannst du jedes Passwort verwenden.</p>
        <div class="newMessage">
            <textarea v-model="newMessage"></textarea>
            <button class="btn btn-primary btn-sm" v-on:click="sendMessage()">Senden</button>
        </div>
        <div class="message" v-for="m in messages" v-bind:key="m">
            {{ m }}
        </div>
    </div>
</template>

<style scoped>
.newMessage {
    display: flex;
    gap: 1rem;
    align-items: flex-end;
}
.newMessage textarea {
    flex-grow: 1;
}

</style>

<script>
export default {
    data() {
        return {
            messages: [],
            newMessage: ""
        };
    },
    async mounted() {
        try {
            signalRService.configureConnection(this.$store.state.userdata.token);
            signalRService.subscribeEvent('ReceiveMessage', this.onMessageReceive);
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
        }
    }
};
</script>
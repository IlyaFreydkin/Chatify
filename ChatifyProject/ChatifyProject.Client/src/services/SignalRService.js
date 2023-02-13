import process from 'node:process'
import * as SignalR from '@microsoft/signalr'

/**
 * SignalRService
 * A facade for the server/client communication over the SignalR protocol.
 */
class SignalRService {
    constructor() {
        this.connected = false;
    }
    configureConnection(token) {
        const host = process.env.NODE_ENV == 'production' ? "/chatHub" : "https://localhost:5001/chatHub";
        // See https://learn.microsoft.com/en-us/javascript/api/@microsoft/signalr/hubconnectionbuilder?view=signalr-js-latest
        this.connection = new SignalR.HubConnectionBuilder()
            .withUrl(`${host}`, { accessTokenFactory: () => token })
            .withAutomaticReconnect()
            .configureLogging(SignalR.LogLevel.Information)
            .build();
    }
    async connect() {
        await this.connection.start();
        this.connected = true;
    }
    subscribeEvent(type, callback) {
        this.connection.on(type, callback);
    }
    unsubscribeEvent(type, callback) {
        if (typeof callback === 'undefined')
            this.connection.off(type);
        else
            this.connection.off(type, callback);
    }
    async sendMessage(message) {
        if (!this.connected) { throw new Error("Invalid state. Not connected."); }
        // SendMessage is corresponding to the C# Method in ChessHub.
        await this.connection.invoke("SendMessage", message);
    }

}

// Export a singleton (only 1 instance in the spa to make state management easier)
const signalRSerivce = new SignalRService();
export default signalRSerivce;

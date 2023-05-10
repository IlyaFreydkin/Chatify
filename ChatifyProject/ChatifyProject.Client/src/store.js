import { createStore } from 'vuex'

export default createStore({
    state() {
        return {
            userdata: {
                username: "",
                guid: "",
                isLoggedIn: false
            }
        }
    },
    mutations: {
        authenticate(state, userdata) {
            if (!userdata) {
                state.userdata = {};
                return;
            }
            state.userdata = userdata;
        }
    }
});

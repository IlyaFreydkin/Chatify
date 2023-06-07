import { createRouter, createWebHistory } from 'vue-router'
import store from '../store.js'
import HomeView from '../views/HomeView.vue'
import ChatRoomView from '../views/ChatRoomView.vue'
import ChatRoom from '../components/ChatRoom.vue'
import Contact from '../views/ContactView.vue'
import Register from '../views/RegisterView.vue'
import Login from '../views/LoginView.vue'
import WebRTC from '../views/RtcChatView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/chatroom',
      name: 'chatroom',
      meta: { authorize: true },
      component: ChatRoomView
    },
    {
      path: '/chatroom/:user', // Anpassung: Parameter ":user" für die Benutzer-ID
      name: 'chatroom/:user',
      meta: { authorize: true },
      component: ChatRoom
    },
    {
      path: '/contact',
      name: 'contact',
      meta: { authorize: false },
      component: Contact
    }, 
    {
      path: '/register',
      name: 'register',
      meta: { authorize: false },
      component: Register
    },
    {
      path: '/login',
      name: 'login',
      meta: { authorize: false },
      component: Login
    },
    {
      path: '/webrtc',
      name: 'webrtc',
      meta: { authorize: true }, //muss später auf true gesetzt werden
      component: WebRTC
    },
  ]
});

router.beforeEach((to, from, next) => {
  const authenticated = store.state.userdata.username ? true : false;
  if (to.meta.authorize && !authenticated) {
    next("/");
    return;
  }
  next();
  return;
});


export default router

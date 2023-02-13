import { createRouter, createWebHistory } from 'vue-router'
import store from '../store.js'
import HomeView from '../views/HomeView.vue'
import ChatDemoView from '../views/ChatDemoView.vue'
import RtcChatView from '../views/RtcChatView.vue'
import WaitingRoomVue from '../views/WaitingRoomView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/chatdemo',
      name: 'chatdemo',
      meta: { authorize: true },
      component: ChatDemoView
    },
    {
      path: '/rtcchat',
      name: 'rtcchat',
      meta: { authorize: true },
      component: RtcChatView
    },
    {
      path: '/waitingroom',
      name: 'waitingroom',
      meta: { authorize: true },
      component: WaitingRoomVue
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

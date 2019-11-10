import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Maps from '../views/Maps.vue'
import History from '../views/History.vue'
import Factions from '../views/Factions.vue'
import Heroes from '../views/Heroes.vue'
import Secrets from '../views/Secrets.vue'
import Remote from '../views/Remote.vue'
import RemoteScreen from '../views/RemoteScreen.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/maps',
    name: 'maps',
    component: Maps
  },
  {
    path: '/history',
    name: 'history',
    component: History
  },
  {
    path: '/factions',
    name: 'factions',
    component: Factions
  },
  {
    path: '/heroes',
    name: 'heroes',
    component: Heroes
  },
  {
    path: '/73 65 63 72 65 74',
    name: 'secret',
    component: Secrets
  },
  {
    path: '/remote',
    name: 'remote',
    component: Remote,
    meta: { blank: true }
  },
  {
    path: '/remote-screen',
    name: 'remote-screen',
    component: RemoteScreen,
    meta: { blank: true }
  }
]

const router = new VueRouter({
  routes
})

export default router

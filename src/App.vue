<template>
  <div id="main-app">
    <div v-if="this.$route.meta.blank" id="blank-container">
      <router-view />
    </div>
    <div v-else id="main-container">
      <div id="app">
        <b-link class="abrade" to="/73 65 63 72 65 74">.</b-link>
        <div class="page-content">
          <transition
            name="fade"
            mode="out-in"
            v-on:before-leave="beforeLeave"
            v-on:enter="enter"
            v-on:after-enter="afterEnter"
          >
            <router-view />
          </transition>
        </div>
        <br />
        <div class="nav-content">
          <b-nav pills align="center">
            <b-nav-item to="/" exact exact-active-class="active">Home</b-nav-item>
            <b-nav-item to="/maps" exact exact-active-class="active">Maps</b-nav-item>
            <b-nav-item to="/history" exact exact-active-class="active">History</b-nav-item>
            <b-nav-item to="/factions" exact exact-active-class="active">Factions</b-nav-item>
            <b-nav-item to="/heroes" exact exact-active-class="active">Heroes</b-nav-item>
          </b-nav>
        </div>
        <br />
      </div>
    </div>
  </div>
</template>

<style lang="scss">
html, body, #main-app, #blank-container {
  height: 100%;
}

#main-container {
  height: 100%;
  background: linear-gradient(rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7)), url('assets/images/site-background.jpg');
  background-position: center;
  background-repeat: repeat-y;
  background-size: cover;
}

#app {
  max-height: 100%;
  display: flex;
  flex-direction: column;
  justify-items: center;
  align-items: center;
  position: relative;
  top: 50%;
  transform: translateY(-50%);
}

.page-content {
  flex: 0 1 auto;
  overflow-y: auto;
  width: 100%;
  margin-top: 15px;
}

.nav-content {
  flex: 0 0 auto;
}

.fade-enter-active,
.fade-leave-active {
  transition-duration: 0.5s;
  transition-property: height, opacity;
  transition-timing-function: ease;
  overflow: hidden;
}

.fade-enter,
.fade-leave-active {
  opacity: 0
}

.abrade {
  position: absolute;
  top: 0%;
  right: 0%;
  color: transparent;
  &:hover {
    color: white;
  }
}
</style>

<script>
export default {
  name: 'App',
  data () {
    return {
      prevHeight: 0
    }
  },
  methods: {
    beforeLeave (element) {
      this.prevHeight = getComputedStyle(element).height
    },
    enter (element) {
      const { height } = getComputedStyle(element)

      element.style.height = this.prevHeight

      setTimeout(() => {
        element.style.height = height
      })
    },
    afterEnter (element) {
      element.style.height = 'auto'
    }
  }
}
</script>

<template>
  <div class="remote-screen text-center">
    <transition name="fade">
      <b-img
        v-if="image.id === -1"
        :key="image.id"
        id="remote-image"
        blank
        blank-color="black"
      ></b-img>
      <b-img
        v-else
        id="remote-image"
        :key="image.id"
        :src="'data:image/' + image.extension + ';base64, ' + image.data"
        :class="imageClass"
      ></b-img>
    </transition>
  </div>
</template>

<style lang="scss" scoped>
html, body {
  height: 100%;
}

.remote-screen {
  height: 100%;
  background-color: black;
}

.full-width {
  width: 100%;
  height: auto;
}

.full-height {
  height: 100%;
  width: auto;
}
</style>

<script>
export default {
  name: 'remote-screen',
  data () {
    return {
      image: { id: -1 },
      imageAspectRatio: 0,
      viewport: {
        width: 0,
        height: 0
      }
    }
  },
  computed: {
    viewportAspectRatio () {
      return this.viewport.width / this.viewport.height
    },
    imageClass () {
      return this.imageAspectRatio > this.viewportAspectRatio ? 'full-width' : 'full-height'
    }
  },
  created () {
    var self = this
    let url = 'ws://' + process.env.VUE_APP_API_ROOT.split('http://')[1] + '/remote/ws'
    let ws = new WebSocket(url)
    ws.addEventListener('message', function (event) {
      self.image = { id: -1 }
      var json = JSON.parse(event.data)
      let img = new Image()
      img.onload = () => {
        self.imageAspectRatio = img.width / img.height
        self.image = json
      }
      img.src = 'data:image/' + json.extension + ';base64, ' + json.data
    })
    window.addEventListener('resize', this.handleResize)
    this.handleResize()
  },
  destroyed () {
    window.removeEventListener('resize', this.handleResize)
  },
  methods: {
    handleResize () {
      this.viewport.width = window.innerWidth
      this.viewport.height = window.innerHeight
    }
  }
}
</script>

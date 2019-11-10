<template>
  <div class="remote">
    <b-container>
      <b-row class="first-row text-center">
        <b-col>
          <b-form-file
            v-model="imageToAdd"
            placeholder="Choose"
            drop-placeholder="Drop file here..."
            size="lg"
          ></b-form-file>
        </b-col>
        <b-col>
          <b-button variant="outline-success" @click="addImage" :disabled="imageToAdd == null" size="lg">Add Image</b-button>
        </b-col>
      </b-row>
      <br />
      <b-row class="text-center">
        <b-col>
          <b-button variant="outline-light" @click="setActiveImage(-1)" size="lg">Blackout</b-button>
          <div v-if="remoteImages.filter(x => x.isActive).length === 0">
            <span style="color: green;">Blackout is active</span>
          </div>
        </b-col>
      </b-row>
      <br />
      <div v-if="imagesLoading" class="text-center">
        <b-spinner label="Loading..." variant="light"></b-spinner>
      </div>
      <div v-else v-for="item in remoteImages" :key="item.id">
        <b-row align-v="center">
          <b-col>
            <b-img thumbnail right height="50" width="auto" @click="setActiveImage(item.id)" :src="'data:image/' + item.extension + ';base64, ' + item.data"></b-img>
          </b-col>
          <b-col class="white">
            <div v-if="activeStateLoading">
              <b-spinner label="Loading..." variant="light"></b-spinner>
            </div>
            <div v-else-if="item.isActive">
              <span style="color: green;">This image is the active one.</span>
            </div>
            <div v-else>
              <b-button variant="outline-danger" @click="deleteImage(item.id)" size="lg">Delete</b-button>
            </div>
          </b-col>
        </b-row>
        <br />
      </div>
    </b-container>
  </div>
</template>

<style lang="scss" scoped>
.remote {
  background-color: black;
  height: 100%;
  overflow-y: scroll;
}

.first-row {
  padding-top: 15px;
}
</style>

<script>
var axios = require('axios')

export default {
  name: 'remote',
  data () {
    return {
      remoteImages: [],
      imagesLoading: false,
      activeStateLoading: false,
      imageToAdd: null
    }
  },
  created () {
    this.getImages()
  },
  methods: {
    getImages () {
      var self = this
      self.imagesLoading = true
      axios.get(process.env.VUE_APP_API_ROOT + '/remote').then(function (response) {
        self.remoteImages = response.data
        self.imagesLoading = false
      })
    },
    addImage () {
      var self = this
      let formData = new FormData()
      formData.append('file', self.imageToAdd)
      axios.post(process.env.VUE_APP_API_ROOT + '/remote', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      }).then(function (response) {
        self.remoteImages.push(response.data)
      })
    },
    setActiveImage (id) {
      var self = this
      self.activeStateLoading = true
      axios.put(process.env.VUE_APP_API_ROOT + '/remote/' + id).then(function (response) {
        self.remoteImages = self.remoteImages.map(x => {
          x.isActive = false
          return x
        })
        if (id !== -1) {
          self.remoteImages.filter(x => x.id === id)[0].isActive = true
        }
        self.activeStateLoading = false
      })
    },
    deleteImage (id) {
      var self = this
      self.remoteImages = self.remoteImages.filter(x => x.id !== id)
      axios.delete(process.env.VUE_APP_API_ROOT + '/remote/' + id)
    }
  }
}
</script>

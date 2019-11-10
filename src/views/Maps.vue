<template>
  <div class="maps">
    <h1 class="text-center white">Maps</h1>
    <b-container>
      <b-row>
        <b-col>
          <b-card>
            <h2 class="white">Aepetria proper</h2>
            <p class="white">Shown are the six nations of Aepetria, along with a few other locations of note.</p>
            <b-img src="@/assets/images/maps/Aepetria_cropped_small.png" fluid-grow></b-img>
          </b-card>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<style lang="scss" scoped>
.card {
  border: 1px solid white;
  background: none;
}
</style>

<script>
export default {
  name: 'maps'
}
</script>

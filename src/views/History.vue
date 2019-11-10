<template>
  <div class="history">
    <b-container class="white">
      <h1 class="text-center">History</h1>
      <b-row>
        <b-col>
          <h2 class="text-center">Discovery</h2>
          <b-img src="@/assets/images/history/discovery.jpg" center rounded></b-img>
          <p>
            Long ago, brave explorers motivated by rumors of new lands containing untold riches sailed to what is now the west coast of Sylvetika.
            To their dismay, the land was teeming with bountiful wildlife and fertile land, but no precious metals with which to claim their fortune.
            This didn't stop others from arriving in Aepetria, and soon many ships were arriving each month at the newly found port town in Sylvetika.
          </p>
          <h2 class="text-center">Exploration</h2>
          <b-img src="@/assets/images/history/exploration.jpg" center rounded></b-img>
          <p>
            The promise of plentiful riches drove determined souls to the far reaches of Aepetria. To the north, they found precious little in terms of
            either hospitable land or precious metals. Adventurers heading east received a stroke of luck a few months into the initial voyages, and
            found traces of gold in one of the many riverbeds in Sylvetika. As one can imagine, this sparked an impetuous gold rush among the explorers,
            and soon everyone was making as much eastward progress as possible.
          </p>
          <h2 class="text-center">Colonization</h2>
          <b-img src="@/assets/images/history/colonization.jpg" center rounded></b-img>
          <p>
            As the gold rush continues to rage forth, many who have either given up the chase or had their fill have set up settlements and towns to host
            weary travelers on their quests to acquire their riches, and facilitate trade among Aepetria. An organization from the west called the Teremor
            has come in to try and establish some semblance of law and order, but as one can imagine, progress is slow-moving.
          </p>
          <h2 class="text-center">Present Day</h2>
          <b-img src="@/assets/images/history/present.jpg" center rounded></b-img>
          <p>
            Many years have passed and the gold rush is still going strong, with new adventurers arriving day by day in Sylvetika. Folks have made it as
            far as Fatemond, and they tell of treacherous isles flush with gold and no rulers still to the east.
          </p>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<style lang="scss" scoped>
img {
  width: 50%;
}
</style>

<script>
export default {
  name: 'history'
}
</script>

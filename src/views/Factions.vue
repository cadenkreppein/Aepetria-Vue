<template>
  <div class="factions">
    <b-container class="white">
      <h1 class="text-center">Factions</h1>
      <b-row>
        <b-col>
          <p>
            As Aepetria's story unfolds, different factions are struggling to find their roles. Listed below are some important factions currently at
            odds with each other within Aepetria.
          </p>
          <h2 class="text-center">Explorers</h2>
          <b-img src="@/assets/images/factions/explorers.jpg" center rounded></b-img>
          <p>
            The promise of wealth from Aepetria has drawn many an explorer, adventurer, plunderer, and cartographer to make a name for themselves. These
            individuals are either out to strike it rich or simply take in all that Aepetria has to offer.
          </p>
          <h2 class="text-center">Ravens</h2>
          <b-img src="@/assets/images/factions/raven_icons.png" center rounded></b-img>
          <p>
            The Ravens are an organized group of bandits taking advantage of the madness caused by the gold rush. They know that many adventurers are
            ill-prepared to fight against a fellow man, let alone an entire group of them. Sometimes the victims are simply robbed of their valuables and
            left alone, but in many cases an encounter with a group of Ravens signifies one's death. The Ravens are one of the main reasons that some
            sympathize with the plight of the Teremor.
          </p>
          <h2 class="text-center">Teremor</h2>
          <b-img src="@/assets/images/factions/teremor.jpg" center rounded></b-img>
          <p>
            The Teremor are delegates sent from the west in an attempt to establish a central government across Aepetria. Few know if their intentions
            are malicious or not, leading to a variety of opinions concerning them. They handle themselves with the utmost elegance, taking care to
            maintain the appearance of officiality in everything that they do.
          </p>
          <h2 class="text-center">Idealists</h2>
          <b-img src="@/assets/images/factions/idealists.jpg" center rounded></b-img>
          <p>
            The Idealists aim to stop the Teremor from achieving their goal of centralized government. Many of the Idealists were among the first to come
            to Aepetria, and believe that the last thing this new land needs is old ideals. Just like any group, there are moderates that simply disagree
            with the Teremor, and radicals that will go out of their way to actively sabotage their plans.
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

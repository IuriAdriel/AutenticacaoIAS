<template>
  <q-page class="flex flex-center">
    <img
      alt="Quasar logo"
      src="~assets/quasar-logo-vertical.svg"
      style="width: 200px; height: 200px"
    >
    <!-- <div><button type="button" @click="increment">clicou</button> CONTADOR: {{ counter }}</div> -->
  </q-page>
</template>

<script>
import { defineComponent } from 'vue'
// import { useCounterStore } from 'src/stores/example-store'
// import { mapActions, storeToRefs } from 'pinia'

export default defineComponent({
  name: 'IndexPage',
  setup(){
    // const counterStore = useCounterStore();
    // const { counter } = storeToRefs(counterStore);
    // const { increment } = mapActions(useCounterStore, ["increment"]);



    return {
      // counter,
      // increment
    }
  }
})
</script>

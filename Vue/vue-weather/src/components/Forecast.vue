<template>
  <v-card>
      <v-card-title>
          <h5 class="Display-1">Forecast</h5>
      </v-card-title>
      <v-simple-table fixed-header height="300px" dense>
        <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">Datum</th>
              <th class="text-left">Temperatur</th>
              <th class="text-left">Temperatur (min)</th>
              <th class="text-left">Temperatur (max)</th>
              <th class="text-left">Feuchte</th>
            </tr>
          </thead>
          <tbody v-for="item in model" :key="item.DateTime">
              <ForcastItem v-bind:item = "item"/>
          </tbody>    
        </template>
      </v-simple-table>
  </v-card>
</template>

<script>
  import ForcastItem from './ForecastItem'

  export default {
    components: {
      ForcastItem
    },
    props: ["model"]
  }
</script>
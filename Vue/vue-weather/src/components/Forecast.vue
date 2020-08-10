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
          <tbody>
            <tr v-for="item in model" :key="item.DateTime">
              <td>{{ item.DateTime }} </td>
              <td>{{ item.Temperature }} Celsius</td>
              <td>{{ item.MinimumTemperature }} Celsius</td>
              <td>{{ item.MaximumTemperature }} Celsius</td>
              <td>{{ item.Humidity }} %</td>
            </tr>
          </tbody>    
        </template>
      </v-simple-table>
  </v-card>
</template>

<script>
  export default {
    props: ["model"]
  }
</script>
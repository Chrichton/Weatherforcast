<template>
<v-card>
    <v-card-title>
        <h5 class="Display-1">History</h5>
    </v-card-title>
    <v-simple-table fixed-header height="300px" dense>
        <template v-slot:default>
        <thead>
            <tr>
            <th class="text-left">Ort</th>
            <th class="text-left">Temperatur</th>
            <th class="text-left">Feuchte</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item in historyData" :key="item.City">
            <td>{{ item.city }} </td>
            <td>{{ item.temperature }} Celsius</td>
            <td>{{ item.humidity }} %</td>
            </tr>
        </tbody>
        </template>
    </v-simple-table>
</v-card>
</template>

<script>
  export default {
    props: ["historyData"]
  }
</script>
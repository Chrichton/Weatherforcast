<template>
    <v-card>
        <v-card-title>
            <h5 class="Display-1">Aktuelles Wetter</h5>
        </v-card-title>
        <v-spacer>
            {{model.current.Temperature}} Celsius
            {{model.current.MinimumTemperature}} Celsius (min)
            {{model.current.MaximumTemperature}} Celsius (max)
            {{model.current.Humidity}} %
            {{model.current.CloudDescription}}
            {{model.AverageTemperature}} Celsius (avg)
            {{model.AverageHumidity}} (avg) %
    </v-spacer>
    </v-card>
</template>

<script>
export default {
    props: ["model"]
}
</script>
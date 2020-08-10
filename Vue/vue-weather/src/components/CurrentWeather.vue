<template>
    <v-card>
        <v-card-title>
            <h5 class="Display-1">Current Weather</h5>
        </v-card-title>
        <v-spacer>
            {{model.Temperature}} Celsius
            {{model.MinimumTemperature}} Celsius
            {{model.MaximumTemperature}} Celsius
            {{model.Humidity}} %
    </v-spacer>
    </v-card>
</template>

<script>
export default {
    props: ["model"]
}
</script>
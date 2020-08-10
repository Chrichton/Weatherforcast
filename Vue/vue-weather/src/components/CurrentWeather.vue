<template>
    <v-card>
        <v-card-title>
            <h5 class="Display-1 text-center">Aktuelles Wetter</h5>
        </v-card-title>
        <v-spacer class="v-card__text">
            <v-spacer>
                Temperatur:
                {{model.current.Temperature}} Celsius
            </v-spacer>

            <v-spacer>
                Minimale Temperatur: 
                {{model.current.MinimumTemperature}} Celsius
            </v-spacer>

            <v-spacer>
                Maximale Temperatur
                {{model.current.MinimumTemperature}} Celsius
            </v-spacer>
            
             <v-spacer>
                Himmel:
                {{model.current.CloudDescription}}
            </v-spacer>

            <v-spacer>
                Feuchte:
                {{model.current.Humidity}} %
            </v-spacer>

            <v-spacer>
                Mittlere Temperatur:
                {{model.AverageTemperature}} Celsius
            </v-spacer>

            <v-spacer>
                Mittlere Feuchte:
                {{model.AverageHumidity}} %
            </v-spacer>
        </v-spacer>
    </v-card>
</template>

<script>
export default {
    props: ["model"]
}
</script>
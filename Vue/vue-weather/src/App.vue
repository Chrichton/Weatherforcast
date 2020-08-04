<template>
  <div id="app">
    {{weather}}
  </div>
</template>

<script>

export default {
  name: 'App',
  components: {
    
  },
  data() {
    return {
      weather: {
        Average: 42,
        Forecast: [
          {
            Temperature: 15.87,
            FeelsLikeTemperature: 15.46,
            MinimumTemperature: 13.58,
            MaximumTemperature: 15.87,
            Humidity: 76,
            Pressure: 1020,
            Windspeed: 1.46,
            WindDirection: 251,
            CloudDescription: "Bedeckt",
            DateTime: "2020-07-30 21:00:00"
          },
          {
            Temperature: 15.87,
            FeelsLikeTemperature: 15.46,
            MinimumTemperature: 13.58,
            MaximumTemperature: 15.87,
            Humidity: 76,
            Pressure: 1020,
            Windspeed: 1.46,
            WindDirection: 251,
            CloudDescription: "Bedeckt",
            DateTime: "2020-07-30 21:00:00"
          },
          {
            Temperature: 15.87,
            FeelsLikeTemperature: 15.46,
            MinimumTemperature: 13.58,
            MaximumTemperature: 15.87,
            Humidity: 76,
            Pressure: 1020,
            Windspeed: 1.46,
            WindDirection: 251,
            CloudDescription: "Bedeckt",
            DateTime: "2020-07-30 21:00:00"
          }
        ]
      }
    }
  }
}
</script>

<style>
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

body {
  font-family: Arial, Helvetica, sans-serif;
  line-height: 1.4;
}
</style>

<template>
  <div id="app">
    <Header/>
    <SelectCity v-on:load-weather="loadWeather"/>
    <Weather v-bind:model="Model"/>
  </div>
</template>

<script>
import Header from './components/layout/Header';
import Weather from './components/Weather';
import SelectCity from './components/SelectCity';
import axios from 'axios';

export default {
  name: 'App',
  components: {
    Header,
    Weather,
    SelectCity
  },
  data() {
    return {
      Model: /*{
        Average: 42,
        Forecast: */[
          {
            Temperature: 14.31,
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
            Temperature: 14.31,
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
            Temperature: 14.31,
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
     // } 
    }
  },
  methods: {
    loadWeather (city) {
        console.log(city);

        axios.get('https://jsonplaceholder.typicode.com/todos')
        // .then(res => this.Model = res.data)
        // .catch(err => console.log(err));
        
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

.btn {
  display: inline-block;
  border: none;
  background: #555;
  color: #fff;
  padding: 7px, 20px;
  cursor: pointer;
}

.btn:hover {
  background: #666;
}
</style>

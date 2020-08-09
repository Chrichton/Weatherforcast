<template>
  <v-app>
    <div id="app">
      <Header/>
      <TestVuetify v-bind:model="Model.forecast"/>
      <SelectCity v-on:load-weather="loadWeather"/>
      <CurrentWeather v-bind:model="Model.current"/>
      <Forecast v-bind:model="Model.forecast"/>
      <History v-bind:historyData="historyData"/>
    </div>
  </v-app>
</template>

<script>
import TestVuetify from './components/TestVuetify'
import Header from './components/layout/Header';
import SelectCity from './components/SelectCity';
import CurrentWeather from './components/CurrentWeather';
import Forecast from './components/Forecast';
import History from './components/History';

import axios from 'axios';

const historyDataKey = 'historyData';

export default {
  name: 'App',
  components: {
    TestVuetify,
    Header,
    SelectCity,
    CurrentWeather,
    Forecast,
    History
  },
  data() {
    return {
      Model: {
        AverageHumidity: 42,
        AverageTemperature: 64.6,
        current:
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
        forecast:[
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
            DateTime: "2020-07-30 21:00:01"
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
            DateTime: "2020-07-30 21:00:03"
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
            DateTime: "2020-07-30 21:00:04"
          }
        ]
      },
      historyData: [
        {
          city:"Hamburg", 
          temperature: 23.96, 
          humidity: 65.95
        },
        {
          city: "Lübeck",
          temperature: 24.3, 
          humidity:42.42
        }
      ]
    }
  },
  mounted() {
    if (localStorage.getItem(historyDataKey)) {
      try {
        this.historyData = JSON.parse(localStorage.getItem(historyDataKey));
      } catch(e) {
        localStorage.removeItem(historyDataKey);
      }
    }
  },
  methods: {
    loadWeather (city, id) {
        console.log(city);

        axios.get(`${process.env.VUE_APP_ROOT_API}id/${id}`)
          .then(res => 
            {
              console.log(res.data);
              this.Model = res.data;
              this.addHistory(city, 
                res.data.AverageTemperature, res.data.AverageHumidity);
            }
          )
          .catch(err => console.log(err));
    },
    addHistory(city, temperature, humidity) {
      console.log(city + ':' + temperature + ':' + humidity)

      const historyItem = {
          id: this.$uuid.v4,
          city: city,
          temperature: temperature,
          humidity: humidity
      };

      this.historyData.push(historyItem);
      this.saveHistory();
    },
    saveHistory() {
      const parsed = JSON.stringify(this.historyData);
      localStorage.setItem(historyDataKey, parsed);
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

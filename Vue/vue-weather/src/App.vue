<template>
  <v-app> 
    <v-spacer id="app">
      <v-container fluid>
          <v-row>
            <v-col cols="12">
              <Header v-bind:city="city"/>
            </v-col>
          </v-row>
       
        <v-row>
          <v-col md="6" lg="6">
            <SelectCity v-on:load-weather="loadWeather"/>
          </v-col>
          <v-col md="6" lg="6">
            <SearchZipcode v-on:load-weather="loadWeather"/>
          </v-col>
        </v-row>
        <v-row 
          :key="1"
          :justify="'center'"
        >
          <v-col
            :key="1"
            md="8"
          >
            <CurrentWeather v-bind:model="Model"/>
            </v-col>
        </v-row>
        <v-row>
          <v-col md="8" lg="8">
            <Forecast v-bind:model="Model.forecast"/>
            
          </v-col>
          <v-col md="4" lg="4">
          <History v-bind:historyData="historyData"/>
          </v-col>
        </v-row>
      </v-container>
    </v-spacer>
  </v-app>
</template>

<script>
import Header from './components/layout/Header';
import SearchZipcode from './components/SearchZipcode'
import SelectCity from './components/SelectCity';
import CurrentWeather from './components/CurrentWeather';
import Forecast from './components/Forecast';
import History from './components/History';

import axios from 'axios';

const historyDataKey = 'historyData';

export default {
  name: 'App',
  components: {
    Header,
    SearchZipcode,
    SelectCity,
    CurrentWeather,
    Forecast,
    History
  },
  data() {
    return {
      city: "Header",
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
              this.city = city;
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

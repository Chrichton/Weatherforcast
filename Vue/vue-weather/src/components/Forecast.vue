<template>
    <div> 
        <h3>Forecast</h3>
        <div v-for="forecast in model" :key="forecast.DateTime">
            <ForecastItem v-bind:forecast="forecast"/>
        </div>
    </div>    
</template>

<script>
import ForecastItem from './ForecastItem';

export default {
   name: "Weather",
   components: {
       ForecastItem
   },
   props: ["model"]
}
</script>

<style scoped>

</style>
<template>
    <div> 
        <div v-for="forecast in model.forecast" :key="forecast">
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
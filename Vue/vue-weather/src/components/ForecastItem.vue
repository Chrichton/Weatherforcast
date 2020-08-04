<template>
    <div class="forecast-item"> 
        <p>
            {{forecast.Temperature}}
            {{forecast.MinimumTemperature}}
            {{forecast.MaximumTemperature}}

        </p>
    </div>
</template>

<script>
export default {
    name: "ForecastItem",
    props: ["forecast"]
}
</script>

<style scoped>
    .forecast-item {
        background: #f4f4f4;
        padding: 10px;
        border-bottom: 1px #ccc dotted;
    }
</style>

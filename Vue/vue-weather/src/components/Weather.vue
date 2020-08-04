<template>
    <div>
        <div v-for="weather in model" :key="weather">
            <h3>{{weather}}</h3>
        </div>
    </div>    
</template>

<script>
export default {
   name: "Weather",
   props: ["model"]
}
</script>

<style scoped>

</style>
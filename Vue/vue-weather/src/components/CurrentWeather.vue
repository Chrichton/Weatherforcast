<template>
    <div>
        <h1>Current Weather</h1>
        <p>
            {{model.Temperature}}
            {{model.MinimumTemperature}}
            {{model.MaximumTemperature}}
            {{model.Humidity}}
        </p>
    </div>
</template>

<script>
export default {
    props: ["model"]
}
</script>

<style lang="less" scoped>

</style>
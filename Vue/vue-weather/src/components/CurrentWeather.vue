<template>
    <div>
        <h1>Current Weather</h1>
        <p>
            {{model.current.Temperature}}
            {{model.current.MinimumTemperature}}
            {{model.current.MaximumTemperature}}
            {{model.current.Humidity}}
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
<template>
    <tr>
        <td>{{ date }} </td>
        <td>{{ item.Temperature }} Celsius</td>
        <td>{{ item.MinimumTemperature }} Celsius</td>
        <td>{{ item.MaximumTemperature }} Celsius</td>
        <td>{{ item.Humidity }} %</td>   
    </tr>
</template>

<script>
export default {
    name: "ForecastItem",
    props: ["item"],
    computed: {
    date: function() {
      return this.item.DateTime
    }
  },
}
</script>
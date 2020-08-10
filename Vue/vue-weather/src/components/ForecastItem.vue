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
        // const datetime = Date.parse(this.item.DateTime)
        // console.log(datetime)

        // const date = Date(this.item.DateTime)
        // const year = date.getYear()
        // console.log(year)

        return this.item.DateTime
        // return datetime.getYear + datetime.getMonth + datetime.getDay + " " 
        //     + datetime.getHours + ":" + datetime.getMinutes + ":" + datetime.getSeconds;
    }
  },
}
</script>
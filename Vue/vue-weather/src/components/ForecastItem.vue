<template>
    <tr>
        <td>{{ getDate }} </td>
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
    getDate: function() {
        const d = new Date(this.item.DateTime);
        const hours = d.getHours();
        const day = d.getDate();
        const month = d.getMonth() + 1; //Months are zero based
        const year = d.getFullYear();
        const combinedDate = `${day}.${month}.${year} ${hours}:00`;

        return combinedDate;
    }
  },
}
</script>
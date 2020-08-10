<template>
 <tr>
    <td>{{ item.name }}</td>
    <td>{{ item.calories }}</td>
    <td>{{ COMPUTED_PROPERTY }}</td>
    <td>{{ item.iron }}</td>
  </tr>   
</template>

<script>

export default {
    name: "TestVuetifyVueItem",
    props: {
    item: {
      type: Object,
      required: true,
    }
  },
  computed: {
    COMPUTED_PROPERTY: function() {
      return this.item.fat +
        this.item.carbs +
        this.item.protein
    }
  },
}
</script>
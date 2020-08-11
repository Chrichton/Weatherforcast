<template>
<v-card>
    <v-card-title>
      Suche Ort nach Postleitzahl
    </v-card-title>
    <v-card-text>
        <v-autocomplete
        v-model="model"
        :items="items"
        :loading="isLoading"
        :search-input.sync="search"
        chips
        clearable
        hide-details
        hide-selected
        item-text="zipcode"
        item-value="city"
        label="Gebe Postleitzahl ein"
        solo
        >
        <template v-slot:selection="{ attr, on, item, selected }">
            <v-chip
            v-bind="attr"
            :input-value="selected"
            color="blue-grey"
            class="white--text"
            v-on="on"
            >
            <span v-text="item.city"></span>
            </v-chip>
        </template>
        <template v-slot:item="{ item }">
            <v-list-item-content>
            <v-list-item-title v-text="item.zipcode"></v-list-item-title>
            <v-list-item-subtitle v-text="item.city"></v-list-item-subtitle>
            </v-list-item-content>
        </template>
        </v-autocomplete>
    </v-card-text>
    <v-divider></v-divider>
  </v-card>
</template>

<script>
  import axios from 'axios'

  export default {
    data: () => ({
      isLoading: false,
      items: [],
      model: null,
      search: null,
    }),

    watch: {
      model (city) {
        if (city != null) {
            const item = this.items.filter(item => item.city === city)
            const id = item[0].id
            this.$emit('load-weather', city, id)

            this.$nextTick(() => {
                this.model = null
            })
        }
      },
      search () {
        // Items have already been loaded
        if (this.items.length > 0) return

        if (!this.search || this.search.length != 5) return

        this.isLoading = true

        // Lazily load input items
        axios.get('http://vueweatherapi.azurewebsites.net/api/weather/forecast/zipcode/' + this.search)
          .then(res => {
            this.items = res.data.result.map(data => ({
                zipcode: this.search,
                city: data.Key,
                id: data.Value
            }))
          })
          .catch(err => {
            console.log(err)
          })
          .finally(() => (this.isLoading = false))
      },
    },
  }
</script>
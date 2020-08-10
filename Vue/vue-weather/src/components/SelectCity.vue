<template>
   <v-card>
    <v-card-title>
      Suche Ort
    </v-card-title>
    <v-card-text>
      <v-autocomplete
        v-model="model"
        :items="items"
        :loading="isLoading"
        :search-input.sync="search"
        hide-no-data
        hide-selected
        item-text="Description"
        item-value="API"
        label="Ort"
        placeholder="Finde Orte"
        prepend-icon="mdi-database-search"
        return-object
      ></v-autocomplete>
    </v-card-text>
    <v-divider></v-divider>
  </v-card>
</template>

<script>

import axios from 'axios';

export default {
    name: "SelectCity",
    data:() => ({
            city: '',
            id: 0,
            descriptionLimit: 60,
            entries: [],
            isLoading: false,
            model: null,
            search: null,
        }),
    computed: {
      fields () {
        if (!this.model) return []

        return Object.keys(this.model).map(key => {
          return {
            key,
            value: this.model[key] || 'n/a',
          }
        })
      },
      items () {
        return this.entries.map(entry => {
          const Description = entry.Key.length > this.descriptionLimit
            ? entry.Key.slice(0, this.descriptionLimit) + '...'
            : entry.Key

          return Object.assign({}, entry, { Description })
        })
      },
    },
    watch: {
      fields () {
        if (this.model) {
          this.$emit('load-weather', this.model.Key, this.model.Value)
        }
      },
      search () {
        if (!this.search) return

       // if (this.search.length > 1) return  // TODO should be active. 
                                              // Otherwise after every keystroke a request is made
                                              // Unfortunately when active the searchtext is not cleared
                                              // Did not find a way to clear it automatically
                                              // Have to investigate

        if (this.model != null)
        {
          this.model = null;
        }
        
        this.isLoading = true

        // Lazily load input items
        axios.get('http://vueweatherapi.azurewebsites.net/api/weather/forecast/cities/' + this.search)
          .then(res => {
            this.count = res.data.result.length
            this.entries = res.data.result
          })
          .catch(err => {
            console.log(err)
          })
          .finally(() => (this.isLoading = false))
      },
    },
}
</script>
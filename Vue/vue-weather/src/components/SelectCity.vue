<template>
   <v-card
    color="red lighten-2"
    dark
  >
    <v-card-title class="headline red lighten-3">
      Suche Ort
    </v-card-title>
    <v-card-text>
      Finde Orte
    </v-card-text>
    <v-card-text>
      <v-autocomplete
        v-model="model"
        :items="items"
        :loading="isLoading"
        :search-input.sync="search"
        color="white"
        hide-no-data
        hide-selected
        item-text="Description"
        item-value="API"
        label="Ort"
        placeholder="Start typing to Search"
        prepend-icon="mdi-database-search"
        return-object
      ></v-autocomplete>
    </v-card-text>
    <v-divider></v-divider>
    <v-expand-transition>
      <v-list v-if="model" class="red lighten-3">
        <v-list-item
          v-for="(field, i) in fields"
          :key="i"
        >
          <v-list-item-content>
            <v-list-item-title v-text="field.value"></v-list-item-title>
            <v-list-item-subtitle v-text="field.key"></v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-expand-transition>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn
        :disabled="!model"
        color="grey darken-3"
        @click="model = null"
      >
        Clear
        <v-icon right>mdi-close-circle</v-icon>
      </v-btn>
    </v-card-actions>
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
    methods: {
        selectCity() {
            this.$emit('load-weather', this.id)
        }
    },
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
        // Items have already been loaded
        // if (this.items.length > 0) return

        // // Items have already been requested
        // if (this.isLoading) return

        if (this.search && this.search.length > 1) return

        if (this.model != null)
        {
          this.model = null;
          this.entries = [];
          this.search = null;
        }
        
        this.isLoading = true

        // Lazily load input items
        axios.get('http://vueweatherapi.azurewebsites.net/api/weather/forecast/cities/' + this.search)
          .then(res => {
            this.count = 1500
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
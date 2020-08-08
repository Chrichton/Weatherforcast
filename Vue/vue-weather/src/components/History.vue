<template>
    <div> 
        <h3>History</h3>
        <div v-for="historyItem in historyData" :key="historyItem.id">
            <HistoryItem v-bind:historyItem="historyItem"/>
        </div>
    </div>    
</template>

<script>
import HistoryItem from './HistoryItem';

export default {
   name: "History",
   components: {
       HistoryItem
   },
   props: ["historyData"]
}
</script>

<style scoped>

</style>
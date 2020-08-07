<template>
    <div class="history-item"> 
        <p>
            {{historyItem.city}}
            {{historyItem.temperature}}
            {{historyItem.humidity}}

        </p>
    </div>
</template>

<script>
export default {
    name: "HistoryItem",
    props: ["historyItem"]
}
</script>

<style scoped>
    .forecast-item {
        background: #f4f4f4;
        padding: 10px;
        border-bottom: 1px #ccc dotted;
    }
</style>

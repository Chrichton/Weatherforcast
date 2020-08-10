<template>
    <v-container>
        <h2 class="headline font-weight-bold mb-3 text-center">
            {{city}}
        </h2>
    </v-container>
</template>

<script>
export default {
    name: "Header",
    props: ["city"]
}
</script>
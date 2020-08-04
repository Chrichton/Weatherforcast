<template>
    <div>
        <form @submit="selectCity">
            <input type="text" v-model="city" name="city" placeholder="City"/>
            <input type="submit" value="Submit" class="btn"/>
        </form>
    </div>
</template>

<script>
export default {
    name: "SelectCity",
    data() {
        return {
            city: ''
        }
    },
    methods: {
        selectCity(e) {
            e.preventDefault();
            console.log(this.city);
        }
    }
}
</script>

<style scoped>
    form {
        display: flex;
    }

    input[type="text"] {
        flex: 10;
        padding: 5px;
    }

    input[type="submit"] {
        flex: 2;
    }

</style>
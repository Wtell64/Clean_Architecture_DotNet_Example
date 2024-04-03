<script setup>
    import { state } from '@/stores/global.state'
    import { onMounted } from 'vue'
    import { useCounterStore } from '@/stores'

    const counterStore = useCounterStore()

    onMounted(() => {
        state.count = 5
    })
</script>
<template>
    <div class="empty">
        <p class="empty-title">Global State: {{ state.count }}</p>
        <button class="btn btn-primary" @click="state.count++">Increment Global State</button>

        <hr />

        <p class="empty-title">Counter Store: {{ counterStore.count }}</p>
        <button class="btn btn-primary" @click="counterStore.count++">Increment Counter Store</button>

        <RouterLink to="/">Home</RouterLink>
    </div>
</template>

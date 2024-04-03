<script setup>
    import { state } from '@/stores/global.state'
    import { useCounterStore } from '@/stores'
    const counterStore = useCounterStore()
</script>
<template>
    <div class="empty">
        <div class="empty-img">
            <img src="/tabler/static/illustrations/undraw_people_re_8spw.svg" height="128" alt="">
        </div>
        <p class="empty-title">Welcome to CRM App</p>

        <p class="empty-title">Global State: {{ state.count }}</p>
        <p class="empty-title">Counter Store: {{ counterStore.count }}</p>

        <p class="empty-subtitle text-muted">
            Try adjusting your search or filter to find what you're looking for.
        </p>
        <div class="empty-action">
            <RouterLink to="/customers" class="btn btn-primary">
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-user" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round"><path stroke="none" d="M0 0h24v24H0z" fill="none" /><path d="M8 7a4 4 0 1 0 8 0a4 4 0 0 0 -8 0" /><path d="M6 21v-2a4 4 0 0 1 4 -4h4a4 4 0 0 1 4 4v2" /></svg>
                Add your first customer
            </RouterLink>
        </div>
    </div>
</template>

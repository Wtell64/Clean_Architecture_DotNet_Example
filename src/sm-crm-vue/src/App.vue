<script setup>
    import '@/plugins/axios'
    import '@/plugins/notification'
    import Loader from '@/components/Loader.vue'
    import { useLayoutStore } from '@/stores'
    import { storeToRefs } from 'pinia'

    const layoutStore = useLayoutStore()
    const { isPageLoading } = storeToRefs(layoutStore)
</script>
<template>
    <Loader :visible="layoutStore.isPageLoading" />
    <RouterView/>
</template>
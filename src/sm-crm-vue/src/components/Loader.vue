<script setup>
const props = defineProps(['visible'])
</script>

<template>
    <div class="page page-center" id="loader" v-show="props.visible">
        <div class="container container-slim py-4">
            <div class="text-center bg-white rounded-3 p-5">
                <div class="mb-3">
                    <a href="." class="navbar-brand navbar-brand-autodark">
                        <img src="/tabler/static/logo.svg" height="36" alt="">
                    </a>
                </div>
                <div class="text-muted mb-3">
                    <slot>Page is loading...</slot>
                </div>
                <div class="progress progress-sm">
                    <div class="progress-bar progress-bar-indeterminate"></div>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
    #loader {
        position: absolute;
        inset: 0;
        z-index: 100;
        background: rgba(0, 0, 0, .4);
    }
</style>
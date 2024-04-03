<script setup>
    import Footer from '@/components/Footer.vue';
    import Navbar from '@/components/Navbar.vue';
    import { RouterView } from 'vue-router';
</script>

<template>
    <div class="page">
        <Navbar />
        <div class="page-wrapper">
            <RouterView />
            <Footer />
        </div>
    </div>
</template>
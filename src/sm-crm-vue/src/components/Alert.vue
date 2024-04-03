<script setup>
const props = defineProps(['type'])
const emits = defineEmits(['onclose'])

/*
Kullanımı:
<Alert type="info" @onclose="isVisible=false" v-if="isVisible">
    <template #title>Başlık 1</template>
    Mesaj 1
</Alert>
*/
</script>

<template>
    <div class="alert" :class="'alert-' + props.type">
        <h5><slot name="title" /></h5>
        <slot />

        <br>
        <button class="btn btn-sm btn-danger"
                @click="emits('onclose')">
            Close
        </button>
    </div>
</template>

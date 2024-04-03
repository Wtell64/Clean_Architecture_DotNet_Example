<script setup>
    defineProps(['data'])
    const emit = defineEmits(['onPageChanged'])
    let pageOffset = 20

    function changePage(page) {
        emit('onPageChanged', page)
    }

    // custom range function creates a range from start to end
    function range(start, end) {
        if (end > pageOffset && end - start < pageOffset) start = end - pageOffset + 1;
        return Array.from({ length: end - start + 1 }, (_, index) => start + index);
    }
</script>
<template>
    <div class="card-footer d-flex align-items-center" v-if="data">
        <p class="m-0 text-muted">Showing <span>{{ data.offsetFrom }}</span> to <span>{{ data.offsetTo }}</span> of <span>{{ data.totalItems }}</span> entries</p>
        <ul class="pagination m-0 ms-auto">
            <li class="page-item" :class="{ 'disabled': !data.hasPreviousPage }">
                <button class="page-link" @click="changePage(1)">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                        <path d="M15 6l-6 6l6 6" />
                    </svg>
                    first
                </button>
            </li>
            <li class="page-item" :class="{ 'disabled': !data.hasPreviousPage }">
                <button class="page-link" @click="changePage(data.page - 1)">
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                        <path d="M15 6l-6 6l6 6" />
                    </svg>
                    prev
                </button>
            </li>

            <li v-if="data" v-for="p in range(data.page, Math.min(data.totalPages, data.page + 19))" class="page-item" :key="p" :class="{ 'active': p == data.page }">
                <button class="page-link" @click="changePage(p)">{{p}}</button>
            </li>

            <li class="page-item" :class="{ 'disabled': !data.hasNextPage }">
                <button class="page-link" @click="changePage(data.page + 1)">
                    next
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                        <path d="M9 6l6 6l-6 6" />
                    </svg>
                </button>
            </li>
            <li class="page-item" :class="{ 'disabled': !data.hasNextPage }">
                <button class="page-link" @click="changePage(data.totalPages)">
                    last
                    <svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                        <path d="M9 6l6 6l-6 6" />
                    </svg>
                </button>
            </li>
        </ul>
    </div>
</template>
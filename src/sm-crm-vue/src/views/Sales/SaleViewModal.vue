<script setup>
    const props = defineProps(['item'])
</script>
<template>
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" id="viewModal">
        <div class="modal-dialog">
            <div class="modal-content" v-if="item">
                <form @submit.prevent="true">
                    <div class="modal-header">
                        <h5 class="modal-title">{{ item.saleAmount }} - View</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Sale Amount</label>
                            <div class="col-lg-9">
                                <span v-text="item.saleAmount" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Description</label>
                            <div class="col-lg-9">
                                <span v-text="item.description" class="form-control"></span>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Sale Date</label>
                            <div class="col-lg-9">
                                <span v-text="item.saleDate" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn me-auto" data-bs-dismiss="modal">Close</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
<script setup>
    const props = defineProps(['item'])
</script>
<template>
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" id="viewModal">
        <div class="modal-dialog">
            <div class="modal-content" v-if="item">
                <form @submit.prevent="true">
                    <div class="modal-header">
                        <h5 class="modal-title">{{ item.companyName }} - View</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Company Name</label>
                            <div class="col-lg-9">
                                <span v-text="item.companyName" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Birth Date</label>
                            <div class="col-lg-9">
                                <span v-text="item.birthDate" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Title</label>
                            <div class="col-lg-9">
                                <span v-text="item.titleName" class="form-control"></span>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Customer Type</label>
                            <div class="col-lg-9">
                                <span v-text="item.customerType == 1 ? 'Corporate' : 'Personel'" class="form-control"></span>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Status Type</label>
                            <div class="col-lg-9">
                                <span v-text="item.statusTypeName" class="form-control"></span>
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
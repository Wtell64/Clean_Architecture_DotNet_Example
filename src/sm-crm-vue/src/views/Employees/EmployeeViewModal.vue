<script setup>
const props = defineProps(['item'])
</script>
<template>
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" id="viewModal">
        <div class="modal-dialog">
            <div class="modal-content" v-if="item">
                <form @submit.prevent="true">
                    <div class="modal-header">
                        <h5 class="modal-title">{{ item.id + " - " + item.fullName }} - View</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <div class="modal-body">
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">User Id</label>
                            <div class="col-lg-9">
                                <span v-text="item.userId" class="form-control" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Identity Number</label>
                            <div class="col-lg-9">
                                <span v-text="item.identityNumber" class="form-control" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Start Date</label>
                            <div class="col-lg-9">
                                <span class="form-control">{{ new Date(item.startDate).toLocaleDateString() }}</span>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Department</label>
                            <div class="col-lg-9">
                                <span v-text="item.departmentName" class="form-control"></span>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Email</label>
                            <div class="col-lg-9">
                                <span v-text="item.email" class="form-control"></span>
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
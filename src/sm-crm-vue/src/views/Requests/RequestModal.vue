<script setup>
    import { ref, computed, onMounted } from 'vue'
    import '@/plugins/axios'
    import '@/plugins/notification'
    import { Clone } from '@/plugins/helper'

    const props = defineProps(['item'])
    const emit = defineEmits(['onSaved'])

    const allCustomers = ref()
    const allEmployee = ref()
    const allRequestStatutes = ref()

    onMounted(() => {
        axios.get('/Customers').then(result => allCustomers.value = result.data)
        axios.get('/Employees').then(result => allEmployee.value = result.data.data)
        axios.get('/RequestStatutes').then(result => allRequestStatutes.value = result.data.data)
    })

    const isValid = computed(() => {
        return props.item.companyName != null &&
            props.item.companyName != '' &&
            props.item.titleId > 0
    })

    function saveItem() {
        if (!isValid.value) {
            toastr.warning('Form data is not valid!')
            return;
        }

        const item = Clone(props.item);
        if (item.id > 0) {
            axios.put("/Customers/" + item.id, item).then(res => {
                toastr.success("Customer updated!", "Updated")
                emit("onSaved", item)
            })
        } else {
            axios.post("/Customers", item).then(res => {
                toastr.success("Customer created!", "Created")
                emit("onSaved", item)
            })
        }
    }
</script>
<template>
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" id="appModal">
        <div class="modal-dialog">
            <div class="modal-content" v-if="item">
                <form @submit.prevent="true">
                    <div class="modal-header">
                        <h5 class="modal-title" v-text="item.id > 0 ? 'Edit' : 'New'"></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <input type="hidden" v-model="item.id" />

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Customer Name</label>
                            <div class="col-lg-9">
                                <input v-model="item.customerFullName" class="form-control" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Employee Name</label>
                            <div class="col-lg-9">
                                <input v-model="item.employeeFullName" class="form-control" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Department</label>
                            <div class="col-lg-9">
                                <select class="form-select">
                                    <option selected>{{ item.departmentName }}</option>
                                    <option v-for="department in allDepartment" :value="department.id">{{ department.name }}</option>
                                </select>
                            </div>
                        </div>


                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Status Type Name</label>
                            <div class="col-lg-9">
                                <select class="form-select">
                                    <option selected>{{ item.statusTypeName }}</option>
                                    <option v-for="statusType in allStatusType" :value="statusType.id">{{ statusType.name }}</option>
                                </select>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Territory Name</label>
                            <div class="col-lg-9">
                                <select class="form-select">
                                    <option selected>{{ item.territoryName }}</option>
                                    <option v-for="territory in allTerritory" :value="territory.id">{{ territory.name }}</option>
                                </select>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Birth Date</label>
                            <div class="col-lg-9">
                                <Calendar v-model="item.birthDate" dateFormat="dd.mm.yy" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Reports UserId</label>
                            <div class="col-lg-9">
                                <input v-model="item.reportsToUserId" class="form-control" />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Photo Path</label>
                            <div class="col-lg-9">
                                <input v-model="item.photoPath" class="form-control" />
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <button class="btn btn-primary" v-text="item.id > 0 ? 'Update' : 'Save'" @click="saveItem" :disabled="!isValid"></button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
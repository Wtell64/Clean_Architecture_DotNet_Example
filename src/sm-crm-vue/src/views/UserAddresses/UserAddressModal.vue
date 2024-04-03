<script setup>
import { ref, computed, onMounted } from 'vue'
import '@/plugins/axios'
import '@/plugins/notification'
import { Clone } from '@/plugins/helper'

const props = defineProps(['item'])
const emit = defineEmits(['onSaved'])

const allUsers = ref()

onMounted(() => {
    axios.get('/UserAddreses/users').then(result => allUsers.value = result.data)
})

const isValid = computed(() => {
    return props.item.userId != '' &&
        props.item.address != '' &&
        props.item.country != '' &&
        props.item.city != '' &&
        props.item.addressType > 0
})

function saveItem() {
    if (!isValid.value) {
        toastr.warning('Form data is not valid!')
        return;
    }

    const item = Clone(props.item);
    if (item.id > 0) {
        axios.put("/UserAddreses/" + item.id, item).then(res => {
            toastr.success("UserAddreses updated!", "Updated")
            emit("onSaved", item)
        })
    } else {
        axios.post("/UserAddreses", item).then(res => {
            toastr.success("UserAddreses created!", "Created")
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
                            <label class="col-lg-3 col-form-label">Username</label>
                            <div class="col-lg-9">
                                <Dropdown v-model="item.userId" :options="allUsers" optionLabel="username" optionValue="id"
                                    placeholder="Please select" showClear />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Address</label>
                            <div class="col-lg-9">
                                <input v-model="item.address" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Country</label>
                            <div class="col-lg-9">
                                <input v-model="item.country" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">City</label>
                            <div class="col-lg-9">
                                <input v-model="item.city" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Address Type</label>
                            <div class="col-lg-9">
                                <div class="form-check form-check-inline">
                                    <input type="radio" class="form-check-input" id="businessRadio" :value="1"
                                        v-model="item.addressType">
                                    <label class="form-check-label" for="businessRadio">Business</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input type="radio" class="form-check-input" id="homeRadio" :value="2"
                                        v-model="item.addressType">
                                    <label class="form-check-label" for="homeRadio">Home</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input type="radio" class="form-check-input" id="otherRadio" :value="3"
                                        v-model="item.addressType">
                                    <label class="form-check-label" for="otherRadio">Other</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <button class="btn btn-primary" v-text="item.id > 0 ? 'Update' : 'Save'" @click="saveItem"
                            :disabled="!isValid"></button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
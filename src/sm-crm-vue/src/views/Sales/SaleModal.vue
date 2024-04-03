<script setup>
    import { ref, computed, onMounted } from 'vue'
    import '@/plugins/axios'
    import '@/plugins/notification'
    import { Clone } from '@/plugins/helper'

    const props = defineProps(['item'])
    const emit = defineEmits(['onSaved'])

    const isValid = computed(() => {
        return true;

        return props.item.description != null &&
            props.item.description != '' &&
            props.item.amount != ''
            //props.item.requestId > 0 &&
            //props.item.employeeUserId != ''
    })

    onMounted(() => {
        
    })

    function saveItem() {
        if (!isValid.value) {
            toastr.warning('Form data is not valid!')
            return;
        }

        const item = Clone(props.item);

        // TODO: Request de?eri adres sat?r?ndan gelecek
        // TODO: UserId de?eri de Token içerisinden al?nacak.
        item.requestId = 1;
        item.employeeUserId = '0017ab89-56ea-4e1b-a616-7622c8a9d698';

        if (item.id > 0) {
            axios.put("/Sales/" + item.id, item).then(res => {
                toastr.success("Sale updated!", "Updated")
                emit("onSaved", item)
            })
        } else {
            axios.post("/Sales", item).then(res => {
                toastr.success("Sale created!", "Created")
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
                            <label class="col-lg-3 col-form-label">Sale Amount</label>
                            <div class="col-lg-9">
                                <input v-model="item.saleAmount" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Description</label>
                            <div class="col-lg-9">
                                <input v-model="item.description" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Sale Date</label>
                            <div class="col-lg-9">
                                <!--<Calendar v-model="item.saleDate" dateFormat="dd.mm.yy" showTime="false" />-->
                                <input type="date" v-model="item.saleDate" class="form-control" />
                            </div>
                        </div>

                        <!--<div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Employee</label>
                            <div class="col-lg-9">
                                <Dropdown v-model="item.EmployeeUserId" :options="allTitles" optionLabel="name" optionValue="id" placeholder="Please select" showClear />
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Sale Type</label>
                            <div class="col-lg-9">
                                <select v-model="item.saleType" class="form-select">
                                    <option value="">Please select</option>
                                    <option v-for="saleType in allSaleTypes" :value="saleType.id"> </option>
                                </select>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Status Type</label>
                            <div class="col-lg-9">
                                <select v-model="item.statusTypeId" class="form-select">
                                    <option value="">Please select</option>
                                    <option v-for="statusType in allStatusTypes" :value="statusType.id"> </option>
                                </select>
                            </div>
                        </div>-->
                    </div>
                    <div class="modal-footer">
                        <button class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <button class="btn btn-primary"
                                v-text="item.id > 0 ? 'Update' : 'Save'"
                                @click="saveItem"
                                :disabled="!isValid"></button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
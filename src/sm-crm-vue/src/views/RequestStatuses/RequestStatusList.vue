<script setup>
    import { onMounted, ref } from 'vue'
    import RequestStatusModal from '@/views/RequestStatuses/RequestStatusModal.vue'
    import RequestStatusViewModal from '@/views/RequestStatuses/RequestStatusViewModal.vue'
    import Pagination from '@/components/Pagination.vue'
    import '@/plugins/axios.js'
    import { swal_confirm } from '@/plugins/helper'

    let appModal
    let currentPage
    const tablePageSize = ref(10)
    const tableSearch = ref('')
    const dataItem = ref()
    const dataList = ref()

    onMounted(() => {
        fetchItems()
    })

    function fetchItems(page = 1) {
        currentPage = page;
        axios.get('/RequestStatuses', { params: {
            pageNumber: page,
            pageSize: tablePageSize.value,
            search: tableSearch.value
        } })
            .then(result => dataList.value = result.data)
    }

    function showModal() {
        appModal = new bootstrap.Modal('#appModal', {
            keyboard: false
        })
        appModal.show()
    }

    function createItem() {
        dataItem.value = {}
        showModal()
    }

    function editItem(id) {
        axios.get('/RequestStatuses/' + id).then(result => {
            dataItem.value = result.data;
            showModal();
        })
    }

    function viewItem(id) {{
        axios.get('/RequestStatuses/' + id).then(result => {
            dataItem.value = result.data;
            let viewModal = new bootstrap.Modal('#viewModal', {
                keyboard: false
            })
            viewModal.show()
        })
    }}

    function deleteItem(id) {
        swal_confirm().then(result => {
            if (result.value) {
                axios.delete("/RequestStatuses/" + id).then(res => {
                    fetchItems(currentPage || 1)
                })
            }
        })
    }

    function itemSaved() {
	    appModal.hide()
	    fetchItems(currentPage || 1)
    }
</script>
<template>
    <div class="page-body">
        <div class="container-xl">
            <div class="col-12">

                <DepartmentModal :item="dataItem" @onSaved="itemSaved" />
                <DepartmentViewModal :item="dataItem" />

                <div class="card">
                    <div class="card-header">
                        <h3 class="card-title">Request Status</h3>
                        <div class="ms-auto">
                            <button class="btn btn-sm btn-success" @click="fetchItems(currentPage)">
                                <i class="bi bi-arrow-clockwise"></i> Refresh
                            </button>
                            <button class="btn btn-sm btn-primary ms-1" @click="createItem">
                                <i class="bi bi-file-earmark"></i> Create
                            </button>
                        </div>
                    </div>
                    <div class="card-body border-bottom py-3">
                        <div class="d-flex">
                            <div class="text-muted">
                                Show
                                <div class="mx-2 d-inline-block">
                                    <input type="text" class="form-control form-control-sm" v-model="tablePageSize" size="3" @keyup.enter="fetchItems(1)">
                                </div>
                                entries
                            </div>
                            <div class="ms-auto text-muted">
                                Search:
                                <div class="ms-2 d-inline-block">
                                    <input type="text" class="form-control form-control-sm" aria-label="Search" v-model="tableSearch" @keyup.enter="fetchItems(1)">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table class="table card-table table-vcenter text-nowrap datatable">
                            <thead>
                                <tr>
                                    <th class="w-1"><input class="form-check-input m-0 align-middle" type="checkbox" aria-label="Select all invoices"></th>
                                    <th class="w-1">
                                        No.
                                        <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-sm icon-thick" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                            <path d="M6 15l6 -6l6 6" />
                                        </svg>
                                    </th>
                                    <th>Request Status Name</th>

                                    <th class="w-1"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="dataList == null || (dataList.items != null && dataList.items.length == 0)">
                                    <td colspan="8" class="text-center">No items!</td>
                                </tr>
                                <tr v-for="item in dataList.items" v-if="dataList">
                                    <td><input class="form-check-input m-0 align-middle" type="checkbox"></td>
                                    <td><span class="text-muted">{{ item.id }}</span></td>
                                    <td>{{ item.name }}</td>

                                    <td class="text-end">
                                        <button class="btn btn-sm btn-success" @click="viewItem(item.id)">
                                            <i class="bi bi-search"></i> View
                                        </button>
                                        <button class="btn btn-sm btn-info mx-1" @click="editItem(item.id)">
                                            <i class="bi bi-pencil"></i> Edit
                                        </button>
                                        <button class="btn btn-sm btn-danger" @click="deleteItem(item.id)">
                                            <i class="bi bi-trash"></i> Delete
                                        </button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <Pagination :data="dataList" @onPageChanged="fetchItems" />
                </div>
            </div>
        </div>
    </div>
</template>
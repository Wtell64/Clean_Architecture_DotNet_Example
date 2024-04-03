<script setup>
import { ref, computed, onMounted } from 'vue'
import '@/plugins/axios'
import '@/plugins/notification'
import { Clone } from '@/plugins/helper'

const props = defineProps(['item'])
const emit = defineEmits(['onSaved'])

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
    axios.put("/Tasks/" + item.id, item).then(res => {
      toastr.success("Task updated!", "Updated")
      emit("onSaved", item)
    })
  } else {
    axios.post("/Tasks", item).then(res => {
      toastr.success("Task created!", "Created")
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
              <label class="col-lg-3 col-form-label">Company Name</label>
              <div class="col-lg-9">
                <input v-model="item.description" class="form-control" />
              </div>
            </div>
            <div class="row mb-3">
              <label class="col-lg-3 col-form-label">Birth Date</label>
              <div class="col-lg-9">
                <Calendar v-model="item.startDate" dateFormat="dd.mm.yy" />
              </div>
            </div>
            <div class="row mb-3">
              <label class="col-lg-3 col-form-label">Birth Date</label>
              <div class="col-lg-9">
                <Calendar v-model="item.endDate" dateFormat="dd.mm.yy" />
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
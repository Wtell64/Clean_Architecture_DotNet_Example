import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useLayoutStore = defineStore('layout', () => {
    // state
    const isPageLoading = ref(false)
	const isTableLoading = ref(false)

	// actions
	function showPageLoading() {
		isPageLoading.value = true
	}

	function hidePageLoading() {
		setTimeout(() => {
			isPageLoading.value = false
		}, 500)
	}

	function showTableLoading() {
		isTableLoading.value = true
	}

	function hideTableLoading() {
		setTimeout(() => {
			isTableLoading.value = false
		}, 1000)
	}

	return {
		isPageLoading, showPageLoading, hidePageLoading,
		isTableLoading, showTableLoading, hideTableLoading
	}
})
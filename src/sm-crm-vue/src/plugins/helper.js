export function Clone(obj) {
    return JSON.parse(JSON.stringify(obj))
}

export function swal_confirm() {
    return swal.fire({
        icon: "question",
        title: "Are you sure?",
        text: "Are you sure you want to delete this item?",
        showCancelButton: true,
        confirmButtonText: "Delete",
        confirmButtonColor: '#ff0000'
    })
}
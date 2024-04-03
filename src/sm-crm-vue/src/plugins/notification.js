import swal from 'sweetalert2'
import toastr from 'toastr'
import 'sweetalert2/dist/sweetalert2.min.css'
import 'toastr/build/toastr.min.css'

window.swal = swal
window.toastr = toastr

toastr.options = {
	preventDuplicates: true,
	showDuration: 500,
	positionClass: 'toast-bottom-right'
}
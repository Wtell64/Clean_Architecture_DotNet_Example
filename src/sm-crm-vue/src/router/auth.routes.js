import Login from '@/views/Auth/Login.vue'
import Register from '@/views/Auth/Register.vue'
import BasicLayout from '@/views/_Layouts/BasicLayout.vue'

const authLayoutPages = {
    path: '/auth', 
    component: BasicLayout, 
    name: 'auth',
    redirect: '/auth/login',
    children: [
        { path: 'login', component: Login, name: 'login' },
        { path: 'register', component: Register, name: 'register' },
    ]
}

export default authLayoutPages
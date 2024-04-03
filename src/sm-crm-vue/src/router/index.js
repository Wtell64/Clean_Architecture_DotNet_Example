import { createRouter, createWebHistory } from 'vue-router'
import NotFound from '@/views/NotFound.vue'
import StateExample from '@/views/StateExample.vue'
import adminPages from './admin.routes.js'
import authPages from './auth.routes.js'
import session from '@/plugins/session'
import { useLayoutStore } from '@/stores'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        { ...adminPages },
        { ...authPages },
        { path: '/state', component: StateExample, name: 'state' },
        { path: '/:pathMatch(.*)*', component: NotFound, name: 'not-found' }
    ]
})

router.beforeEach((to, from) => {
    const { showPageLoading } = useLayoutStore()
    showPageLoading()

    if (
        to.name !== 'login' &&
        !session.isLoggedIn() &&
        to.matched.some((record) => record.meta.authorize)
    ) {
        return {
            name: 'login',
            // save the location we were at to come back later
            query: { redirect: to.fullPath }
        }
    }
})

router.afterEach((to, from) => {
    const { hidePageLoading } = useLayoutStore()
    hidePageLoading()
})

export default router

import axios from 'axios'
import session from '@/plugins/session'
import '@/plugins/notification'

window.axios = axios

axios.defaults.baseURL = import.meta.env.VITE_API_URL || ''

// Request Interceptor
axios.interceptors.request.use((config) => {
    if (!import.meta.env.PROD)
        console.log('[Request]', config.baseURL + config.url)

    const token = session.getSession()
    if (token) {
        config.headers["Authorization"] = `Bearer ${token.accessToken}`
    }

    return config
})

// Response Interceptor
axios.interceptors.response.use((response) => {
    if (!import.meta.env.PROD)
        console.log('[Response]', response)
    return response
}, (error) => {
    const { config, response } = error
    const originalRequest = config
    const { status, statusText, data } = response || {}

    if (error.code == "ERR_NETWORK" || status >= 500) {
        const errorMessage = (data && data.message) || error.message || error.response.data.message
        toastr.error(errorMessage, 'Error')
        return Promise.reject(error)
    }

    if (status === 401) {
        if (!originalRequest._retry) {
            originalRequest._retry = true;
            const token = session.getSession()
            const refreshToken = token.refreshToken;
            if (!import.meta.env.PROD)
                console.log('[Refresh Token]', refreshToken)

            return axios.post("auth/refresh-token", refreshToken, {
                headers: { "Content-Type": "application/json" }
            }).then((res) => {
                debugger;
                if (res && res.data && res.data.accessToken) {
                    if (!import.meta.env.PROD)
                        console.log('[New Token]', res.data)

                    session.setSession(res.data)
                    axios.defaults.headers.common["Authorization"] = "Bearer " + res.data.accessToken;
                    originalRequest.headers["Authorization"] = "Bearer " + res.data.accessToken;
                    return axios(originalRequest);
                }
            });
        }
        else {
            toastr.warn('Please login again!', 'Auth')
            router.push({ name: 'login' })
        }
    }
})
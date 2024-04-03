import { createPinia } from 'pinia'
import { createApp } from 'vue'
import App from './App.vue'
import './assets/main.css'
import router from './router';
import PrimeVue from 'primevue/config';
import Dropdown from 'primevue/dropdown'
import Calendar from 'primevue/calendar'
import 'primevue/resources/themes/aura-light-blue/theme.css'

const app = createApp(App)

app.use(PrimeVue, {
    zIndex: {
        overlay: 10000
    }
});
app.component('Dropdown', Dropdown);
app.component('Calendar', Calendar);

app.use(createPinia())
app.use(router)

app.mount('#app')
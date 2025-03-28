import { createApp } from 'vue'
import './style.css'
import App from './App.vue'

import 'vuetify/styles/main.css';
import { createVuetify } from 'vuetify';
const vuetify = createVuetify();

createApp(App).use(vuetify).mount('#app')

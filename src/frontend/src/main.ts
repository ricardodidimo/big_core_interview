import { createApp } from "vue";
import "./style.css";
import './assets/styles/main.scss';
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import "vuetify/styles/main.css";
import "vuetify/dist/vuetify.css";

import i18n from "./i18n";
createApp(App).use(i18n).use(vuetify).mount("#app");

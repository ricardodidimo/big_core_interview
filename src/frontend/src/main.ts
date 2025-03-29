import "@fortawesome/fontawesome-free/css/all.css";
import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";

import { createVuetify } from "vuetify";
import "vuetify/styles/main.css";
import "vuetify/dist/vuetify.css";
import { fa } from "vuetify/iconsets/fa";
import i18n from "./i18n";

const vuetify = createVuetify({
  icons: {
    defaultSet: "fa",
    sets: { fa },
  },
});

createApp(App).use(i18n).use(vuetify).mount("#app");

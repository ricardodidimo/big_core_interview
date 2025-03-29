import { createI18n } from 'vue-i18n';

import en from './locales/en.json';
import pt from './locales/pt.json';

function getCurrentPreferredLocale(): string {
  return localStorage.getItem("user-locale") || "pt"; 
}

const i18n = createI18n({
  legacy: false,
  locale: getCurrentPreferredLocale(),
  fallbackLocale: 'en',
  messages: { en, pt }
});

export default i18n;

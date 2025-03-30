import { createI18n } from 'vue-i18n';

import en from './locales/en.json';
import pt from './locales/pt.json';
import { getLanguage } from './helpers/localStoragePersistence';

function getCurrentPreferredLocale(): string {
  return getLanguage(); 
}

const i18n = createI18n({
  legacy: false,
  locale: getCurrentPreferredLocale(),
  fallbackLocale: 'en',
  messages: { en, pt }
});

export default i18n;

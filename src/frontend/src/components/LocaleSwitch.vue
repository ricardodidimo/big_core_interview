<script setup lang="ts">
import { useI18n } from 'vue-i18n';
import { setLanguage } from '../helpers/localStoragePersistence';

const { locale, t } = useI18n();

const changeLanguage = (lang: string) => {
    locale.value = lang;
    setLanguage(locale.value);
};

const availableLangs: {locale: string, I18Key: string}[] = [
    {
        locale: "en",
        I18Key: "english"
    },
    {
        locale: "pt",
        I18Key: "portuguese"
    }
]
</script>

<template>
    <div>
        <small> {{ t('languages.choose_lang')}}</small>
            <v-chip v-for="lang in availableLangs" class="mx-1" :rounded="false" @click="changeLanguage(lang.locale)">
                <v-icon v-if="locale === lang.locale" class="mr-3 text" icon="fas fa-flag" size="15" />
                <p>{{ t(`languages.${lang.I18Key}`) }}</p>
            </v-chip>
     
    </div>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useI18n } from 'vue-i18n';
import { useTheme } from 'vuetify';
import { getTheme, setTheme } from '../helpers/localStoragePersistence';


const { t } = useI18n();
const theme = useTheme();
const isDark = ref(getTheme() === 'dark');

const toggleTheme = () => {
    isDark.value = !isDark.value;
    document.body.classList.toggle('dark-theme', isDark.value);
    theme.global.name.value = isDark.value ? 'dark' : 'light';
    setTheme(isDark.value ? 'dark' : 'light')
};

onMounted(() => {
    if (isDark.value) {
        document.body.classList.add('dark-theme');
        theme.global.name.value = 'dark';
    }
});
</script>

<template>
    <div>
        <v-icon :icon="isDark ? 'fa fa-moon' : 'fa fa-sun'" class="mr-2 mt-1"/>
        <v-btn @click="toggleTheme" density="default">
            <p>
                {{ isDark ? t('theme_switch.call_to_action_dark') : t('theme_switch.call_to_action_light') }}
            </p>
        </v-btn>
    </div>
</template>
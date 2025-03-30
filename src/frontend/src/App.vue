<script setup lang="ts">
import Dashboard from './views/Dashboard.vue';
import LocaleSwitch from './components/LocaleSwitch.vue';
import ThemeSwitch from './components/ThemeSwitch.vue';
</script>

<template>
  <div class="d-flex align-center justify-center">
    <a href="https://www.linkedin.com/in/ricardo-didimo-558630209/" target="_blank">
      <v-icon icon="fa-brands fa-linkedin" color="primary" size="50" class="logo"></v-icon>
    </a>

    <a href="https://github.com/ricardodidimo/big_core_interview" target="_blank">
      <v-icon icon="fa-brands fa-vuejs" color="secondary" size="50" class="logo"></v-icon>
    </a>

  </div>
  <small>Aplicativo demonstrativo para entrevista técnica Big Core @ 2025</small>

  <v-container class="d-flex align-center justify-space-between">
    <ThemeSwitch />
    <LocaleSwitch />
  </v-container>
  <Dashboard />
</template>

<style scoped>
.logo {
  padding: 1em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>

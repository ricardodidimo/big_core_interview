<script setup lang="ts">
import LocaleSwitch from '../components/LocaleSwitch.vue';
import OdometerTrackerDataTable from '../components/OdometerTrackerDataTable.vue';
import ThemeSwitch from '../components/ThemeSwitch.vue';
</script>

<template>
  <v-container>
    <ThemeSwitch />
    <LocaleSwitch />
    <OdometerTrackerDataTable />
  </v-container>
</template>

<style scoped>
</style>
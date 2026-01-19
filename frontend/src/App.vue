<script setup lang="ts">
import ContactList from './components/ContactList.vue';
</script>

<template>
  <v-app>
    <v-app-bar color="primary" elevation="4">
      <v-app-bar-title class="text-h5 font-weight-bold">
        Управление Контактами
      </v-app-bar-title>
    </v-app-bar>

    <v-main>
      <ContactList />
    </v-main>
  </v-app>
</template>

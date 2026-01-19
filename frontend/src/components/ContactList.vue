<script setup lang="ts">
import { ref, onMounted } from 'vue';
import type { Contact, CreateContactRequest, UpdateContactRequest } from '../types/contact';
import { contactService } from '../services/contactService';
import ContactDialog from './ContactDialog.vue';

const contacts = ref<Contact[]>([]);
const loading = ref(false);
const showDialog = ref(false);
const selectedContact = ref<Contact | undefined>(undefined);
const showDeleteDialog = ref(false);
const contactToDelete = ref<Contact | undefined>(undefined);
const snackbar = ref(false);
const snackbarText = ref('');
const snackbarColor = ref('success');

const loadContacts = async () => {
  loading.value = true;
  try {
    contacts.value = await contactService.getAll();
  } catch (error) {
    showSnackbar('Ошибка загрузки контактов', 'error');
  } finally {
    loading.value = false;
  }
};

const openCreateDialog = () => {
  selectedContact.value = undefined;
  showDialog.value = true;
};

const openEditDialog = (contact: Contact) => {
  selectedContact.value = contact;
  showDialog.value = true;
};

const openDeleteDialog = (contact: Contact) => {
  contactToDelete.value = contact;
  showDeleteDialog.value = true;
};

const saveContact = async (contactData: CreateContactRequest | UpdateContactRequest) => {
  loading.value = true;
  try {
    if ('id' in contactData) {
      await contactService.update(contactData);
      showSnackbar('Контакт успешно обновлен');
    } else {
      await contactService.create(contactData);
      showSnackbar('Контакт успешно создан');
    }
    await loadContacts();
  } catch (error) {
    showSnackbar('Ошибка сохранения контакта', 'error');
  } finally {
    loading.value = false;
  }
};

const deleteContact = async () => {
  if (!contactToDelete.value) return;

  loading.value = true;
  try {
    await contactService.delete(contactToDelete.value.id);
    showSnackbar('Контакт успешно удален');
    await loadContacts();
  } catch (error) {
    showSnackbar('Ошибка удаления контакта', 'error');
  } finally {
    loading.value = false;
    showDeleteDialog.value = false;
    contactToDelete.value = undefined;
  }
};

const formatDate = (dateString: string) => {
  const date = new Date(dateString);
  return date.toLocaleDateString('ru-RU');
};

const showSnackbar = (text: string, color: string = 'success') => {
  snackbarText.value = text;
  snackbarColor.value = color;
  snackbar.value = true;
};

onMounted(() => {
  loadContacts();
});
</script>

<template>
  <v-container class="pa-6">
    <v-row class="mb-6">
      <v-col>
        <div class="d-flex justify-space-between align-center">
          <h1 class="text-h3 font-weight-bold">Контакты</h1>
          <v-btn
            color="primary"
            prepend-icon="mdi-plus"
            size="large"
            @click="openCreateDialog"
          >
            Добавить контакт
          </v-btn>
        </div>
      </v-col>
    </v-row>

    <v-progress-linear
      v-if="loading"
      indeterminate
      color="primary"
      class="mb-4"
    />

    <v-card v-if="!loading">
      <v-card-text>
        <v-table v-if="contacts.length > 0" density="comfortable">
          <thead>
            <tr>
              <th class="text-left font-weight-bold">Имя</th>
              <th class="text-left font-weight-bold">Телефон</th>
              <th class="text-left font-weight-bold">Должность</th>
              <th class="text-left font-weight-bold">Дата рождения</th>
              <th class="text-left font-weight-bold">Действия</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="contact in contacts"
              :key="contact.id"
              class="contact-row"
            >
              <td>
                <div class="d-flex align-center">
                  <span class="font-weight-medium">{{ contact.name }}</span>
                </div>
              </td>
              <td>
                <div class="d-flex align-center">
                  {{ contact.mobilePhone }}
                </div>
              </td>
              <td>
                <div class="d-flex align-center">
                  {{ contact.jobTitle }}
                </div>
              </td>
              <td>
                <div class="d-flex align-center">
                  {{ formatDate(contact.birthDate) }}
                </div>
              </td>
              <td>
                <div class="d-flex">
                  <v-btn
                    icon="mdi-pencil"
                    size="small"
                    color="primary"
                    variant="text"
                    @click="openEditDialog(contact)"
                  />
                  <v-btn
                    icon="mdi-delete"
                    size="small"
                    color="error"
                    variant="text"
                    @click="openDeleteDialog(contact)"
                  />
                </div>
              </td>
            </tr>
          </tbody>
        </v-table>

        <div v-else class="text-center pa-8">
          <p class="text-h6 text-grey">Контакты отсутствуют</p>
        </div>
      </v-card-text>
    </v-card>

    <ContactDialog
      v-model="showDialog"
      :contact="selectedContact"
      @save="saveContact"
    />

    <v-dialog
      v-model="showDeleteDialog"
      max-width="400"
    >
      <v-card>
        <v-card-title class="text-h5 bg-error text-white">
          Удалить контакт
        </v-card-title>
        <v-card-text class="pt-6">
          Вы уверены, что хотите удалить контакт <strong>{{ contactToDelete?.name }}</strong>?
        </v-card-text>
        <v-card-actions class="px-6 pb-6">
          <v-spacer />
          <v-btn
            color="grey-darken-1"
            variant="text"
            @click="showDeleteDialog = false"
          >
            Отмена
          </v-btn>
          <v-btn
            color="error"
            variant="elevated"
            @click="deleteContact"
          >
            Удалить
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-snackbar
      v-model="snackbar"
      :color="snackbarColor"
      :timeout="3000"
      location="top"
    >
      {{ snackbarText }}
    </v-snackbar>
  </v-container>
</template>

<style scoped>
.contact-row:hover {
  background-color: rgba(0, 0, 0, 0.02);
}
</style>
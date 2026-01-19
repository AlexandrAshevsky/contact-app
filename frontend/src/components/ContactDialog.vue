<script setup lang="ts">
import { ref, watch, computed } from 'vue';
import type { Contact, CreateContactRequest, UpdateContactRequest } from '../types/contact';
import { validationRules } from '../utils/validation';

interface Props {
  modelValue: boolean;
  contact?: Contact;
}

interface Emits {
  (e: 'update:modelValue', value: boolean): void;
  (e: 'save', contact: CreateContactRequest | UpdateContactRequest): void;
}

const props = defineProps<Props>();
const emit = defineEmits<Emits>();

const form = ref();
const formData = ref({
  name: '',
  mobilePhone: '',
  jobTitle: '',
  birthDate: '',
});

const isEditMode = computed(() => !!props.contact);
const dialogTitle = computed(() => isEditMode.value ? 'Редактировать контакт' : 'Создать контакт');

watch(() => props.modelValue, (newValue) => {
  if (newValue) {
    if (props.contact) {
      formData.value = {
        name: props.contact.name,
        mobilePhone: props.contact.mobilePhone,
        jobTitle: props.contact.jobTitle,
        birthDate: props.contact.birthDate,
      };
    } else {
      resetForm();
    }
  }
});

const resetForm = () => {
  formData.value = {
    name: '',
    mobilePhone: '',
    jobTitle: '',
    birthDate: '',
  };
  form.value?.resetValidation();
};

const closeDialog = () => {
  emit('update:modelValue', false);
  resetForm();
};

const saveContact = async () => {
  const { valid } = await form.value.validate();

  if (valid) {
    if (isEditMode.value && props.contact) {
      const updateData: UpdateContactRequest = {
        ...formData.value,
        id: props.contact.id,
      };
      emit('save', updateData);
    } else {
      emit('save', formData.value);
    }
    closeDialog();
  }
};
</script>

<template>
  <v-dialog
    :model-value="modelValue"
    @update:model-value="emit('update:modelValue', $event)"
    max-width="600px"
    persistent
  >
    <v-card>
      <v-card-title class="text-h5 bg-primary">
        {{ dialogTitle }}
      </v-card-title>

      <v-card-text class="pt-6">
        <v-form ref="form">
          <v-text-field
            v-model="formData.name"
            label="Имя"
            :rules="validationRules.name"
            variant="outlined"
            prepend-inner-icon="mdi-account"
            required
            class="mb-2"
          />

          <v-text-field
            v-model="formData.mobilePhone"
            label="Мобильный телефон"
            :rules="validationRules.mobilePhone"
            variant="outlined"
            prepend-inner-icon="mdi-phone"
            required
            class="mb-2"
            placeholder="+375 12 345 67 89"
          />

          <v-text-field
            v-model="formData.jobTitle"
            label="Должность"
            :rules="validationRules.jobTitle"
            variant="outlined"
            prepend-inner-icon="mdi-briefcase"
            required
            class="mb-2"
          />

          <v-text-field
            v-model="formData.birthDate"
            label="Дата рождения"
            :rules="validationRules.birthDate"
            variant="outlined"
            type="date"
            prepend-inner-icon="mdi-calendar"
            required
          />
        </v-form>
      </v-card-text>

      <v-card-actions class="px-6 pb-6">
        <v-spacer />
        <v-btn
          color="grey-darken-1"
          variant="text"
          @click="closeDialog"
        >
          Отмена
        </v-btn>
        <v-btn
          color="primary"
          variant="elevated"
          @click="saveContact"
        >
          Сохранить
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

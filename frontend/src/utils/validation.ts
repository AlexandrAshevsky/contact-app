export const validationRules = {
  name: [
    (v: string) => !!v || 'Имя обязательно',
    (v: string) => (v && v.length >= 2) || 'Имя должно содержать минимум 2 символа',
    (v: string) => (v && v.length <= 100) || 'Имя должно содержать максимум 100 символов',
  ],

  mobilePhone: [
    (v: string) => !!v || 'Номер телефона обязателен',
    (v: string) => {
      const cleanedPhone = v.replace(/[\s-]/g, '');

      const phoneRegex = /^\+375\d{9}$/;
      
      return phoneRegex.test(cleanedPhone) || 'Введите корректный белорусский номер телефона (например +375 29 123 45 67)';
    },
  ],

  jobTitle: [
    (v: string) => !!v || 'Должность обязательна',
    (v: string) => (v && v.length >= 2) || 'Должность должна содержать минимум 2 символа',
    (v: string) => (v && v.length <= 100) || 'Должность должна содержать максимум 100 символов',
  ],

  birthDate: [
    (v: string) => !!v || 'Дата рождения обязательна',
    (v: string) => {
      if (!v) return true;
      const date = new Date(v);
      const now = new Date();
      const minDate = new Date(1900, 0, 1);
      return (date >= minDate && date <= now) || 'Введите корректную дату рождения';
    },
    (v: string) => {
      if (!v) return true;
      const date = new Date(v);
      const now = new Date();
      const age = now.getFullYear() - date.getFullYear();
      return (age >= 18 && age <= 100) || 'Возраст должен быть от 18 до 100 лет';
    },
  ],
};

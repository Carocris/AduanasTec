<template>
  <form @submit.prevent="onSubmit">
    <div>
      <label>Nombre:</label>
      <input v-model="form.nombre" required />
    </div>
    <div>
      <label>Email:</label>
      <input v-model="form.email" type="email" required />
    </div>
    <div>
      <label>Teléfono:</label>
      <input v-model="form.telefono" required />
    </div>
    <button type="submit">{{ form.id ? 'Actualizar' : 'Crear' }}</button>
    <button v-if="form.id" type="button" @click="$emit('cancel')">Cancelar</button>
  </form>
</template>

<script setup>
import { reactive, watch } from 'vue';
const props = defineProps({
  cliente: {
    type: Object,
    default: () => ({ nombre: '', email: '', telefono: '' })
  }
});
const emit = defineEmits(['submit', 'cancel']);
const form = reactive({ ...props.cliente });
watch(() => props.cliente, (val) => {
  Object.assign(form, val || { nombre: '', email: '', telefono: '' });
});
function onSubmit() {
  emit('submit', { ...form });
}
</script>

<style scoped>
form {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  max-width: 350px;
  background: #fafafa;
  padding: 1rem;
  border-radius: 8px;
  box-shadow: 0 2px 8px #0001;
}
label {
  font-weight: bold;
}
input {
  padding: 0.3rem;
  border: 1px solid #ccc;
  border-radius: 4px;
}
button {
  margin-top: 0.5rem;
}
@media (max-width: 600px) {
  form {
    max-width: 100%;
    padding: 0.5rem;
  }
}
</style> 
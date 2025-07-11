<template>
  <form @submit.prevent="onSubmit">
    <div>
      <label>Nombre:</label>
      <input v-model="form.nombre" required />
    </div>
    <div>
      <label>Descripción:</label>
      <input v-model="form.descripcion" required />
    </div>
    <div>
      <label>Precio:</label>
      <input type="number" v-model.number="form.precio" min="0" required />
    </div>
    <div>
      <label>Stock:</label>
      <input type="number" v-model.number="form.stock" min="0" required />
    </div>
    <button type="submit">{{ form.id ? 'Actualizar' : 'Crear' }}</button>
    <button v-if="form.id" type="button" @click="$emit('cancel')">Cancelar</button>
  </form>
</template>

<script setup>
import { reactive, watch, toRefs } from 'vue';
const props = defineProps({
  producto: {
    type: Object,
    default: () => ({ nombre: '', descripcion: '', precio: 0, stock: 0 })
  }
});
const emit = defineEmits(['submit', 'cancel']);
const form = reactive({ ...props.producto });
watch(() => props.producto, (val) => {
  Object.assign(form, val || { nombre: '', descripcion: '', precio: 0, stock: 0 });
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
<template>
  <form @submit.prevent="onSubmit">
    <div>
      <label>Cliente:</label>
      <select v-model="form.clienteId" required>
        <option value="" disabled>Seleccione un cliente</option>
        <option v-for="cliente in clientes" :key="cliente.id" :value="cliente.id">
          {{ cliente.nombre }}
        </option>
      </select>
    </div>
    <div>
      <label>Productos:</label>
      <div v-for="producto in productos" :key="producto.id" style="margin-bottom: 0.5rem;">
        <input type="checkbox" :value="producto.id" v-model="selectedProductos" />
        {{ producto.nombre }} (Stock: {{ producto.stock }})
        <input v-if="selectedProductos.includes(producto.id)" type="number" min="1" :max="producto.stock" v-model.number="cantidades[producto.id]" placeholder="Cantidad" style="width: 60px; margin-left: 8px;" />
      </div>
    </div>
    <button type="submit">Registrar Venta</button>
    <button type="button" @click="$emit('cancel')">Cancelar</button>
  </form>
</template>

<script setup>
import { reactive, ref, watch } from 'vue';
const props = defineProps({
  clientes: Array,
  productos: Array
});
const emit = defineEmits(['submit', 'cancel']);
const form = reactive({ clienteId: '', productos: [] });
const selectedProductos = ref([]);
const cantidades = reactive({});

watch(selectedProductos, (ids) => {
  form.productos = ids.map(id => ({ productoId: id, cantidad: cantidades[id] || 1 }));
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
  max-width: 400px;
  background: #fafafa;
  padding: 1rem;
  border-radius: 8px;
  box-shadow: 0 2px 8px #0001;
}
label {
  font-weight: bold;
}
select, input[type="number"] {
  margin-left: 8px;
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
<template>
  <div>
    <h2>Ventas</h2>
    <button @click="showForm = true">Registrar Venta</button>
    <VentaForm v-if="showForm" :clientes="clientes" :productos="productos" @submit="handleSubmit" @cancel="handleCancel" />
    <VentasTable :ventas="ventas" />
    <div v-if="success" class="success">{{ success }}</div>
    <div v-if="error" class="error">{{ error }}</div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import VentasTable from '../components/VentasTable.vue';
import VentaForm from '../components/VentaForm.vue';

const ventas = ref([]);
const clientes = ref([]);
const productos = ref([]);
const showForm = ref(false);
const success = ref('');
const error = ref('');

const fetchVentas = async () => {
  try {
    const token = localStorage.getItem('token');
    const response = await axios.get('http://localhost:5000/api/venta', {
      headers: { Authorization: `Bearer ${token}` }
    });
    ventas.value = response.data;
  } catch (e) {
    error.value = 'Error al cargar ventas';
  }
};

const fetchClientes = async () => {
  const token = localStorage.getItem('token');
  const response = await axios.get('http://localhost:5000/api/cliente', {
    headers: { Authorization: `Bearer ${token}` }
  });
  clientes.value = response.data;
};

const fetchProductos = async () => {
  const token = localStorage.getItem('token');
  const response = await axios.get('http://localhost:5000/api/producto', {
    headers: { Authorization: `Bearer ${token}` }
  });
  productos.value = response.data;
};

onMounted(async () => {
  await fetchVentas();
  await fetchClientes();
  await fetchProductos();
});

function handleCancel() {
  showForm.value = false;
}

async function handleSubmit(venta) {
  error.value = '';
  success.value = '';
  const token = localStorage.getItem('token');
  try {
    if (!venta.clienteId || !venta.productos || venta.productos.length === 0) {
      error.value = 'Debe seleccionar un cliente y al menos un producto';
      return;
    }
    for (const p of venta.productos) {
      if (!p.cantidad || p.cantidad < 1) {
        error.value = 'La cantidad de cada producto debe ser mayor a 0';
        return;
      }
    }
    await axios.post('http://localhost:5000/api/venta', venta, {
      headers: { Authorization: `Bearer ${token}` }
    });
    showForm.value = false;
    success.value = 'Venta registrada correctamente';
    await fetchVentas();
  } catch (e) {
    error.value = 'Error al registrar la venta';
  }
}
</script>

<style scoped>
.success { color: green; margin-top: 1rem; }
.error { color: red; margin-top: 1rem; }
</style> 
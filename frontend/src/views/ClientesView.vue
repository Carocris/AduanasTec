<template>
  <div>
    <h2>Clientes</h2>
    <button @click="showForm = true; selected = null">Agregar Cliente</button>
    <ClienteForm v-if="showForm" :cliente="selected" @submit="handleSubmit" @cancel="handleCancel" />
    <ClientesTable :clientes="clientes" />
    <div v-if="!showForm">
      <button v-for="cliente in clientes" :key="cliente.id + '-edit'" @click="editCliente(cliente)">Editar</button>
      <button v-for="cliente in clientes" :key="cliente.id + '-del'" @click="deleteCliente(cliente.id)">Eliminar</button>
    </div>
    <div v-if="success" class="success">{{ success }}</div>
    <div v-if="error" class="error">{{ error }}</div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import ClientesTable from '../components/ClientesTable.vue';
import ClienteForm from '../components/ClienteForm.vue';

const clientes = ref([]);
const showForm = ref(false);
const selected = ref(null);
const success = ref('');
const error = ref('');

const fetchClientes = async () => {
  try {
    const token = localStorage.getItem('token');
    const response = await axios.get('http://localhost:5000/api/cliente', {
      headers: { Authorization: `Bearer ${token}` }
    });
    clientes.value = response.data;
  } catch (e) {
    error.value = 'Error al cargar clientes';
  }
};

onMounted(fetchClientes);

function editCliente(cliente) {
  selected.value = { ...cliente };
  showForm.value = true;
}

function handleCancel() {
  showForm.value = false;
  selected.value = null;
}

async function handleSubmit(cliente) {
  error.value = '';
  success.value = '';
  const token = localStorage.getItem('token');
  try {
    if (!cliente.nombre || !cliente.email || !cliente.telefono) {
      error.value = 'Todos los campos son obligatorios';
      return;
    }
    if (!/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(cliente.email)) {
      error.value = 'El email no es válido';
      return;
    }
    if (cliente.id) {
      await axios.put(`http://localhost:5000/api/cliente/${cliente.id}`, cliente, {
        headers: { Authorization: `Bearer ${token}` }
      });
      success.value = 'Cliente actualizado correctamente';
    } else {
      await axios.post('http://localhost:5000/api/cliente', cliente, {
        headers: { Authorization: `Bearer ${token}` }
      });
      success.value = 'Cliente creado correctamente';
    }
    showForm.value = false;
    selected.value = null;
    await fetchClientes();
  } catch (e) {
    error.value = 'Error al guardar el cliente';
  }
}

async function deleteCliente(id) {
  error.value = '';
  success.value = '';
  const token = localStorage.getItem('token');
  try {
    await axios.delete(`http://localhost:5000/api/cliente/${id}`, {
      headers: { Authorization: `Bearer ${token}` }
    });
    success.value = 'Cliente eliminado correctamente';
    await fetchClientes();
  } catch (e) {
    error.value = 'Error al eliminar el cliente';
  }
}
</script>

<style scoped>
.success { color: green; margin-top: 1rem; }
.error { color: red; margin-top: 1rem; }
</style> 
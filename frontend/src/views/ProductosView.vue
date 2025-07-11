<template>
  <div>
    <h2>Productos</h2>
    <button @click="showForm = true; selected = null">Agregar Producto</button>
    <ProductoForm v-if="showForm" :producto="selected" @submit="handleSubmit" @cancel="handleCancel" />
    <ProductosTable :productos="productos" />
    <div v-if="!showForm">
      <button v-for="producto in productos" :key="producto.id + '-edit'" @click="editProducto(producto)">Editar</button>
      <button v-for="producto in productos" :key="producto.id + '-del'" @click="deleteProducto(producto.id)">Eliminar</button>
    </div>
    <div v-if="success" class="success">{{ success }}</div>
    <div v-if="error" class="error">{{ error }}</div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import ProductosTable from '../components/ProductosTable.vue';
import ProductoForm from '../components/ProductoForm.vue';

const productos = ref([]);
const showForm = ref(false);
const selected = ref(null);
const success = ref('');
const error = ref('');

const fetchProductos = async () => {
  try {
    const token = localStorage.getItem('token');
    const response = await axios.get('http://localhost:5000/api/producto', {
      headers: { Authorization: `Bearer ${token}` }
    });
    productos.value = response.data;
  } catch (e) {
    error.value = 'Error al cargar productos';
  }
};

onMounted(fetchProductos);

function editProducto(producto) {
  selected.value = { ...producto };
  showForm.value = true;
}

function handleCancel() {
  showForm.value = false;
  selected.value = null;
}

async function handleSubmit(producto) {
  error.value = '';
  success.value = '';
  const token = localStorage.getItem('token');
  try {
    if (!producto.nombre || !producto.descripcion || producto.precio < 0 || producto.stock < 0) {
      error.value = 'Todos los campos son obligatorios y deben ser válidos';
      return;
    }
    if (producto.id) {
      await axios.put(`http://localhost:5000/api/producto/${producto.id}`, producto, {
        headers: { Authorization: `Bearer ${token}` }
      });
      success.value = 'Producto actualizado correctamente';
    } else {
      await axios.post('http://localhost:5000/api/producto', producto, {
        headers: { Authorization: `Bearer ${token}` }
      });
      success.value = 'Producto creado correctamente';
    }
    showForm.value = false;
    selected.value = null;
    await fetchProductos();
  } catch (e) {
    error.value = 'Error al guardar el producto';
  }
}

async function deleteProducto(id) {
  error.value = '';
  success.value = '';
  const token = localStorage.getItem('token');
  try {
    await axios.delete(`http://localhost:5000/api/producto/${id}`, {
      headers: { Authorization: `Bearer ${token}` }
    });
    success.value = 'Producto eliminado correctamente';
    await fetchProductos();
  } catch (e) {
    error.value = 'Error al eliminar el producto';
  }
}
</script>

<style scoped>
.success { color: green; margin-top: 1rem; }
.error { color: red; margin-top: 1rem; }
</style> 
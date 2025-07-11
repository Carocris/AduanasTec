<template>
  <div class="table-responsive">
    <h2>Lista de Ventas</h2>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Fecha</th>
          <th>Cliente</th>
          <th>Total</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="venta in ventas" :key="venta.id">
          <td>{{ venta.id }}</td>
          <td>{{ venta.fecha }}</td>
          <td>{{ venta.cliente?.nombre || venta.clienteId }}</td>
          <td>{{ venta.total }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { defineProps } from 'vue';
const props = defineProps({
  ventas: {
    type: Array,
    default: () => []
  }
});
</script>

<style scoped>
.table-responsive {
  overflow-x: auto;
}
table {
  width: 100%;
  border-collapse: collapse;
  min-width: 500px;
}
th, td {
  border: 1px solid #ccc;
  padding: 8px;
  text-align: left;
}
th {
  background: #f4f4f4;
}
@media (max-width: 700px) {
  table {
    min-width: 350px;
    font-size: 0.95em;
  }
  th, td {
    padding: 6px;
  }
}
</style> 
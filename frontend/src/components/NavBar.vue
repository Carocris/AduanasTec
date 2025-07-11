<template>
  <nav class="navbar">
    <div class="nav-links">
      <router-link to="/productos">Productos</router-link>
      <router-link to="/clientes">Clientes</router-link>
      <router-link to="/ventas">Ventas</router-link>
    </div>
    <button @click="logout">Cerrar sesión</button>
  </nav>
</template>

<script setup>
import { useRouter } from 'vue-router';
const router = useRouter();
function logout() {
  localStorage.removeItem('token');
  router.push('/login');
}
</script>

<style scoped>
.navbar {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  background: #222;
  padding: 1rem;
  align-items: center;
  justify-content: space-between;
}
.nav-links {
  display: flex;
  gap: 1rem;
  flex-wrap: wrap;
}
.navbar a {
  color: #fff;
  text-decoration: none;
  font-weight: bold;
  padding: 0.3rem 0.7rem;
  border-radius: 4px;
  transition: background 0.2s;
}
.navbar a.router-link-active {
  background: #444;
}
button {
  background: #e74c3c;
  color: #fff;
  border: none;
  padding: 0.5rem 1rem;
  cursor: pointer;
  border-radius: 4px;
}
@media (max-width: 600px) {
  .navbar {
    flex-direction: column;
    align-items: stretch;
    gap: 0.5rem;
  }
  .nav-links {
    flex-direction: column;
    gap: 0.5rem;
  }
  button {
    width: 100%;
  }
}
</style> 
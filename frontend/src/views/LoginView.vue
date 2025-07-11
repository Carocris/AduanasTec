<template>
  <div>
    <h2>Login</h2>
    <form @submit.prevent="login">
      <div>
        <label>Usuario:</label>
        <input v-model="username" required />
      </div>
      <div>
        <label>Contraseña:</label>
        <input type="password" v-model="password" required />
      </div>
      <button type="submit">Entrar</button>
    </form>
    <div v-if="error" style="color: red;">{{ error }}</div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';

const username = ref('');
const password = ref('');
const error = ref('');
const router = useRouter();

const login = async () => {
  error.value = '';
  try {
    const response = await axios.post('http://localhost:5000/api/auth/login', {
      username: username.value,
      password: password.value
    });
    localStorage.setItem('token', response.data.token);
    router.push('/productos');
  } catch (e) {
    error.value = 'Usuario o contraseña incorrectos';
  }
};
</script> 
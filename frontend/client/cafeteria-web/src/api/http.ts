import axios from 'axios'

export const http = axios.create({
  baseURL: '/api', // Proxy via Vite -> https://localhost:5001
  headers: { 'Content-Type': 'application/json' }
})
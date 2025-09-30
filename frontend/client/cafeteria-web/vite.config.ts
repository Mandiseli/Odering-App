import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      // proxies /api to API running on localhost:5001
      '/api': {
        target: 'https://localhost:5001',
        changeOrigin: true,
        secure: false
      }
    }
  }
})


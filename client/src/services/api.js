import axios from 'axios'

const api = axios.create({
  baseURL: 'https://catalogojcc.devroma.online/api',
  headers: { 'Content-Type': 'application/json' }
})

export default api


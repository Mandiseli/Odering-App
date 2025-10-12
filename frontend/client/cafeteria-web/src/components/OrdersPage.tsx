import React, { useEffect, useState } from 'react'
import { http } from '../api/http'
import { Order } from '../types'

export default function OrdersPage(){
  const [employeeNumber, setEmployeeNumber] = useState('EMP001')
  const [orders, setOrders] = useState<Order[]>([])

  const load = () => {
    http.get<Order[]>(`/orders/employee/${employeeNumber}`)
      .then(r => setOrders(r.data))
      .catch(err => console.error('Load orders failed', err))
  }

  useEffect(()=> { load() }, [])

  return (
    <section className="card">
      <h2>My Orders</h2>
      <div style={{display:'flex',gap:8, marginBottom:12}}>
        <input value={employeeNumber} onChange={e => setEmployeeNumber(e.target.value)} />
        <button className="btn" onClick={load}>Refresh</button>
      </div>

      <table>
        <thead><tr><th>Order</th><th>Date</th><th>Total (R)</th><th>Status</th></tr></thead>
        <tbody>
          {orders.map(o => (
            <tr key={o.id}>
              <td>#{o.id}</td>
              <td>{new Date(o.orderDate).toLocaleString()}</td>
              <td>{o.totalAmount.toFixed(2)}</td>
              <td><span className={`status ${o.status.toLowerCase()}`}>{o.status}</span></td>
            </tr>
          ))}
          {orders.length === 0 && <tr><td colSpan={4}>No orders yet.</td></tr>}
        </tbody>
      </table>
    </section>
  )
}

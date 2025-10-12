import React, { useEffect, useState } from 'react'
import { http } from '../api/http'
import { Order } from '../types'

export default function AdminPage(){
  const [orders, setOrders] = useState<Order[]>([])

  const load = () => {
    http.get<Order[]>('/admin/orders/pending')
      .then(r => setOrders(r.data))
      .catch(err => console.error('Load pending orders failed', err))
  }

  useEffect(()=> { load() }, [])

  const updateStatus = async (id:number, status:'Preparing'|'Delivering'|'Delivered') => {
    try {
      await http.patch(`/admin/orders/${id}/status/${status}`)
      load()
    } catch (err:any) {
      alert(err.response?.data ?? err.message)
    }
  }

  return (
    <section className="card">
      <h2>Admin — Pending Orders</h2>
      <table>
        <thead><tr><th>#</th><th>Employee</th><th>Total (R)</th><th>Status</th><th>Actions</th></tr></thead>
        <tbody>
          {orders.map(o => (
            <tr key={o.id}>
              <td>{o.id}</td>
              <td>{o.employeeId}</td>
              <td>{o.totalAmount.toFixed(2)}</td>
              <td>{o.status}</td>
              <td>
                <button className="btn" onClick={() => updateStatus(o.id,'Preparing')}>Preparing</button>
                <button className="btn" onClick={() => updateStatus(o.id,'Delivering')}>Delivering</button>
                <button className="btn" onClick={() => updateStatus(o.id,'Delivered')}>Delivered</button>
              </td>
            </tr>
          ))}
          {orders.length === 0 && <tr><td colSpan={5}>No pending orders.</td></tr>}
        </tbody>
      </table>
    </section>
  )
}

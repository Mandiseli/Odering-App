import React, { useEffect, useState } from 'react'
import { http } from '../api/http'
import { Restaurant, MenuItem } from '../types'

export default function MenuView({ restaurant, onBack }: { restaurant: Restaurant, onBack: () => void }) {
  const [items, setItems] = useState<MenuItem[]>([])
  const [cart, setCart] = useState<Record<number, number>>({})
  const [employeeNumber, setEmployeeNumber] = useState('EMP001')

  useEffect(()=> {
    http.get<MenuItem[]>(`/restaurants/${restaurant.id}/menu`)
      .then(r => setItems(r.data))
      .catch(err => console.error('Failed to load menu', err))
  }, [restaurant])

  const inc = (id:number, delta:number) => setCart(prev => ({...prev, [id]: Math.max(0, (prev[id] || 0) + delta)}))

  const place = async () => {
    const payload = {
      employeeNumber,
      items: Object.entries(cart).filter(([_,q]) => q > 0).map(([id,q]) => ({ menuItemId: Number(id), quantity: q }))
    }
    if (payload.items.length === 0) return alert('Add items to cart')
    try {
      const res = await http.post('/orders/place', payload)
      alert(`Order #${res.data.id} placed — total R${res.data.totalAmount}`)
      setCart({})
    } catch (err:any) {
      console.error(err)
      alert(err.response?.data ?? err.message ?? 'Order failed')
    }
  }

  const total = items.reduce((s,it) => s + it.price * (cart[it.id] || 0), 0)

  return (
    <div className="card">
      <button className="btn secondary" onClick={onBack}>← Back</button>
      <h2>{restaurant.name} — Menu</h2>

      <div style={{marginBottom:12}}>
        <label>Employee Number</label>
        <input value={employeeNumber} onChange={e => setEmployeeNumber(e.target.value)} />
      </div>

      <div className="grid cols-3">
        {items.map(it => (
          <div key={it.id} className="card small-card">
            <h4>{it.name}</h4>
            <div className="small">{it.description}</div>
            <div style={{marginTop:8,fontWeight:800}}>R {it.price.toFixed(2)}</div>
            <div style={{display:'flex',gap:8,marginTop:10, alignItems:'center'}}>
              <button className="btn" onClick={() => inc(it.id, -1)}>-</button>
              <div>{cart[it.id] || 0}</div>
              <button className="btn" onClick={() => inc(it.id, 1)}>+</button>
            </div>
          </div>
        ))}
      </div>

      <div style={{display:'flex', justifyContent:'space-between', alignItems:'center', marginTop:12}}>
        <div style={{fontWeight:700}}>Total: R {total.toFixed(2)}</div>
        <button className="btn" onClick={place} disabled={total <= 0}>Place Order</button>
      </div>
    </div>
  )
}

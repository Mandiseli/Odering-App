import { useState } from 'react';
import { MenuItem } from '../models/Menu';
import { http } from '../api/http';

export default function Cart(
  { employeeNumber, items, setItems }:
  { employeeNumber:string, items:Record<number,{item:MenuItem, qty:number}>, setItems:(v:any)=>void }
) {
  const [msg, setMsg] = useState('');
  const total = Object.values(items).reduce((sum, x) => sum + x.item.price * x.qty, 0);

  const place = async () => {
    const payload = {
      employeeNumber,
      items: Object.values(items).map(x => ({ menuItemId: x.item.id, quantity: x.qty }))
    };
    try {
      const res = await http.post('/orders', payload);
      setMsg(`Order #${res.data.id} placed. Total R${res.data.totalAmount}.`);
      setItems({});
    } catch (e:any) {
      setMsg(e.response?.data || e.message);
    }
  };

  return (
    <div>
      <h3>Cart</h3>
      {Object.values(items).length === 0 ? <p>No items</p> : (
        <ul>
          {Object.values(items).map(x => (
            <li key={x.item.id}>{x.item.name} x {x.qty} — R {(x.item.price*x.qty).toFixed(2)}</li>
          ))}
        </ul>
      )}
      <p><b>Total:</b> R {total.toFixed(2)}</p>
      <button disabled={total<=0} onClick={place}>Place Order</button>
      {msg && <p>{msg}</p>}
    </div>
  );
}

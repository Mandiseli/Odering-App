import { useEffect, useState } from 'react';
import { http } from '../api/http';
import { Order } from '../models/Order';

export default function AdminOrdersPage(){
  const [orders, setOrders] = useState<Order[]>([]);
  const load = () => http.get('/admin/orders/pending').then(r=>setOrders(r.data));
  useEffect(load, []);

  const update = async (id:number, status:string) => {
    await http.patch(`/admin/orders/${id}/status/${status}`);
    load();
  };

  return (
    <div>
      <h1>Admin — Manage Orders</h1>
      <ul>
        {orders.map(o => (
          <li key={o.id}>
            #{o.id} — Emp {o.employeeId} — {o.status}
            <select value={o.status} onChange={e=>update(o.id, e.target.value)}>
              <option>Pending</option>
              <option>Preparing</option>
              <option>Delivering</option>
              <option>Delivered</option>
            </select>
          </li>
        ))}
      </ul>
    </div>
  );
}

import { useState } from 'react';
import EmployeesPage from './pages/EmployeesPage';
import RestaurantsPage from './pages/RestaurantsPage';
import OrdersPage from './pages/OrdersPage';
import AdminOrdersPage from './pages/AdminOrdersPage';

export default function App(){
  const [tab, setTab] = useState<'emp'|'rest'|'orders'|'admin'>('emp');
  return (
    <div style={{padding:16}}>
      <nav style={{display:'flex', gap:8, marginBottom:16}}>
        <button onClick={()=>setTab('emp')}>Employees</button>
        <button onClick={()=>setTab('rest')}>Order Food</button>
        <button onClick={()=>setTab('orders')}>My Orders</button>
        <button onClick={()=>setTab('admin')}>Admin</button>
      </nav>
      {tab==='emp' && <EmployeesPage />}
      {tab==='rest' && <RestaurantsPage />}
      {tab==='orders' && <OrdersPage />}
      {tab==='admin' && <AdminOrdersPage />}
    </div>
  );
}

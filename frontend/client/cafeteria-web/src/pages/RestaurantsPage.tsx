import { useState } from 'react';
import RestaurantList from '../components/RestaurantList';
import MenuList from '../components/MenuList';
import Cart from '../components/Cart';
import { MenuItem } from '../models/Menu';
import { Restaurant } from '../models/Restaurant';

export default function RestaurantsPage(){
  const [selected, setSelected] = useState<Restaurant | null>(null);
  const [employeeNumber, setEmployeeNumber] = useState('EMP001');
  const [cart, setCart] = useState<Record<number,{item:MenuItem, qty:number}>>({});

  const onAdd = (mi:MenuItem) =>
    setCart(prev => ({ ...prev, [mi.id]: { item: mi, qty: (prev[mi.id]?.qty || 0) + 1 }}));

  return (
    <div>
      <h1>Order Food</h1>
      <div>
        <label>Employee # </label>
        <input value={employeeNumber} onChange={e=>setEmployeeNumber(e.target.value)} />
      </div>
      <div style={{display:'grid', gridTemplateColumns:'1fr 1fr 1fr', gap:16}}>
        <RestaurantList onSelect={setSelected} />
        {selected && <MenuList restaurantId={selected.id} onAdd={onAdd} />}
        <Cart employeeNumber={employeeNumber} items={cart} setItems={setCart} />
      </div>
    </div>
  );
}

import { useEffect, useState } from 'react';
import { http } from '../api/http';
import { MenuItem } from '../models/Menu';

export default function MenuList({ restaurantId, onAdd }:{ restaurantId:number, onAdd:(item:MenuItem)=>void }) {
  const [items, setItems] = useState<MenuItem[]>([]);
  useEffect(() => {
    http.get(`/restaurants/${restaurantId}/menu`).then(r => setItems(r.data));
  }, [restaurantId]);

  return (
    <div>
      <h3>Menu</h3>
      <ul>
        {items.map(i => (
          <li key={i.id}>
            <div>{i.name} — R {i.price.toFixed(2)}</div>
            <button onClick={()=>onAdd(i)}>Add</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

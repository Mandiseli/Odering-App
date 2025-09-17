import { useEffect, useState } from 'react';
import { http } from '../api/http';
import { Restaurant } from '../models/Restaurant';

export default function RestaurantList({ onSelect }:{ onSelect:(r:Restaurant)=>void }) {
  const [restaurants, setRestaurants] = useState<Restaurant[]>([]);
  useEffect(() => { http.get('/restaurants').then(r => setRestaurants(r.data)); }, []);
  return (
    <div>
      <h2>Restaurants</h2>
      <ul>
        {restaurants.map(r => (
          <li key={r.id}>
            <button onClick={()=>onSelect(r)}>{r.name}</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

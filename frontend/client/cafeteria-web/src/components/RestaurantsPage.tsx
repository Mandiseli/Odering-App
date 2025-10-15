import React, { useEffect, useState } from 'react'
import { http } from '../api/http'
import { Restaurant } from '../types'
import MenuView from './components./MenuView'

export default function RestaurantsPage(){
  const [restaurants, setRestaurants] = useState<Restaurant[]>([])
  const [selected, setSelected] = useState<Restaurant | null>(null)

  useEffect(()=> {
    http.get<Restaurant[]>('/restaurants')
      .then(r => setRestaurants(r.data))
      .catch(err => console.error('Failed load restaurants', err))
  }, [])

  return (
    <section>
      {!selected ? (
        <div className="card">
          <h2>Restaurants</h2>
          <div className="grid cols-3">
            {restaurants.map(r => (
              <div key={r.id} className="card small-card clickable" onClick={()=>setSelected(r)}>
                <h3>{r.name}</h3>
                <div className="small">{r.locationDescription}</div>
                <div className="small">Contact: {r.contactNumber}</div>
              </div>
            ))}
            {restaurants.length === 0 && <p>No restaurants found.</p>}
          </div>
        </div>
      ) : (
        <MenuView restaurant={selected} onBack={() => setSelected(null)} />
      )}
    </section>
  )
}

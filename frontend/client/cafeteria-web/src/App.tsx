import React, { useState } from 'react'
import EmployeesPage from './components/EmployeesPage'
import DepositPage from './components/DepositPage'
import RestaurantsPage from './components/RestaurantsPage'
import OrdersPage from './components/OrdersPage'
import AdminPage from './components/AdminPage'
import './styles/index.css'

export default function App() {
  const [view, setView] = useState<'employees'|'deposit'|'restaurants'|'orders'|'admin'>('employees')

  return (
    <div className="container">
      <header className="header">
        <div>
          <h1 className="app-title">🍽 Employee Cafeteria — Credit & Ordering</h1>
          <p className="subtitle">Demo app — deposit, order, and manage cafeteria credits</p>
        </div>
      </header>

      <nav className="nav">
        <button className="btn" onClick={() => setView('employees')}>Employees</button>
        <button className="btn" onClick={() => setView('deposit')}>Deposit</button>
        <button className="btn" onClick={() => setView('restaurants')}>Order Food</button>
        <button className="btn" onClick={() => setView('orders')}>My Orders</button>
        <button className="btn" onClick={() => setView('admin')}>Admin</button>
      </nav>

      <main>
        {view === 'employees' && <EmployeesPage />}
        {view === 'deposit' && <DepositPage />}
        {view === 'restaurants' && <RestaurantsPage />}
        {view === 'orders' && <OrdersPage />}
        {view === 'admin' && <AdminPage />}
      </main>

      <footer className="footer">
        <small>Built with ❤️ • Demo</small>
      </footer>
    </div>
  )
}

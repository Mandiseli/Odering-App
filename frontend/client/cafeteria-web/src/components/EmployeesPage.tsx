import React, { useEffect, useState } from 'react'
import { http } from '../api/http'
import { Employee } from '../types'

export default function EmployeesPage(){
  const [employees, setEmployees] = useState<Employee[]>([])

  useEffect(()=> {
    http.get<Employee[]>('/employees')
      .then(r => setEmployees(r.data))
      .catch(err => console.error('Failed to load employees', err))
  }, [])

  return (
    <section className="card">
      <h2>Employees</h2>
      <div className="grid cols-2">
        {employees.map(e => (
          <div className="card small-card" key={e.id}>
            <div className="card-row">
              <div>
                <h3>{e.name}</h3>
                <div className="small">#{e.employeeNumber}</div>
              </div>
              <div style={{textAlign:'right'}}>
                <div className="balance">R {e.balance.toFixed(2)}</div>
                <div className="small">Balance</div>
              </div>
            </div>
          </div>
        ))}
        {employees.length === 0 && <p>No employees found.</p>}
      </div>
    </section>
  )
}

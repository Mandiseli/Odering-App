import React, { useState } from 'react'
import { http } from '../api/http'

export default function DepositPage(){
  const [employeeNumber, setEmployeeNumber] = useState('')
  const [amount, setAmount] = useState<number | ''>('')

  const submit = async (e: React.FormEvent) => {
    e.preventDefault()
    if (!employeeNumber || !amount) return alert('Provide employee number and amount')
    try {
      // body uses depositAmount key (to match some backend DTOs)
      const res = await http.post('/deposits', { employeeNumber, depositAmount: amount })
      const data = res.data
      alert(`Deposited R${data.depositAmount ?? amount}. Bonus: R${data.bonusApplied ?? 0}. New balance: R${data.newBalance ?? data.balance}`)
      setEmployeeNumber('')
      setAmount('')
    } catch (err: any) {
      console.error(err)
      alert(err.response?.data ?? err.message ?? 'Deposit failed')
    }
  }

  return (
    <section className="card" style={{maxWidth:560}}>
      <h2>Make a Deposit</h2>
      <form onSubmit={submit}>
        <label>Employee Number</label>
        <input type="text" value={employeeNumber} onChange={e=>setEmployeeNumber(e.target.value)} placeholder="EMP001" />

        <label>Amount (R)</label>
        <input type="number" value={amount as any} onChange={e=>setAmount(e.target.value ? parseFloat(e.target.value) : '')} placeholder="250" />

        <button className="btn" type="submit">Deposit</button>
      </form>
    </section>
  )
}

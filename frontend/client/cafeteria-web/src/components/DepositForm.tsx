import { useState } from 'react';
import { http } from '../api/http';

export default function DepositForm() {
  const [employeeNumber, setEmployeeNumber] = useState('EMP001');
  const [amount, setAmount] = useState<number>(250);
  const [message, setMessage] = useState('');

  const submit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await http.post('/deposits', { employeeNumber, depositAmount: amount });
      const d = res.data;
      setMessage(`Deposited R${d.depositAmount}. Bonus: R${d.bonusApplied}. New balance: R${d.newBalance}.`);
    } catch (err: any) {
      setMessage(err.response?.data || err.message);
    }
  };

  return (
    <form onSubmit={submit}>
      <h3>Make a Deposit</h3>
      <div>
        <label>Employee #</label>
        <input value={employeeNumber} onChange={e=>setEmployeeNumber(e.target.value)} />
      </div>
      <div>
        <label>Amount</label>
        <input type="number" value={amount} onChange={e=>setAmount(parseFloat(e.target.value))} />
      </div>
      <button type="submit">Deposit</button>
      {message && <p>{message}</p>}
    </form>
  );
}

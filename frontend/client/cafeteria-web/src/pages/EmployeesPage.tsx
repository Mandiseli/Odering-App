import EmployeeList from '../components/EmployeeList';
import DepositForm from '../components/DepositForm';

export default function EmployeesPage() {
  return (
    <div>
      <h1>Employees</h1>
      <div style={{display:'grid', gridTemplateColumns:'1fr 1fr', gap:16}}>
        <EmployeeList />
        <DepositForm />
      </div>
    </div>
  );
}

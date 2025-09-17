export interface Employee {
  id: number;
  name: string;
  employeeNumber: string;
  balance: number;
  lastDepositMonth?: string | null;
  monthlyDepositTotal: number;
}

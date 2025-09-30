export interface Employee {
  id: number;
  name: string;
  employeeNumber: string;
  balance: number;
  lastDepositMonth?: string | null;
  monthlyDepositTotal?: number;
}

export interface Restaurant {
  id: number;
  name: string;
  locationDescription?: string;
  contactNumber?: string;
}

export interface MenuItem {
  id: number;
  restaurantId: number;
  name: string;
  description?: string;
  price: number;
}

export type OrderStatus = 'Pending' | 'Preparing' | 'Delivering' | 'Delivered';

export interface OrderItem {
  id?: number;
  orderId?: number;
  menuItemId: number;
  quantity: number;
  unitPriceAtTimeOfOrder?: number;
}

export interface Order {
  id: number;
  employeeId: number;
  orderDate: string;
  totalAmount: number;
  status: OrderStatus;
  items: OrderItem[];
}

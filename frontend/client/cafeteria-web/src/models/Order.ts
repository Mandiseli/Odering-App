export type OrderStatus = 'Pending' | 'Preparing' | 'Delivering' | 'Delivered';

export interface OrderItem {
  id: number;
  orderId: number;
  menuItemId: number;
  quantity: number;
  unitPriceAtTimeOfOrder: number;
}

export interface Order {
  id: number;
  employeeId: number;
  orderDate: string;
  totalAmount: number;
  status: OrderStatus;
  items: OrderItem[];
}

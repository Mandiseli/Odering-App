namespace Cafeteria.Api.Dtos
{
    public record OrderItemRequest(int MenuItemId, int Quantity);
    public record PlaceOrderRequest(string EmployeeNumber, List<OrderItemRequest> Items);
}

using SteamClone.Backend.DTOs;
using SteamClone.Backend.Entities;

namespace SteamClone.Backend.Extensions;

public static class OrderExtension
{
    public static OrderResponseDto MapToDto(this Order order)
    {
        return new OrderResponseDto
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate,
            Status = order.Status,
            Items = order.Items.Select(item => new OrderItemDto
            {
                GameId = item.GameId,
                Title = item.Game.Title,
                Price = item.Price,
                Quantity = item.Quantity,
                ImageUrl = item.ImageUrl
            }).ToList()
        };
    }
}

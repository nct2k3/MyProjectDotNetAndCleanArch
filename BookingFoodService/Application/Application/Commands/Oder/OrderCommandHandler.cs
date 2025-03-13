using System.Text.Json;
using Application.Application.Common;
using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.RabbitMQMessageQueeu;
using Domain.Entities;
using MediatR;

namespace Application.Application.Commands.Oder;

public class OrderCommandHandler : IRequestHandler<OrderCommand, OrderResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRabbitMqMessageQueeu _rabbitMqMessageQueeu;

    public OrderCommandHandler(IUnitOfWork unitOfWork, IRabbitMqMessageQueeu rabbitMqMessageQueeu)
    {
        _unitOfWork = unitOfWork;
        _rabbitMqMessageQueeu = rabbitMqMessageQueeu;
    }

    public async Task<OrderResult> Handle(OrderCommand request, CancellationToken cancellationToken)
    {
        var orderRepository = _unitOfWork.Repository<Order>();
        var orderDetailRepository = _unitOfWork.Repository<OrderDetail>();
        var AnUser= _unitOfWork.Repository<User>();
        var Food = _unitOfWork.Repository<FoodItems>();
        
        var order = new Order
        {
            UserId = request.UserId,
            OrderDate = request.OrderDate,
            Status = request.Status,
            TotalAmount = request.TotalAmount
        };

        await orderRepository.AddAsync(order);
        User dataUser = await AnUser.GetByIdAsync(request.UserId);
        
        var orderDetails = new List<OrderDetail>();
        var dataFoodItems = new List<FoodItems>();
        foreach (var detail in request.Details)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = order.Id, 
                FoodId = detail.FoodId,
                Quantity = detail.Quantity,
                Price = detail.Price
            };
            orderDetails.Add(orderDetail);
            await orderDetailRepository.AddAsync(orderDetail);
            FoodItems foodItems= await Food.GetByIdAsync(detail.FoodId);
            dataFoodItems.Add(foodItems);
        }

        var rabbitMessage = new
        {
            OrderId = order.Id,
            User = new
            {
                dataUser.Id,
                dataUser.FirstName,
                dataUser.LastName,
                dataUser.Email
            },
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Details = orderDetails.Select(d => new
            {
                d.Id,
                d.FoodId,
                d.Quantity,
                d.Price
            }),
        };
        var jsonMessage = JsonSerializer.Serialize(rabbitMessage);
       _rabbitMqMessageQueeu.SendMessageQueeu("Invoices" ,jsonMessage);
        
        //await _unitOfWork.SaveChangesAsync();
        
        return new OrderResult
        {
            Order = order,
            OrderDetails = orderDetails
        };
    }
}
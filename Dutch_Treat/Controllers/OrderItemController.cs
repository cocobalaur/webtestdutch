using Dutch_Treat.Data.Interfaces;
using Dutch_Treat.Data;
using Microsoft.AspNetCore.Mvc;
using Dutch_Treat.Controllers.Base;
using DutchTreat.Data.Entities;

namespace Dutch_Treat.Controllers
{
    public class OrderItemController : BaseController<OrderItem>
    {
        private readonly ILogger<OrderItemController> _logger;
        private readonly IDutchOrderItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderItemController(ILogger<OrderItemController> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork.GetRepository<IDutchRepository<OrderItem>>()) //dependency injection from repository
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repository = (IDutchOrderItemRepository)_unitOfWork.GetRepository<IDutchRepository<OrderItem>>();
        }
    }
}

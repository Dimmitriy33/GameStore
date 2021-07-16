using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.Web.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region Services

        private readonly IClaimsReader _claimsHelper;
        private readonly IOrderService _orderService;

        #endregion

        public OrdersController(IOrderService orderService, IClaimsReader claimsHelper)
        {
            _orderService = orderService;
            _claimsHelper = claimsHelper;
        }

        /// <summary>
        /// Add products to order
        /// </summary>
        /// <param name="orderGamesDTO">Order games models</param>
        /// <response code="201">Add some products to order successfully</response>
        /// <response code="400">Unable to create an order</response>
        [HttpPost]
        public async Task<ActionResult<string>> AddProductsToOrder([BindRequired] ICollection<OrderGameDTO> orderGamesDTO)
        {
            var userId = _claimsHelper.GetUserId(User).Result;

            var orderItems = orderGamesDTO.Select(gameDTO =>
                new OrderItemDTO
                {
                    ProductId = gameDTO.ProductId,
                    Amount = gameDTO.Amount,
                    UserId = userId,
                })
                .ToList();

            var result = await _orderService.AddProductsToOrderAsync(orderItems);

            if (result.ServiceResultType is ServiceResultType.Success)
            {
                return StatusCode((int)HttpStatusCode.Created);
            }

            return StatusCode((int)result.ServiceResultType);
        }

        /// <summary>
        /// Get Order products
        /// </summary>
        /// <param name="orderList">List of orders unique identifier</param>
        /// <response code="200">Found order list successfully</response>
        [HttpGet]
        public async Task<ActionResult<List<GameResponseDTO>>> GetOrderList([FromQuery] ICollection<Guid> orderList)
        {
            var userId = _claimsHelper.GetUserId(User).Result;

            var games = !orderList.Any()
                ? await _orderService.SearchForOrderListByUserIdAsync(userId)
                : await _orderService.SearchForOrderListByOrdersIdAsync(orderList);

            return StatusCode((int)games.ServiceResultType, games.Result);
        }

        /// <summary>
        /// Remove selected items
        /// </summary>
        /// <param name="orderList">Collection of orderId</param>
        /// <response code="204">Successful remove items attempt</response>
        /// <response code="400">Invalid remove items attempt</response>
        [HttpDelete]
        public async Task<ActionResult<string>> RemoveItems([FromQuery, BindRequired] ICollection<Guid> orderList)
        {
            var result = await _orderService.RemoveSelectedItemsAsync(orderList);

            if (result.ServiceResultType is ServiceResultType.Success)
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Reject selected orders
        /// </summary>
        /// <param name="orderList">collection of orderId</param>
        /// <response code="204">Successful soft remove items attempt</response>
        /// <response code="400">Invalid soft remove items attempt</response>
        [HttpDelete("soft")]
        public async Task<ActionResult<string>> SoftRemoveItems([FromQuery, BindRequired] ICollection<Guid> orderList)
        {
            var result = await _orderService.SoftRemoveSelectedItemsAsync(orderList);

            if (result.ServiceResultType is ServiceResultType.Success)
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Buy selected items
        /// </summary>
        /// <param name="orderList">collection of orderId</param>
        /// <response code="204">Successful buy attempt</response>
        /// <response code="400">Invalid buy attempt</response>
        [HttpPost("buy")]
        public async Task<ActionResult<string>> BuySelectedItems([FromQuery, BindRequired] ICollection<Guid> orderList)
        {
            var result = await _orderService.BuySelectedItemsAsync(orderList);

            if (result.ServiceResultType is ServiceResultType.Success)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}

using System;
using System.Linq.Expressions;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.BLL.Interfaces
{
    public interface IProductSelectionHelper
    {
        Expression<Func<Product, bool>> GetFilterExpression(string filter, string value);
        OrderType GetOrderType(string value);
        Expression<Func<Product, object>> GetSortExpression(string sort);
    }
}

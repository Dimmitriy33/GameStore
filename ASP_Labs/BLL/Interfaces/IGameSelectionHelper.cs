using System;
using System.Linq.Expressions;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Interfaces
{
    public interface IGameSelectionHelper
    {
        Expression<Func<Product, bool>> GetFilterExpression(string filter, string value);
        Expression<Func<Product, object>> GetSortExpression(string sort);
    }
}

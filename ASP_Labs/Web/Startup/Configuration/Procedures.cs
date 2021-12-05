using Microsoft.EntityFrameworkCore;
using WebApp.DAL;

namespace WebApp.Web.Startup.Configuration
{
    public static class Procedures
    {
        public static void Initialize(ApplicationDbContext context)
        {
            var proc = $@"
			/****** Object:  StoredProcedure [dbo].[ChangeOrderStatus]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[ChangeOrderStatus] (
				@order_id	uniqueidentifier,
				@status	int
			)
			AS
				SET NOCOUNT ON;  
				BEGIN
					UPDATE Orders
					SET	Status = @status
					FROM Orders
					WHERE  OrderId = @order_id
				END

			/****** Object:  StoredProcedure [dbo].[ChangeProductAvgRating]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[ChangeProductAvgRating] (
				@product_id	uniqueidentifier
			)
			AS
				SET NOCOUNT ON;  
				BEGIN
					UPDATE Products
					SET	TotalRating = (
						SELECT AVG([t0].[Rating]) AS [value]
						FROM [ProductRating] AS [t0]
						WHERE [t0].[ProductId] = @product_id)
					WHERE  ProductId = @product_id
				END

			/****** Object:  StoredProcedure [dbo].[CheckProductsExistence]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[CheckProductsExistence] (
				@product_id	uniqueidentifier
			)
			AS
				SET NOCOUNT ON;  
				BEGIN
					SELECT ProductId
					FROM Products AS [Result]
					WHERE ProductId = @product_id;
				END

			/****** Object:  StoredProcedure [dbo].[GetGameById]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[GetGameById] (
				@product_id uniqueidentifier
			)
			AS
				SET NOCOUNT ON;  
				BEGIN
					SELECT *   
					FROM Products  
					WHERE ProductId = @product_id
				END
			/****** Object:  StoredProcedure [dbo].[GetGamesByOrderId]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[GetGamesByOrderId] (
				@order_id	uniqueidentifier
			)
			AS
				SET NOCOUNT ON;  
				SELECT *   
				FROM Orders  
				WHERE OrderId = @order_id

			/****** Object:  StoredProcedure [dbo].[GetGamesByUserId]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[GetGamesByUserId] (
				@user_id	uniqueidentifier
			)
			AS
				SET NOCOUNT ON;  
				SELECT *   
				FROM Orders  
				WHERE UserId = @user_id

			/****** Object:  StoredProcedure [dbo].[GetProductByName]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[GetProductByName] (
				@term	nvarchar(255),
				@limit	int,
				@offset	int
			)
			AS
				SET NOCOUNT ON;  
				BEGIN
					SELECT *
					FROM (
						SELECT ROW_NUMBER() OVER (ORDER BY [t0].[ProductId], [t0].[Name], [t0].[Platform], [t0].[DateCreated], [t0].[TotalRating], [t0].[Genre], [t0].[Rating], [t0].[Logo], [t0].[Background], [t0].[Price], [t0].[Count], [t0].[IsDeleted]) AS [ROW_NUMBER], [t0].[ProductId], [t0].[Name], [t0].[Platform], [t0].[DateCreated], [t0].[TotalRating], [t0].[Genre], [t0].[Rating], [t0].[Logo], [t0].[Background], [t0].[Price], [t0].[Count], [t0].[IsDeleted]
							FROM [Products] AS [t0]
							WHERE Name LIKE @term + '%'
					) AS [t1]
					WHERE [t1].[ROW_NUMBER] BETWEEN @offset + 1 AND @offset + @limit
					ORDER BY [t1].[ROW_NUMBER]
				END

			/****** Object:  StoredProcedure [dbo].[GetTopPopularPlatforms]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[GetTopPopularPlatforms] (
				@count	int
			)
			AS
				SET NOCOUNT ON;  
				BEGIN
					SELECT TOP (@count) [t1].[Platform]
					FROM (
						SELECT COUNT(*) AS [value], [t0].[Platform]
						FROM [Products] AS [t0]
						GROUP BY [t0].[Platform]
						) AS [t1]
					ORDER BY [t1].[value] DESC
				END

			/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[GetUserById] (
				@user_id uniqueidentifier
			)
			AS
				SET NOCOUNT ON;  
				BEGIN
					SELECT TOP(1) *
					FROM AspNetUsers  
					WHERE Id = @user_id
				END

			/****** Object:  StoredProcedure [dbo].[ProductSoftDelete]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[ProductSoftDelete] (
				@product_id	uniqueidentifier
			)
			AS
				SET NOCOUNT ON;  
				BEGIN
					UPDATE Products
					SET	IsDeleted = 'true'
					WHERE  ProductId = @product_id
				END

			/****** Object:  StoredProcedure [dbo].[RemoveOrderById]    Script Date: 05.12.2021 22:39:39 ******/
			CREATE OR ALTER PROCEDURE [dbo].[RemoveOrderById] (
				@order_id	uniqueidentifier
			)
			AS
				SET NOCOUNT ON;  
				BEGIN
					DELETE
					FROM Orders
					WHERE  OrderId = @order_id
				END
			";
            context.Database.ExecuteSqlRaw(proc);
        }
    }
}

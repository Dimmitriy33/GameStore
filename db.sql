USE [aspnet-ASP_Labs]
GO
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

-- Get games by userId from table orders
CREATE or ALTER PROCEDURE [dbo].GetGamesByUserId (
	@user_id	uniqueidentifier
)
AS
	SET NOCOUNT ON;  
    SELECT *   
    FROM Orders  
    WHERE UserId = @user_id
GO

--DROP PROCEDURE GetGamesByUserId;  
--GO 

-- Get games by orderId from table orders
CREATE or ALTER PROCEDURE [dbo].GetGamesByOrderId (
	@order_id	uniqueidentifier
)
AS
	SET NOCOUNT ON;  
    SELECT *   
    FROM Orders  
    WHERE OrderId = @order_id
GO

--DROP PROCEDURE GetGamesByOrderId;  
--GO 

-- Remove order from table orders by order id
CREATE or ALTER PROCEDURE [dbo].RemoveOrderById (
	@order_id	uniqueidentifier
)
AS
	SET NOCOUNT ON;  
	BEGIN
        DELETE 
		FROM Orders
        WHERE  OrderId = @order_id
    END
GO

--DROP PROCEDURE RemoveOrderById;  

-- Change status of elem from table orders by order id and status
CREATE or ALTER PROCEDURE [dbo].ChangeOrderStatus (
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
GO

--DROP PROCEDURE ChangeOrderStatus;  
--GO 

-- Get @count popular platform
CREATE or ALTER PROCEDURE [dbo].GetTopPopularPlatforms (
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
GO

--exec GetTopPopularPlatforms 3;
--DROP PROCEDURE GetTopPopularPlatforms;  
--GO 

-- Get product by name(@term), skip @offset and take @limit items
CREATE or ALTER PROCEDURE [dbo].GetProductByName (
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
GO
--exec [dbo].GetProductByName 'm',2,0;
--GO 

-- Get product by id
CREATE or ALTER PROCEDURE [dbo].GetGameById (
	@product_id uniqueidentifier
)
AS
	SET NOCOUNT ON;  
	BEGIN
        SELECT *   
		FROM Products  
		WHERE ProductId = @product_id
    END
GO

--	exec GetGameById '5acb3515-9b43-4b80-9338-1ff4e0dc972b';
--DROP PROCEDURE GetProductByName;  
--GO 

-- Change product isDeleted field  
CREATE or ALTER PROCEDURE [dbo].ProductSoftDelete (
	@product_id	uniqueidentifier
)
AS
	SET NOCOUNT ON;  
	BEGIN
        UPDATE Products
        SET	IsDeleted = 'true'
        WHERE  ProductId = @product_id
    END
GO

--	exec ProductSoftDelete '5acb3515-9b43-4b80-9338-1ff4e0dc972b';
--DROP PROCEDURE ProductSoftDelete;  
--GO 

-- Change product rating
CREATE or ALTER PROCEDURE [dbo].ChangeProductAvgRating (
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
GO

--	exec ChangeProductAvgRating '5acb3515-9b43-4b80-9338-1ff4e0dc972b';
--DROP PROCEDURE ChangeProductAvgRating;  
--GO 

-- Check product existence
CREATE or ALTER PROCEDURE [dbo].CheckProductsExistence (
	@product_id	uniqueidentifier
)
AS
	SET NOCOUNT ON;  
	BEGIN
		SELECT ProductId
		FROM Products AS [Result]
		WHERE ProductId = @product_id;
    END
GO

--	exec CheckProductsExistence '5ACB3515-9B43-4B80-9338-1FF4E0DC972B';
--DROP PROCEDURE ChangeProductAvgRating;  
--GO 

-- Get user by id
CREATE or ALTER PROCEDURE [dbo].GetUserById (
	@user_id uniqueidentifier
)
AS
	SET NOCOUNT ON;  
	BEGIN
        SELECT TOP(1) *
		FROM AspNetUsers  
		WHERE Id = @user_id
    END
GO

--	exec GetUserById 'd3d8f2e3-9ce6-40a3-5611-08d9b77182f6';
--DROP PROCEDURE GetUserById;  
--GO 


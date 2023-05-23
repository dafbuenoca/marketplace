USE Sales;
GO

EXEC marketplace.WriteOrderProduct
     @OrderId = 4,
     @ProductId = 100,
     @Qty = 3,
     @UnitPrice = 10.00,
     @TotalPrice = 30.00;


SELECT * FROM marketplace.OrderProduct WHERE OrderId = 4 AND ProductId = 100;

USE Sales;
GO

SELECT * FROM sys.procedures WHERE name = 'WriteOrder' AND SCHEMA_NAME(schema_id) = 'marketplace';


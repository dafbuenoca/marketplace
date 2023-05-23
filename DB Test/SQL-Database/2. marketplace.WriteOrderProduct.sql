USE Sales;
GO

CREATE OR ALTER PROCEDURE marketplace.WriteOrderProduct
    @OrderId int,
    @ProductId int = NULL,
    @Delete bit = 0,
    @Qty int = NULL,
    @UnitPrice money = NULL,
    @TotalPrice money = NULL
AS
	
    IF (@Delete = 0) 
    BEGIN
            
        IF EXISTS (SELECT 1 FROM marketplace.[Order] WHERE OrderId = @OrderId AND [Status] = 'OPEN')
        BEGIN
                
            IF (@ProductId IS NOT NULL AND @Qty IS NOT NULL AND @UnitPrice IS NOT NULL AND @TotalPrice IS NOT NULL
                AND @Qty > 0 AND @UnitPrice > 0 AND @TotalPrice > 0)
            BEGIN
                MERGE marketplace.OrderProduct AS target
                USING (VALUES (@OrderId, @ProductId, @Qty, @UnitPrice, @TotalPrice)) AS source (OrderId, ProductId, Qty, UnitPrice, TotalPrice)
                ON (target.OrderId = source.OrderId AND target.ProductId = source.ProductId)
                WHEN MATCHED THEN
                    UPDATE SET Qty = source.Qty, UnitPrice = source.UnitPrice, TotalPrice = source.TotalPrice
                WHEN NOT MATCHED THEN
                    INSERT (OrderId, ProductId, Qty, UnitPrice, TotalPrice)
                    VALUES (source.OrderId, source.ProductId, source.Qty, source.UnitPrice, source.TotalPrice);
            END
        END
    END
    ELSE 
    BEGIN
        IF EXISTS (SELECT 1 FROM marketplace.[Order] WHERE OrderId = @OrderId AND [Status] = 'OPEN')
        BEGIN
            DELETE FROM marketplace.OrderProduct WHERE OrderId = @OrderId AND ProductId = @ProductId;
        END
    END
GO
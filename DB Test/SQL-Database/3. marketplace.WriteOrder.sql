USE Sales;
GO

CREATE OR ALTER PROCEDURE marketplace.WriteOrder
    @OrderId int = NULL OUTPUT,
    @Delete bit = 0,
    @CustomerId int = NULL,
    @OrderDate datetime = NULL,
    @Status varchar(20) = NULL
AS
BEGIN
    -- Validaciones
    IF @Delete = 0
    BEGIN
        -- Creación o actualización del pedido utilizando MERGE
        DECLARE @InsertedOrderId TABLE (OrderId int);

        MERGE marketplace.[Order] AS target
        USING (VALUES (@OrderId)) AS source (OrderId)
        ON (target.OrderId = source.OrderId)
        WHEN MATCHED THEN
            UPDATE SET
                CustomerId = @CustomerId,
                OrderDate = @OrderDate,
                [Status] = @Status
        WHEN NOT MATCHED THEN
            INSERT (CustomerId, OrderDate, [Status])
            VALUES (@CustomerId, @OrderDate, @Status)
            OUTPUT inserted.OrderId INTO @InsertedOrderId;

        -- Obtener el identificador del pedido recién creado
        IF @OrderId IS NOT NULL
        BEGIN
            SELECT @OrderId = OrderId FROM @InsertedOrderId;
        END

        -- Llamar al procedimiento almacenado marketplace.WriteOrderProduct si es necesario
        IF @OrderId IS NOT NULL
        BEGIN
            EXEC marketplace.WriteOrderProduct
                @OrderId = @OrderId,
                @Delete = @Delete -- Proporciona aquí los valores necesarios para marketplace.WriteOrderProduct
        END
    END
    ELSE
    BEGIN
        -- Eliminación del pedido y llamada a marketplace.WriteOrderProduct
        DELETE FROM marketplace.[Order] WHERE OrderId = @OrderId;
        EXEC marketplace.WriteOrderProduct
            @OrderId = @OrderId,
            @Delete = @Delete -- Proporciona aquí los valores necesarios para marketplace.WriteOrderProduct
    END
END
GO
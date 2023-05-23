DECLARE @NewOrderId int;
EXEC marketplace.WriteOrder
    @OrderId = @NewOrderId OUTPUT,
    @Delete = 0,
    @CustomerId = 1,
    @OrderDate = '2023-05-21 15:15:51.107',
    @Status = 'OPEN';

SELECT @NewOrderId AS NewOrderId;
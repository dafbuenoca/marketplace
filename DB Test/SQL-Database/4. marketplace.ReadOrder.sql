USE Sales;
GO

CREATE OR ALTER PROCEDURE marketplace.ReadOrder
    @FilterXml xml,
    @DataTablesXml xml,
    @Language varchar(2) = 'EN'
AS
BEGIN
    -- Validaciones

    -- Lectura de par�metros
    DECLARE @FilterTable TABLE (
        TableName varchar(50)
    );

    INSERT INTO @FilterTable (TableName)
        SELECT DataTable.value('(TableName)[1]', 'varchar(50)')
        FROM @DataTablesXml.nodes('/ROOT/DataTable') AS DataTables(DataTable);

    -- 'Order-Header'
    IF EXISTS (SELECT 1 FROM @FilterTable WHERE TableName = 'Order-Header')
    BEGIN
        SELECT TableName = 'Order-Header';

        SELECT
            O.OrderId,
            O.CustomerId,
            C.CustomerName,
            O.OrderDate,
            O.Status
        FROM marketplace.[Order] AS O
        INNER JOIN marketplace.Customer AS C ON O.CustomerId = C.CustomerId
        WHERE
            (@FilterXml.exist('/ROOT/Filter/CustomerId') = 0 OR O.CustomerId = @FilterXml.value('(/ROOT/Filter/CustomerId)[1]', 'int'))
            AND (@FilterXml.exist('/ROOT/Filter/OrderId') = 0 OR O.OrderId = @FilterXml.value('(/ROOT/Filter/OrderId)[1]', 'int'))
            AND (@FilterXml.exist('/ROOT/Filter/Status') = 0 OR O.Status = @FilterXml.value('(/ROOT/Filter/Status)[1]', 'nvarchar(20)'))
        ORDER BY O.OrderId;
    END;

    -- 'Order-Detail'
    IF EXISTS (SELECT 1 FROM @FilterTable WHERE TableName = 'Order-Detail')
    BEGIN
        SELECT TableName = 'Order-Detail';

        SELECT
            OP.OrderId,
            OP.ProductId,
            OP.Qty,
            OP.UnitPrice,
            OP.TotalPrice
        FROM marketplace.OrderProductComplete AS OP
        INNER JOIN marketplace.[Order] AS O ON OP.OrderId = O.OrderId
        WHERE
            (@FilterXml.exist('/ROOT/Filter/CustomerId') = 0 OR O.CustomerId = @FilterXml.value('(/ROOT/Filter/CustomerId)[1]', 'int'))
            AND (@FilterXml.exist('/ROOT/Filter/OrderId') = 0 OR OP.OrderId = @FilterXml.value('(/ROOT/Filter/OrderId)[1]', 'int'))
            AND (@FilterXml.exist('/ROOT/Filter/Status') = 0 OR O.Status = @FilterXml.value('(/ROOT/Filter/Status)[1]', 'nvarchar(20)'))
        ORDER BY OP.OrderId;
    END;

    -- 'Order-History-Json'
    IF EXISTS (SELECT 1 FROM @FilterTable WHERE TableName = 'Order-History-Json')
    BEGIN
        SELECT TableName = 'Order-History-Json';

        SELECT
            O.OrderId AS _id,
            O.OrderDate,
            O.Status,
            (
                SELECT
                    OP.ProductId,
                    P.ProductName,
                    OP.Qty,
                    OP.UnitPrice,
                    OP.TotalPrice
                FROM marketplace.OrderProductComplete AS OP
                INNER JOIN marketplace.Product AS P ON OP.ProductId = P.ProductId
                WHERE OP.OrderId = O.OrderId
                FOR JSON PATH
            ) AS Details
        FROM marketplace.[Order] AS O
        WHERE
            (@FilterXml.exist('/ROOT/Filter/CustomerId') = 0 OR O.CustomerId = @FilterXml.value('(/ROOT/Filter/CustomerId)[1]', 'int'))
            AND (@FilterXml.exist('/ROOT/Filter/OrderId') = 0 OR O.OrderId = @FilterXml.value('(/ROOT/Filter/OrderId)[1]', 'int'))
            AND (@FilterXml.exist('/ROOT/Filter/Status') = 0 OR O.Status = @FilterXml.value('(/ROOT/Filter/Status)[1]', 'nvarchar(20)'))
        ORDER BY O.OrderId
        FOR JSON PATH;
    END;
END;
GO
/*
-------------------- By OrderId
DECLARE
    @FilterXml xml = '
	<ROOT>
		<Filter OrderId="1"/>
	</ROOT>',
    @DataTablesXml xml = '
	<ROOT>
		<DataTable TableName="Order-Header"/>
		<DataTable TableName="Order-Detail"/>
	</ROOT>';

EXEC marketplace.ReadOrder
    @FilterXml = @FilterXml,
    @DataTablesXml = @DataTablesXml;
GO

-------------------- By CustomerId and Order in OPEN Status
DECLARE
    @FilterXml xml = '
	<ROOT>
		<Filter CustomerId="1" Status="OPEN"/>
	</ROOT>',
    @DataTablesXml xml = '
	<ROOT>
		<DataTable TableName="Order-Header"/>
		<DataTable TableName="Order-Detail"/>
	</ROOT>';

EXEC marketplace.ReadOrder
    @FilterXml = @FilterXml,
    @DataTablesXml = @DataTablesXml;
*/

-------------------- Order-History by CustomerId 
DECLARE
    @FilterXml xml = '
	<ROOT>
		<!-- <Filter CustomerId="1"/> -->
		<Filter CustomerId="-1" />
	</ROOT>',
    @DataTablesXml xml = '
	<ROOT>
		<DataTable TableName="Order-History-Json"/>
	</ROOT>';

EXEC marketplace.ReadOrder
    @FilterXml = @FilterXml,
    @DataTablesXml = @DataTablesXml,
    @Language = 'EN';


DECLARE @FilterXml xml = '
<ROOT>
    <!-- <Filter CustomerId="1"/> -->
    <Filter CustomerId="-1" />
</ROOT>';

DECLARE @DataTablesXml xml = '
<ROOT>
    <DataTable TableName="Order-History-Json"/>
</ROOT>';

DECLARE @JsonResult nvarchar(max);

EXEC marketplace.ReadOrder
    @FilterXml = @FilterXml,
    @DataTablesXml = @DataTablesXml,
    @Language = 'EN',
    @JsonResult = @JsonResult OUTPUT;

-- Print the formatted JSON
SELECT JSON_QUERY(@JsonResult) AS [Order-History-Json]; 
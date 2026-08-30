-- =============================================
-- Creacion base de datos
-- =============================================

IF NOT EXISTS (
SELECT 1
FROM sys.databases
WHERE name = 'ProductManagementDb'
)
BEGIN
CREATE DATABASE ProductManagementDb;
END
GO

USE ProductManagementDb; 
GO

-- =============================================
-- Creacion tablas
-- =============================================

-- validacion tabla products
IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL
DROP TABLE dbo.Products;
GO

CREATE TABLE [PAR].[ECO_MORTGAGE_APPRAISAL_STATUS](
	[ID] [int] NOT NULL,
	[DESCRIPTION] [varchar](100) NOT NULL,
	[IS_ENABLED] [bit] NOT NULL,
 CONSTRAINT [PK_ECO_MORTGAGE_APPRAISAL_STATUS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE DBO.PRODUCTS
(
	ID INT IDENTITY(1,1) NOT NULL, 
	[NAME] NVARCHAR(100) NOT NULL,
	[DESCRIPTION] NVARCHAR(500) NOT NULL,
	[PRICE] DECIMAL(18,2) NOT NULL,
	DATE_CREATION DATETIME NOT NULL
	CONSTRAINT DF_PRODUCTS_DATECREATION
	DEFAULT(GETDATE())
 CONSTRAINT [PK_PRODUCTS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Creacion sp creacion de productos

CREATE OR ALTER PROCEDURE DBO.SP_PRODUCT_CREATE
(
    @Name NVARCHAR(100),
    @Description NVARCHAR(500),
    @Price DECIMAL(18,2)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Price <= 0
    BEGIN
        THROW 50001, 'El precio debe ser superior a cero.', 1;
    END;

    INSERT INTO DBO.PRODUCTS([NAME],[DESCRIPTION],PRICE)
    VALUES(@Name,@Description,@Price);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ProductId;
END;
GO

-- Creacion sp listado de todos los productos de productos

CREATE OR ALTER PROCEDURE DBO.SP_PRODUCT_GETALL
AS
BEGIN

    SET NOCOUNT ON;

    SELECT ID,[NAME],[DESCRIPTION], [PRICE], [DATE_CREATION]
    FROM DBO.PRODUCTS
    ORDER BY ID DESC;
END;
GO

-- Creacion sp listado por id producto

CREATE OR ALTER PROCEDURE DBO.SP_PRODUCT_GETBYID
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID,[NAME],[DESCRIPTION],[PRICE],[DATE_CREATION]
    FROM DBO.PRODUCTS
    WHERE ID = @ID;
END;
GO

-- Creacion sp actualizacion producto


CREATE OR ALTER PROCEDURE DBO.SP_PRODUCT_UPDATE
(
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(500),
    @Price DECIMAL(18,2)
)
AS
BEGIN

    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM DBO.PRODUCTS
        WHERE Id = @Id
    )
    BEGIN
        THROW 50002, 'No se encontró el producto.', 1;
    END;

    IF @Price <= 0
    BEGIN
        THROW 50003, 'El precio debe ser superior a cero.', 1;
    END;

    UPDATE DBO.PRODUCTS
    SET
        [NAME] = @Name,
        [DESCRIPTION] = @Description,
        Price = @Price
    WHERE Id = @Id;
END;
GO

-- Creacion sp eliminacion producto

CREATE OR ALTER PROCEDURE DBO.SP_PRODUCT_DELETE
(
    @Id INT
)
AS
BEGIN

    SET NOCOUNT ON;

    IF NOT EXISTS
    (
        SELECT 1
        FROM DBO.PRODUCTS
        WHERE Id = @Id
    )
    BEGIN
        THROW 50004, 'No se encontró el producto.', 1;
    END;

    DELETE FROM DBO.PRODUCTS
    WHERE Id = @Id;

END;
GO

EXEC dbo.sp_Product_Create
    @Name = 'Laptop',
    @Description = 'Dell Latitude',
    @Price = 3500;



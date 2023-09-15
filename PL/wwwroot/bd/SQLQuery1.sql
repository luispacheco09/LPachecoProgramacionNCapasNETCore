CREATE DATABASE LPachecoProgramacionNCapasNETCore

DROP DATABASE LPachecoProgramacionNCapasNETCore

CREATE TABLE Area(
IdArea INT PRIMARY KEY  IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL
)

CREATE TABLE Usuario(
IdUsuario INT PRIMARY KEY  IDENTITY(1,1),
CURP VARCHAR(50) NOT NULL UNIQUE,
Nombre VARCHAR(50) NOT NULL,
ApellidoPaterno VARCHAR(50) NOT NULL,
ApellidoMaterno VARCHAR(50) NULL,
EmailEmpresarial VARCHAR(254) NOT NULL UNIQUE,
Sexo CHAR(2) NOT NULL,
Telefono VARCHAR(20) NOT NULL,
Celular VARCHAR(20) NULL,
FechaNacimiento DATE NULL,
FechaRegistro DATETIME,
FotoRegistro VARBINARY (MAX),
IdArea INT REFERENCES Area(IdArea),
IdUsuarioModificacion INT REFERENCES Usuario(IdUsuario)
)

CREATE TABLE UsuarioFecha(
IdUsuarioFecha INT PRIMARY KEY IDENTITY(1,1),
IdUsuario INT REFERENCES Usuario(IdUsuario),
FechaIngreso DATETIME,
FechaSalida DATETIME,
Observaciones VARCHAR(500)
)

CREATE TABLE Departamento(
IdDepartamento INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100)
)

CREATE TABLE Marca(
IdMarca INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100)
)

CREATE TABLE Proveedor(
IdProveedor INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100),
Direccion VARCHAR(200),
Telefono VARCHAR(20),
Celular VARCHAR(20),
PaginaWeb VARCHAR(250)
)

CREATE TABLE Producto(
IdProducto INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(200) NOT NULL,
Descripcion VARCHAR(500),
IdDepartamento INT REFERENCES Departamento(IdDepartamento),
FechaIngreso DATE,
CodigoBarras VARCHAR(50),
Imagen VARBINARY(MAX),
Modelo VARCHAR(100), 
IdMarca INT REFERENCES Marca(IdMarca),
IdProveedor INT REFERENCES Proveedor(IdProveedor),
IdUsuarioModificacion INT REFERENCES Usuario(IdUsuario)
)

CREATE TABLE UsuarioProducto(
IdUsuarioProducto INT PRIMARY KEY IDENTITY(1,1),
IdUsuario INT REFERENCES Usuario(IdUsuario),
IdProductoInventario INT REFERENCES Producto(IdProducto),
FechaAsignacion DATE,
FechaEntrega DATE,
IdUsuarioModificacion INT REFERENCES Usuario(IdUsuario)
)

CREATE TABLE ProductoInventario(
IdProductoInventario INT PRIMARY KEY IDENTITY(1,1),
IdProducto INT REFERENCES Producto(IdProducto),
Cantidad INT,
Fecha DATE
)


/*
Scaffold-DbContext "Server=.; Database=LPachecoProgramacionNCapasNETCore; TrustServerCertificate=True; Trusted_Connection=True; User ID=sa; Password=pass@word1;" Microsoft.EntityFrameworkCore.SqlServer -f

*/

/*Sucursal*/
CREATE TABLE Sucursal(
IdSucursal INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50),
Calle  VARCHAR(50),
NumeroInterior VARCHAR(50),
NumeroExterior VARCHAR(50),
CP VARCHAR(50),
Colonia VARCHAR(50),
Municipio VARCHAR(50),
Estado VARCHAR(50),
PaginaWeb VARCHAR(100)
)

CREATE TABLE SucursalProducto(
IdSucursalProducto INT PRIMARY KEY IDENTITY(1,1),
IdProducto INT REFERENCES Producto(IdProducto),
IdSucursal INT REFERENCES Sucursal(IdSucursal),
Stock INT
)

ALTER TABLE Departamento
ADD IdArea INT REFERENCES Area(IdArea)

ALTER TABLE Producto
ADD PrecioUnitario DECIMAL(18,2)


CREATE TABLE MetodoPago(
IdMetodoPago INT PRIMARY KEY IDENTITY(1,1),
Metodo VARCHAR(50) NOT NULL
)

CREATE TABLE Venta(
IdVenta  INT PRIMARY KEY IDENTITY(1,1),
--IdUsuario INT REFERENCES Usuario(IdUsuario),
IdUser NVARCHAR(450) REFERENCES AspNetUsers(Id),
Total DECIMAL (18,2) NOT NULL,
IdMetodoPago INT REFERENCES MetodoPago(IdMetodoPago),
Fecha DATETIME
)

CREATE TABLE VentaProducto(
IdVentaProducto INT PRIMARY KEY IDENTITY(1,1),
IdVenta INT REFERENCES Venta(IdVenta),
IdSucursalProducto INT REFERENCES SucursalProducto(IdSucursalProducto),
Cantidad INT NOT NULL
)

--------PRUEBAS--------
	select Producto.Nombre, Sucursal.Nombre
	from SucursalProducto
	inner join Sucursal
	on SucursalProducto.IdSucursal = Sucursal.IdSucursal
	inner join Producto
	on SucursalProducto.IdProducto = Producto.IdProducto
	where Sucursal.IdSucursal = 2 AND Producto.IdDepartamento =4


INSERT INTO [dbo].[Sucursal]
           ([Nombre]
           ,[Calle]
           ,[NumeroInterior]
           ,[NumeroExterior]
           ,[CP]
           ,[Colonia]
           ,[Municipio]
           ,[Estado]
           ,[PaginaWeb])
     VALUES
           ('Portales','El rosario',12,25,0147,'Tierra Nueva','Azcapotzalco','CDMX','www.rosario.com')
GO

-------------Trigger---------------------------
Alter TRIGGER SucursalProductoAdd
ON dbo.Sucursal
AFTER INSERT
AS
BEGIN
	Insert INTO SucursalProducto(Idproducto,IdSucursal, Stock)
	Select (IdProducto),(inserted.IdSucursal),(0)
	FROM Producto, inserted
END

DROP TRIGGER SucursalProductoAdd

SELECT *
FROM SucursalProductoPrueba


--Store procedure
 GO

 CREATE PROCEDURE ProductoDeleteCascade
  @IdProducto INT
 AS
	BEGIN
		DELETE SucursalProducto FROM Producto 
		INNER JOIN SucursalProducto
		ON Producto.IdProducto = SucursalProducto.IdProducto
		WHERE Producto.IdProducto = @IdProducto

	
		DELETE FROM Producto
		WHERE IdProducto = @IdProducto
	END
EXECUTE ProductoDeleteCascade 9

ALTER TABLE SucursalProducto  WITH CHECK ADD  CONSTRAINT FK_SucursalP_IdProducto FOREIGN KEY(IdProducto)
REFERENCES Producto (IdProducto)
ON DELETE CASCADE
GO
 
ALTER TABLE SucursalProducto CHECK CONSTRAINT FK_SucursalP_IdProducto
GO
--inserts--

--AREA
INSERT INTO [dbo].[Area]
           ([Nombre])
     VALUES
           ('Mascotas'),
           ('Abarrotes'),
		   ('Deportes')
GO}
--DEPARTAMENTO

INSERT INTO [dbo].[Departamento]
           ([Nombre]
           ,[IdArea])
     VALUES
           ('Perros'
           ,1),
		   ('Gatos'
           ,1),
		   ('Jugos y Bebidas'
           ,2),
		   ('Galletas, Cereales y Barras'
           ,2),
		   ('Botanas y Snacks'
           ,2),
		   ('Camping'
           ,3),
		   ('Ciclismo'
           ,3)
GO
--Marca
INSERT INTO [dbo].[Marca]
           ([Nombre])
     VALUES
	 
		   ('Nupec'),
           ('Dog Chow'),
           ('Cat Chow'),
           ('Minino'),

           ('Gamesa'),
           ('Sabritas'),
           ('Coca-Cola'),
           ('Nescafe'),
           ('Manzanita Sol'),
		   ('Jumex'),

		   --Bicis
           ('Specialized'),
           ('Scott'),
		   --Tiendas campar
           ('Coleman'),
           ('North Face')



GO


	
--Proveedor

INSERT INTO [dbo].[Proveedor]
           ([Nombre]
           ,[Direccion]
           ,[Telefono]
           ,[Celular]
           ,[PaginaWeb])
     VALUES
           ('Proveedor 1'
           ,'San Juan #320'
           ,5529350954
           ,5512096543
           ,'www.proveedor-1.com'),
		   ('Proveedor 2'
           ,'San Juan #320'
           ,5529350954
           ,5512096543
           ,'www.proveedor-2.com'),
		   ('Proveedor 3'
           ,'San Juan #320'
           ,5529350954
           ,5512096543
           ,'www.proveedor-3.com'),
		   ('Proveedor 4'
           ,'San Juan #320'
           ,5529350954
           ,5512096543
           ,'www.proveedor-4.com')

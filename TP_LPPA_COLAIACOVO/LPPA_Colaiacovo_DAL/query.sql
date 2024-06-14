CREATE DATABASE GColaiacovoLPPA
GO


CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Contrasena NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE,
    Rol NVARCHAR(100),
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreado DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificado DATETIME
);


INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, FechaNacimiento, Rol, Activo, FechaCreado, FechaModificado)
VALUES ('Juan', 'Perez', 'juan.perez@example.com', 'bf733ae1731f8fe8ed936df6d6b6859b3e3946c8ebcd872ecb61e57b0bc8b64f', '1985-06-15', 'ADMIN', 1, GETDATE(), NULL);

INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, FechaNacimiento, Rol, Activo, FechaCreado, FechaModificado)
VALUES ('Ana', 'Garcia', 'ana.garcia@example.com', 'd8b659df6fb202fb84feb7717eb64d9d0a401be3ab3b12da93bb64c5ea48b150', '1990-09-23', 'USER', 1, GETDATE(), NULL);

INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, FechaNacimiento, Rol, Activo, FechaCreado, FechaModificado)
VALUES ('Carlos', 'Rodriguez', 'carlos.rodriguez@example.com', 'de19c5a09d29c3c569d253248694bd386c3503e84f8457b6616b163d3a22e3df', '1982-12-05', 'USER', 1, GETDATE(), NULL);


CREATE TABLE Producto (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Marca NVARCHAR(100) NOT NULL,
    CategoriaId INT NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    UrlImagen NVARCHAR(255),
    Stock INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreado DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificado DATETIME
);

-- Insertando algunos datos de ejemplo en la tabla Producto
INSERT INTO Producto (Nombre, Descripcion, Marca, CategoriaId, Precio, UrlImagen, Stock, Activo, FechaCreado, FechaModificado)
VALUES ('Proteína Whey', 'Suplemento de proteina de alta calidad.', 'ENA', 1, 29.99, 'https://http2.mlstatic.com/D_NQ_NP_2X_957539-MLA50144895276_052022-F.webp', 100, 1, GETDATE(), NULL);

INSERT INTO Producto (Nombre, Descripcion, Marca, CategoriaId, Precio, UrlImagen, Stock, Activo, FechaCreado, FechaModificado)
VALUES ('Multivitaminas', 'Complejo multivitaminico para la salud diaria.', 'Centrum', 2, 19.99, 'https://http2.mlstatic.com/D_NQ_NP_2X_662380-MLU72641181663_112023-F.webp', 200, 1, GETDATE(), NULL);

INSERT INTO Producto (Nombre, Descripcion, Marca, CategoriaId, Precio, UrlImagen, Stock, Activo, FechaCreado, FechaModificado)
VALUES ('Creatina Monohidrato', 'Suplemento para mejorar el rendimiento fisico.', 'ENA', 3, 24.99, 'https://http2.mlstatic.com/D_NQ_NP_2X_882636-MLU75591903237_042024-F.webp', 150, 1, GETDATE(), NULL);


CREATE TABLE Bitacora (
    Id INT PRIMARY KEY IDENTITY,
    Descripcion NVARCHAR(MAX) NOT NULL,
    IdUsuario INT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreado DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificado DATETIME
);

CREATE TABLE Venta (
    Id INT PRIMARY KEY IDENTITY,
    IdUsuario INT NULL,
    MontoTotal DECIMAL(10, 2) NOT NULL,
    MetodoDePago INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreado DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificado DATETIME
);

CREATE TABLE VentaProducto (
    Id INT PRIMARY KEY IDENTITY,
    IdVenta INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    Monto DECIMAL(18, 2) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreado DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificado DATETIME,

    FOREIGN KEY (IdVenta) REFERENCES Ventas(Id),
    FOREIGN KEY (IdProducto) REFERENCES Productos(Id)
);
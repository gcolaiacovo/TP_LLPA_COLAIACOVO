CREATE DATABASE GColaiacovoLPPA
GO


USE GColaiacovoLPPA

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
    FechaModificado DATETIME,
    DigitoVerificador INT NOT NULL
);


INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, FechaNacimiento, Rol, Activo, FechaCreado, FechaModificado, DigitoVerificador)
VALUES ('Juan', 'Perez', 'juan.perez@example.com', 'bf733ae1731f8fe8ed936df6d6b6859b3e3946c8ebcd872ecb61e57b0bc8b64f', '1985-06-15', 'ADMIN', 1, GETDATE(), NULL, 107);

INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, FechaNacimiento, Rol, Activo, FechaCreado, FechaModificado, DigitoVerificador)
VALUES ('Ana', 'Garcia', 'ana.garcia@example.com', 'd8b659df6fb202fb84feb7717eb64d9d0a401be3ab3b12da93bb64c5ea48b150', '1990-09-23', 'USER', 1, GETDATE(), NULL, 70);

INSERT INTO Usuario (Nombre, Apellido, Email, Contrasena, FechaNacimiento, Rol, Activo, FechaCreado, FechaModificado, DigitoVerificador)
VALUES ('Carlos', 'Rodriguez', 'carlos.rodriguez@example.com', 'de19c5a09d29c3c569d253248694bd386c3503e84f8457b6616b163d3a22e3df', '1982-12-05', 'USER', 1, GETDATE(), NULL, 75);

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
    FechaModificado DATETIME,
    DigitoVerificador INT NOT NULL
);


-- Insertando algunos datos de ejemplo en la tabla Producto
INSERT INTO Producto (Nombre, Descripcion, Marca, CategoriaId, Precio, UrlImagen, Stock, Activo, FechaCreado, FechaModificado, DigitoVerificador)
VALUES 
('Proteína Whey', 'Suplemento de proteina de alta calidad.', 'ENA', 1, 29.99, 'https://http2.mlstatic.com/D_NQ_NP_2X_957539-MLA50144895276_052022-F.webp', 100, 1, GETDATE(), NULL, 138),

('Multivitaminas', 'Complejo multivitaminico para la salud diaria.', 'Centrum', 1, 19.99, 'https://http2.mlstatic.com/D_NQ_NP_2X_662380-MLU72641181663_112023-F.webp', 200, 1, GETDATE(), NULL, 119),

('Creatina Monohidrato', 'Suplemento para mejorar el rendimiento fisico.', 'ENA', 1, 24.99, 'https://http2.mlstatic.com/D_NQ_NP_2X_882636-MLU75591903237_042024-F.webp', 150, 1, GETDATE(), NULL, 108),

('Creatina Monohidrato', 'Suplemento para mejorar el rendimiento fisico.', 'NF Nutrition', 1, 34.99, 'https://acdn.mitiendanube.com/stores/002/792/557/products/nf-nutrition-creatina-89ecae97f06da01ad217061322559037-1024-1024.webp', 150, 1, GETDATE(), NULL, 191),

('Creatina Monohidrato', 'Suplemento para mejorar el rendimiento fisico.', 'Star Nutrition', 1, 13.99, 'https://http2.mlstatic.com/D_NQ_NP_637950-MLU76340237109_052024-O.webp', 150, 1, GETDATE(), NULL, 188),

('Proteína Whey', 'Suplemento de proteina de alta calidad.', 'Star Nutrition', 1, 19.99, 'https://http2.mlstatic.com/D_NQ_NP_2X_859897-MLA75935662805_042024-F.webp', 100, 1, GETDATE(), NULL, 217),

('Entrenamiento personalizado', 'Entrenamiento personalizado, con rutina, ajustes semanales, recomendaciones de dieta', 'N/A', 2, 99.99, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQfCs810OShs8CZ7NFEJsk7UDwxNIOZ4kXTXQ&s', 100, 1, GETDATE(), NULL, 246),

('Entrenamiento grupal online', 'Entrenamiento grupal, diferentes rutinas ajustables, recomendaciones de dieta', 'N/A', 2, 49.99, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTicM10zzTNO8F1HUjIogDaw6Fqk6tnPt5pQQ&s', 100, 1, GETDATE(), NULL, 176),

('Recomendaciones nutricionales', 'Analisis de porcentaje de grasa corporal, dieta ajustable a objetivos del cliente', 'N/A', 2, 29.99, 'https://hips.hearstapps.com/hmg-prod/images/dieta-fodmap-pros-contras-elle-1660415938.jpg?crop=0.670xw:1.00xh;0.151xw,0&resize=1200:*', 100, 1, GETDATE(), NULL, 38);


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
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL,           
    IdProducto INT NOT NULL,        
    Cantidad INT NOT NULL,          
    Monto DECIMAL(18, 2) NOT NULL,   
    Activo BIT NOT NULL DEFAULT 1,   
    FechaCreado DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificado DATETIME NULL,   
    FOREIGN KEY (IdVenta) REFERENCES Venta(Id) ON DELETE CASCADE,
    FOREIGN KEY (IdProducto) REFERENCES Producto(Id) ON DELETE CASCADE
);

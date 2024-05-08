use ProyectoNFT

INSERT INTO TipoUsuario (Descripcion) VALUES ('admin');
INSERT INTO TipoUsuario (Descripcion) VALUES ('procesos');
INSERT INTO TipoUsuario (Descripcion) VALUES ('reportes');

INSERT INTO Usuario (NombreUsuario, Nombre, Apellido1, Apellido2, Estado, Contrasenna, IdTipoUsuario) 
VALUES ('admin', 'Miguel', 'Santamaria', 'Obando', 1, 'u3djZaMc+ipFDIgoAeS4iQ==', 1);
INSERT INTO Usuario (NombreUsuario, Nombre, Apellido1, Apellido2, Estado, Contrasenna, IdTipoUsuario) 
VALUES ('procesos', 'Kate', 'Campos', 'Vindas', 1, 'u3djZaMc+ipFDIgoAeS4iQ==', 2);
INSERT INTO Usuario (NombreUsuario, Nombre, Apellido1, Apellido2, Estado, Contrasenna, IdTipoUsuario) 
VALUES ('reportes', 'Kenneth', 'Santamaria', 'Castro', 1, 'u3djZaMc+ipFDIgoAeS4iQ==', 3);

INSERT INTO Pais (Id, Descripcion) VALUES ('188', 'Costa Rica');
INSERT INTO Pais (Id, Descripcion) VALUES ('156', 'China');
INSERT INTO Pais (Id, Descripcion) VALUES ('250', 'Francia');
INSERT INTO Pais (Id, Descripcion) VALUES ('276', 'Alemania');
INSERT INTO Pais (Id, Descripcion) VALUES ('392', 'Jap�n');
INSERT INTO Pais (Id, Descripcion) VALUES ('643', 'Rusia');
INSERT INTO Pais (Id, Descripcion) VALUES ('826', 'Reino Unido');
INSERT INTO Pais (Id, Descripcion) VALUES ('840', 'Estados Unidos de Am�rica');
INSERT INTO Pais (Id, Descripcion) VALUES ('356', 'India');
INSERT INTO Pais (Id, Descripcion) VALUES ('410', 'Corea del Sur');

INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Juan', 'Martinez', 'Gonz�lez', 'mdsantamaria02@gmail.com', 'M', '1985-03-15', 188, 1, '123456789');
INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Mar�a', 'L�pez', 'Hern�ndez', 'mdsantamaria02@gmail.com', 'F', '1990-08-25', 840, 1, '987654321');
INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Pedro', 'Garc�a', 'Rodr�guez', 'mdsantamaria02@gmail.com', 'M', '1988-11-03', 250, 1, '456789123');
INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Ana', 'Mart�nez', 'P�rez', 'mdsantamaria02@gmail.com', 'F', '1982-05-20', 156, 1, '789123456');
INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Luis', 'Rodr�guez', 'Fern�ndez', 'mdsantamaria02@gmail.com', 'M', '1976-09-12', 276, 1, '321654987');
INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Laura', 'Gonz�lez', 'S�nchez', 'mdsantamaria02@gmail.com', 'F', '1995-02-28', 392, 1, '654987321');
INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Carlos', 'Hern�ndez', 'L�pez', 'mdsantamaria02@gmail.com', 'M', '1989-07-10', 643, 1, '987321654');
INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Paula', 'D�az', 'Mart�nez', 'mdsantamaria02@gmail.com', 'F', '1993-12-05', 826, 1, '321987654');
INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Miguel', 'P�rez', 'G�mez', 'mdsantamaria02@gmail.com', 'M', '1980-04-18', 410, 1, '654321987');
INSERT INTO Cliente (Nombre, Apellido1, Apellido2, Correo, Sexo, FechaN, IdPais, Estado, Cedula) 
VALUES ('Sof�a', 'S�nchez', 'Guti�rrez', 'mdsantamaria02@gmail.com', 'F', '1987-06-30', 356, 1, '123789456');

INSERT INTO TipoTarjeta (Descrpcion, Estado) VALUES ('Mastercard', 1);
INSERT INTO TipoTarjeta (Descrpcion, Estado) VALUES ('Visa', 1);
INSERT INTO TipoTarjeta (Descrpcion, Estado) VALUES ('American Express', 1);
INSERT INTO TipoTarjeta (Descrpcion, Estado) VALUES ('Discover', 1);
INSERT INTO TipoTarjeta (Descrpcion, Estado) VALUES ('Diners Club', 1);
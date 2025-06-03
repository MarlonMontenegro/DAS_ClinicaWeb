-- Creacion de la base de datos

CREATE DATABASE ClinicaDB;

GO

USE clinicaDB;

GO


-- Creacion de Tablas

CREATE TABLE PACIENTES (

    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(75) NOT NULL,
    Apellido NVARCHAR(75) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    DUI NVARCHAR(20) NOT NULL UNIQUE,
    Direccion NVARCHAR(250),
    Telefono NVARCHAR(20),
    CorreoElectronico NVARCHAR(100) UNIQUE


);

GO

-- Tabla Especialidades
CREATE TABLE Especialidades (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NombreEspecialidad NVARCHAR(100) NOT NULL UNIQUE
);
GO


CREATE TABLE Medicos (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(75) NOT NULL,
    Apellido NVARCHAR(75) NOT NULL,
    ID_Especialidad INT NOT NULL,
    DUI NVARCHAR(20) NOT NULL UNIQUE,
    Direccion NVARCHAR(250),
    Telefono NVARCHAR(20),
    CorreoElectronico NVARCHAR(100) UNIQUE,
    CONSTRAINT FK_Medicos_Especialidades FOREIGN KEY (ID_Especialidad) REFERENCES Especialidades(ID)
);
GO

CREATE TABLE Usuarios (
    ID_Usuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(75) NOT NULL,
    Apellido NVARCHAR(75) NOT NULL,
    Nombre_Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(20) NOT NULL CHECK (Rol IN ('Administrador', 'Medico', 'Recepcionista')),
    CorreoElectronico NVARCHAR(100) UNIQUE
);


CREATE TABLE UsuarioMedico (
    ID_Usuario INT PRIMARY KEY,
    ID_Medico INT UNIQUE,
    FOREIGN KEY (ID_Usuario) REFERENCES Usuarios(ID_Usuario),
    FOREIGN KEY (ID_Medico) REFERENCES Medicos(ID)
);



-- Tabla HistorialMedico para antecedentes, alergias y padecimientos
CREATE TABLE HistorialMedico (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ID_Paciente INT NOT NULL,
    Tipo NVARCHAR(50) NOT NULL, -- Ejemplo: 'Antecedente', 'Alergia', 'Padecimiento'
    Descripcion NVARCHAR(MAX) NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_HistorialMedico_Pacientes FOREIGN KEY (ID_Paciente) REFERENCES Pacientes(ID)
);
GO


-- Tabla Citas
CREATE TABLE Citas (
    ID_Cita INT IDENTITY(1,1) PRIMARY KEY,
    ID_Paciente INT NOT NULL,
    ID_Medico INT NOT NULL,
    Fecha_Hora DATETIME NOT NULL,
    Motivo_Consulta NVARCHAR(250),
    Estado NVARCHAR(50),
    CONSTRAINT FK_Citas_Pacientes FOREIGN KEY (ID_Paciente) REFERENCES Pacientes(ID),
    CONSTRAINT FK_Citas_Medicos FOREIGN KEY (ID_Medico) REFERENCES Medicos(ID)
);
GO

-- Tabla Consultas
CREATE TABLE Consultas (
    ID_Consulta INT IDENTITY(1,1) PRIMARY KEY,
    ID_Cita INT NOT NULL,
    Diagnostico NVARCHAR(MAX),
    Tratamiento NVARCHAR(MAX),
    Notas NVARCHAR(MAX),
    Fecha_Consulta DATE NOT NULL,
    CONSTRAINT FK_Consultas_Citas FOREIGN KEY (ID_Cita) REFERENCES Citas(ID_Cita)
);



INSERT INTO Especialidades (NombreEspecialidad) VALUES 
('Medicina General'),
('Pediatría'),
('Cardiología');


INSERT INTO Medicos (Nombre, Apellido, ID_Especialidad, DUI, Direccion, Telefono, CorreoElectronico) 
VALUES ('Carlos', 'Ramírez', 1, '01234567-0', 'Colonia Médica, SS', '2222-3333', 'carlos.ramirez@clinica.com');


-- Administrador
INSERT INTO Usuarios (Nombre, Apellido, Nombre_Usuario, Contrasena, Rol, CorreoElectronico) 
VALUES ('Ana', 'Gómez', 'admin', '123', 'Administrador', 'ana.gomez@clinica.com');

-- Recepcionista
INSERT INTO Usuarios (Nombre, Apellido, Nombre_Usuario, Contrasena, Rol, CorreoElectronico) 
VALUES ('Lucía', 'Pérez', 'rece', '123', 'Recepcionista', 'lucia.perez@clinica.com');

-- Médico (debe coincidir con el médico insertado arriba)
INSERT INTO Usuarios (Nombre, Apellido, Nombre_Usuario, Contrasena, Rol, CorreoElectronico) 
VALUES ('Carlos', 'Ramírez', 'medico', '123', 'Medico', 'carlos.ramirez@clinica.com');



INSERT INTO UsuarioMedico (ID_Usuario, ID_Medico) VALUES (3, 1);


INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, DUI, Direccion, Telefono, CorreoElectronico) 
VALUES ('María', 'López', '1990-08-15', '12345678-9', 'Santa Tecla', '7890-4567', 'maria.lopez@correo.com');


INSERT INTO Citas (ID_Paciente, ID_Medico, Fecha_Hora, Motivo_Consulta, Estado) 
VALUES (1, 1, '2025-06-05 10:30:00', 'Dolor de cabeza persistente', 'Pendiente');


INSERT INTO Consultas (ID_Cita, Diagnostico, Tratamiento, Notas, Fecha_Consulta) 
VALUES (1, 'Migraña', 'Paracetamol 500mg', 'Paciente estable, seguimiento en 1 semana', '2025-06-05');

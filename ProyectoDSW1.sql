use master 
go

create database ProyectoDSW1
go

use ProyectoDSW1
go


--Crear tabla libros
create table tb_libros(
	codigo int primary key identity(1,1),
	nombre varchar(50),
	autor varchar(50),
	idGenero int,
	stock int,
	precio decimal,
	fechaRegistro datetime,
	estado int
)
go

--Crear tabla libros_baja; este sirve para llevar registro de las eliminaciones
create table tb_libros_baja(
	id int primary key identity(1,1),
	codigoLibro int,
	fechaRegistro datetime
)
go
select * from tb_libros_baja

--Crear tabla genero
create table tb_genero(
	idGenero int primary key,
	nGenero varchar(50)
)
go

--Llave foranea de tabla libros con tabla genero
alter table tb_libros add constraint fk1 foreign key (idGenero) references tb_genero(idGenero)
go

--Crear trigger para que registre en tabla librosBaja cuando un libro cambie su estado a 0
create trigger libroBajaTrigger
on tb_libros
after update
as
	if update(estado)
	begin
		insert into tb_libros_baja(codigoLibro, fechaRegistro)
		select codigo, GETDATE() 
		from tb_libros
		where estado = 0
end

create or alter procedure usp_generos_select
as
select * from tb_genero
go

create or alter procedure usp_libros_crud
	@indicador varchar(20),
	@codigo int,
	@nombre varchar(50) = '',
	@autor varchar(50) = '',
	@genero int = 0,
	@stock int = 0,
	@precio decimal = 0
as
begin
	set nocount on

	if @indicador = 'getXID'
	begin
		select codigo, nombre, autor, idGenero, stock, precio from tb_libros
		where codigo = @codigo AND estado = 1
	end

	if @indicador = 'listarTodos'
	begin
		select codigo, nombre, autor, idGenero, stock, precio from tb_libros
		where estado = 1
	end

	if @indicador = 'listarPorNombre'
	begin 
		select codigo, nombre, autor, idGenero, stock, precio from tb_libros		
		where nombre LIKE @nombre AND estado = 1
	end

	if @indicador = 'registrar'
	begin 
		insert into tb_libros(nombre, autor, idGenero, stock, precio, fechaRegistro, estado)
		values (@nombre, @autor, @genero, @stock, @precio, GETDATE(), 1)
		SELECT 1
	end

	if @indicador = 'actualizar'
	begin
		update tb_libros set nombre = @nombre, autor = @autor, idGenero = @genero, stock = @stock, precio = @precio
		where codigo = @codigo
		SELECT 1
	end

	if @indicador = 'eliminar'
	begin
		update tb_libros set estado = 0 where codigo = @codigo
		SELECT 1
	end
end
go

INSERT INTO tb_genero VALUES (1, 'Ciencia ficcion')
INSERT INTO tb_genero VALUES (2, 'Fantasia');
INSERT INTO tb_genero VALUES (3, 'Misterio');
INSERT INTO tb_genero VALUES (4, 'Romance');
INSERT INTO tb_genero VALUES (5, 'Aventura');
INSERT INTO tb_genero VALUES (6, 'Terror');
INSERT INTO tb_genero VALUES (7, 'Humor');
INSERT INTO tb_genero VALUES (8, 'Historico');
INSERT INTO tb_genero VALUES (9, 'Realismo magico');
INSERT INTO tb_genero VALUES (10, 'Distopia');
INSERT INTO tb_genero VALUES (11, 'Novela grafica');
INSERT INTO tb_genero VALUES (12, 'Autobiografia');
INSERT INTO tb_genero VALUES (13, 'Biografia');
INSERT INTO tb_genero VALUES (14, 'Cronica');
INSERT INTO tb_genero VALUES (15, 'Ensayo');
INSERT INTO tb_genero VALUES (16, 'Fabula');
INSERT INTO tb_genero VALUES (17, 'Leyendas');
INSERT INTO tb_genero VALUES (18, 'Mitologia');
INSERT INTO tb_genero VALUES (19, 'Epistolar');
INSERT INTO tb_genero VALUES (20, 'Policial');
INSERT INTO tb_genero VALUES (21, 'Suspense');
INSERT INTO tb_genero VALUES (22, 'Western');
INSERT INTO tb_genero VALUES (23, 'Ficcion');

exec usp_libros_crud 'registrar', '', 'Star Wars Thrawn', 'Timothy Zahn', 1, 40, 45.00
exec usp_libros_crud 'registrar', '', 'Cien años de soledad', 'Gabriel García Márquez', 23, 35, 48.00
exec usp_libros_crud 'registrar', '', 'El codigo Da Vinci', 'Dan Brown', 3, 53, 39.00
exec usp_libros_crud 'registrar', '', 'Orgullo y prejuicio', 'Jane Austen', 4, 76, 42.00
exec usp_libros_crud 'registrar', '', 'Harry Potter y la piedra filosofal', 'J.K. Rowling', 2, 71, 60.00
exec usp_libros_crud 'registrar', '', 'El alquimista', 'Paulo Coelho', 5, 27, 48.00
exec usp_libros_crud 'registrar', '', 'Dracula', 'Bram Stoker', 6, 34, 55.00
exec usp_libros_crud 'registrar', '', 'El señor de los anillos: La comunidad del anillo', 'J.R.R. Tolkien', 23, 40, 45.00
exec usp_libros_crud 'registrar', '', 'Los juegos del hambre', 'Suzanne Collins', 1, 46, 56.00

select * from tb_libros
go
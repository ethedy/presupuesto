
use datapresupuestos;

select * from analisis;

select * from precio;

select * from obrasocial;

alter table precio
add constraint FK_Precio_Analisis
foreign key (IdAnalisis) references Analisis (ID);


select * from PreciosCuidados;
create view PreciosCuidados as
select 
	AN.CodAna, AN.Nombre as Analisis, PR.Precio, OS.Nombre as ObraSocial
from Analisis as AN 
  inner join precio as PR on AN.Id = PR.IdAnalisis
  inner join obrasocial as OS on PR.IdObraSocial = OS.ID
where PR.Precio < 10;

insert into datapresupuestos.obrasocial values( default,'Obra Social de tu Hermana', 0);
  
show databases;

show engines;

show engine InnoDB status;

show create database datapresupuestos;

show databases;

show master status;

show global status where variable_name like '%version%';

select version();

show variables where 
	variable_name like '%version%' or 
    variable_name like '%server%' or
	variable_name like '%name%';

show variables where variable_name like '%server%';

show variables where variable_name like '%name%';

select 
  AN.nombre as Analisis, 
  PR.precio, 
  OS.nombre as ObraSocial
from analisis as AN 
  inner join precio as PR on AN.id = PR.idanalisis
  inner join obrasocial as OS on PR.idObraSocial=OS.id
where 
  OS.id = 2
  and AN.Activo and OS.Activo;
    
  
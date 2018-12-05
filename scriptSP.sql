DELIMITER //
 CREATE PROCEDURE InsertarTipoUsuario (tipo char(50))
   BEGIN
		INSERT INTO tipousuario (NombreTipo) values (tipo);
   END //
 DELIMITER ;
 
 call InsertarTipoUsuario("administrador");
 call InsertarTipoUsuario("profesor");
 call InsertarTipoUsuario("estudiante");
 select * from tipousuario;
 
 
 DELIMITER //
 CREATE PROCEDURE RegistrarUsuario (tipoUsuario tinyint, nombre varchar(45), apellido1 varchar(45), 
									apellido2 varchar(45), email nvarchar(80),carne int, pass blob)
   BEGIN
		INSERT INTO usuario (fkTipoUsuario,Nombre,Apellido1,Apellido2,Email,Carne,Contrasena) 
        values (tipoUsuario,nombre,apellido1,apellido2,email,carne,MD5(pass));
   END //
 DELIMITER ;
 
 call RegistrarUsuario(2,'Pepito','Rojas','Rojas','pepito@gmail.com',null,'prueba');
 select * from Usuario;
 
 
 
  DELIMITER //
 CREATE PROCEDURE UsuarioValido (correo nvarchar(80),pass blob)
   BEGIN
		select ifnull((select fkTipoUsuario from usuario where Email = correo and Contrasena = MD5(pass)) ,0);
   END //
 DELIMITER ;
 
 call UsuarioValido('juliocg1695@gmail.com','prueba');
 
 
   DELIMITER //
 CREATE PROCEDURE Login (correo nvarchar(80),pass blob)
   BEGIN
		select idUsuario from usuario where Email = correo and Contrasena = MD5(pass);
   END //
 DELIMITER ;
 
 call Login('juliocg1695@gmail.com','prueba');
 

 DELIMITER //
CREATE PROCEDURE registrarActividad (nombreActividad varchar(45), FKProyect int,FKUniMedida tinyint
				, cantidadAct int, DateInicio date, DateFin date)
   BEGIN
		Insert into actividades (Nombre,fkProyecto,fkUnidadMedida,Cantidad,FechaInicio,FechaFin) values 
        (nombreActividad,FKProyect,FKUniMedida,cantidadAct,DateInicio,DateFin);
   END //
DELIMITER ;

select * from actividades;

DELIMITER //
CREATE PROCEDURE registrarDesgloseActividad (FKActivity int, FKResources mediumint)
   BEGIN
		Insert into desgloceactividad (fkActividad,fkRecursos,CostoUnidad,Cantidad) values 
        (FKActivity,FKResources,(Select Costo from recursos where idRecursos=FKResources),1);
   END //
DELIMITER ;

select * from desgloceactividad;



DELIMITER //
CREATE PROCEDURE insertarUnidadMedida (nombreUnidad char(15))
   BEGIN
		Insert into unidadmedida (Nombre) values (nombreUnidad);
   END //
DELIMITER ;

select idUnidadMedida,Nombre from unidadmedida;

DELIMITER //
CREATE PROCEDURE actualizarUnidadMedida (nombreUnidad char(15), idUnidad tinyint)
   BEGIN
		Update unidadmedida set Nombre = nombreUnidad where idUnidadMedida = idUnidad;
   END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE deleteUnidadMedida (idUnidad tinyint)
   BEGIN
		delete from unidadmedida where idUnidadMedida = idUnidad;
   END //
DELIMITER ;



DELIMITER //
CREATE PROCEDURE verUnidadesMedida ()
   BEGIN
		select idUnidadMedida as 'ID',Nombre from unidadmedida;
   END //
DELIMITER ;

call verUnidadesMedida();

DELIMITER //
CREATE PROCEDURE insertarFamilia (nombreFamilia varchar(100))
   BEGIN
		Insert into familia (Nombre) values (nombreFamilia);
   END //
DELIMITER ;

call insertarFamilia ("Agregados");
call insertarFamilia ("Perfiles");
select idFamilia,Nombre from familia;


DELIMITER //
CREATE PROCEDURE insertarTipoRecurso (nombreTipoRecurso varchar(45))
   BEGIN
		Insert into tiporecurso (Nombre) values (nombreTipoRecurso);
   END //
DELIMITER ;

call insertarTipoRecurso("Materiales");
call insertarTipoRecurso("Equipo");
call insertarTipoRecurso("Mano de obra");

select * from tiporecurso;

DELIMITER //
CREATE PROCEDURE verTipoRecurso ()
   BEGIN
		Select idTipoRecurso as 'ID',Nombre from tiporecurso;
   END //
DELIMITER ;

call verTipoRecurso();


DELIMITER //
CREATE PROCEDURE insertarRecurso (nombreRecurso varchar(100),costoRecurso float,FKUnidadMedida tinyint, FKTipoRecurso tinyint, FKFamilia tinyint)
   BEGIN
		Insert into recursos (Nombre,Costo,fkUnidadMedida,fkTipoRecurso,fkFamilia)  
        values (nombreRecurso,costoRecurso,FKUnidadMedida,FKTipoRecurso,FKFamilia);
   END //
DELIMITER ;

select * from recursos;

DELIMITER //
CREATE PROCEDURE actualizarRecurso (id mediumint,nombreRecurso varchar(100),costoRecurso float,FKUnidadMedida tinyint, FKTipoRecurso tinyint, FKFamilia tinyint)
   BEGIN
		update recursos set Nombre = nombreRecurso, Costo = costoRecurso, fkUnidadMedida = FKUnidadMedida,
        fkTipoRecurso = FKTipoRecurso, fkFamilia = FKFamilia where idRecursos = id;
   END //
DELIMITER ;


DELIMITER //
CREATE PROCEDURE verMateriales ()
   BEGIN
		select r.idRecursos as 'ID', r.Nombre, r.Costo as 'Precio Unitario', um.Nombre as 'Unidad de Medida', f.Nombre as 'Familia' 
        from recursos r inner join unidadmedida um on r.fkUnidadMedida=um.idUnidadMedida inner join Familia f 
        on r.fkFamilia = f.idFamilia where r.fkTipoRecurso = 1;
   END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE deleteRecurso (idRecurso tinyint)
   BEGIN
		delete from recursos where idRecursos = idRecurso;
   END //
DELIMITER ;

call verMateriales ();

DELIMITER //
CREATE PROCEDURE verEquipo ()
   BEGIN
		select r.idRecursos as 'ID', r.Nombre, r.Costo as 'Precio Unitario', um.Nombre as 'Unidad de Medida' from recursos r 
        inner join unidadmedida um on r.fkUnidadMedida=um.idUnidadMedida  where r.fkTipoRecurso = 2;
   END //
DELIMITER ;

call verEquipo ();

DELIMITER //
CREATE PROCEDURE verManoObra ()
   BEGIN
		select r.idRecursos as 'ID',r.Nombre, r.Costo as 'Precio Unitario', um.Nombre as 'Unidad de Medida' from recursos r 
        inner join unidadmedida um on r.fkUnidadMedida=um.idUnidadMedida  where r.fkTipoRecurso = 3;
   END //
DELIMITER ;

call verManoObra ();

DELIMITER //
CREATE PROCEDURE insertarCurso (FKProfesorCargo int,nombreCurso varchar(60),semestreCurso tinyint(2),Anho mediumint, NumGrupo tinyint)
   BEGIN
		Insert into curso (fkProfesor,Nombre,Semestre,Año,NumeroGrupo) values 
        (FKProfesorCargo,nombreCurso,semestreCurso,Anho,NumGrupo);
   END //
DELIMITER ;

select * From curso;

DELIMITER //
CREATE PROCEDURE verCursosCreados (FKProfesorCargo int)
   BEGIN
		Select idCurso as 'ID',Nombre,Semestre,Año,NumeroGrupo as 'Número de Grupo' from curso where 
        fkProfesor = FKProfesorCargo;
   END //
DELIMITER ;

select idCurso,CONCAT(Nombre, ' Sem: ', Semestre, ' ', Año, ' Gr: ',NumeroGrupo ) as 'Descripción' from curso where fkProfesor = 4;

call verCursosCreados(4);

DELIMITER //
CREATE PROCEDURE actualizarCurso (id int,nombreCurso varchar(60),semestreCurso tinyint(2),Anho mediumint, NumGrupo tinyint)
   BEGIN
		Update curso set Nombre=nombreCurso, semestre=semestreCurso, Año=Anho, NumeroGrupo=NumGrupo 
        where idCurso = id;
   END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE deleteCurso (id int)
   BEGIN
		delete from curso where idCurso = id;
   END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE buscarEstudiante (identificacion int)
   BEGIN
		select idUsuario,CONCAT(Nombre, ' ', Apellido1, ' ', Apellido2) as 'Nombre' from usuario where Carne=identificacion;
   END //
DELIMITER ;

call buscarEstudiante(2014046522);

DELIMITER //
CREATE PROCEDURE agregarEstudianteCurso (FKCurso int,FKEstudiante int)
   BEGIN
		Insert into cursoxestudiante(fkCurso,fkEstudiante) values (FKCurso,FKEstudiante);
   END //
DELIMITER ;

select * from cursoxestudiante;

DELIMITER //
CREATE PROCEDURE estudiantesxCurso (idprofe int)
   BEGIN
		select ce.idCursoXEstudiante as 'ID',CONCAT(u.Nombre, ' ', u.Apellido1, ' ', u.Apellido2) as 'Nombre',
        u.Carne, CONCAT(c.Nombre, ' Sem: ', c.Semestre, ' ', c.Año, ' Gr: ',c.NumeroGrupo ) as 'Curso' 
        from usuario u inner join cursoxestudiante ce on ce.fkEstudiante = u.idUsuario inner join curso c 
        on c.idCurso = ce.fkCurso  where c.fkProfesor = idprofe order by c.idCurso;
   END //
DELIMITER ;

call estudiantesxCurso(5);

DELIMITER //
CREATE PROCEDURE deleteEstudiantesxCurso (idestudiantexcurso int)
   BEGIN
		delete from cursoxestudiante where idCursoXEstudiante =idestudiantexcurso;
   END //
DELIMITER ;

select * from cursoxestudiante;

select * from proyecto;

DELIMITER //
CREATE PROCEDURE verCursosSuscritos (idUsuarioEncargado int)
   BEGIN
		Select c.idCurso as 'ID', CONCAT(c.Nombre, ' Sem: ', c.Semestre, ' ', c.Año, ' Gr: ',c.NumeroGrupo ) as 'Curso'
        from curso c inner join cursoxestudiante ce on c.idCurso=ce.fkCurso inner join usuario u on u.idUsuario =
        ce.fkEstudiante where u.idUsuario = idUsuarioEncargado ;
   END //
DELIMITER ;

call verCursosSuscritos(3);


DELIMITER //
CREATE PROCEDURE mostrarProyectosUsuario (idUsuario int)
   BEGIN
		Select p.idProyecto as 'ID',p.Nombre,p.Ubicacion as 'Ubicación',p.Propietario, 
        CONCAT(u.Nombre, ' ', u.Apellido1, ' ', u.Apellido2) as 'Creado por' from proyecto p 
        left join curso  c on p.fkCursoAsociado = c.idCurso inner join usuario u on u.idUsuario = 
        p.fkUsuarioEncargado where p.fkUsuarioEncargado = idUsuario or c.fkProfesor = idUsuario;
   END // 
DELIMITER ; 

select * from proyecto where fkUsuarioEncargado=1;

call mostrarProyectosUsuario(1);
call mostrarProyectosUsuario(4);

DELIMITER //
CREATE PROCEDURE verProyectoEspecifico (IDProject int)
   BEGIN
		Select idProyecto as 'ID',Nombre,Ubicacion,Propietario,FechaInicio,FechaFin,Descripcion,datediff(FechaFin,FechaInicio) as 
        'Duración' from proyecto where idProyecto = IDProject;
   END //
DELIMITER ;

call verProyectoEspecifico(2);

select * from proyecto;

DELIMITER //
CREATE PROCEDURE updateProyecto (IDProject int, nombreProy varchar(45), ubicacionProy varchar(500),propietarioProy
varchar(120),descripcionProy varchar(600),FechaInicioProy date, FechaFinProy date)
   BEGIN
		Update proyecto set Nombre=nombreProy, Ubicacion=ubicacionProy,Propietario=propietarioProy,
        Descripcion=descripcionProy, FechaInicio=FechaInicioProy,FechaFin=FechaFinProy
        where idProyecto = IDProject;
   END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE verActividadesProyecto (IDProyecto int)
   BEGIN
		Select a.idActividades as 'ID',a.Nombre,u.Nombre as 'Unidad',a.Cantidad, a.FechaInicio as 'Fecha Inicio',
        a.FechaFin as 'Fecha Fin', round(ifnull((select sum(d.CostoUnidad*d.Cantidad) from desgloceactividad d inner join 
        recursos r on d.fkRecursos = r.idRecursos inner join tiporecurso tp on r.fkTipoRecurso = tp.idTipoRecurso
        where d.fkActividad = a.idActividades and tp.idTipoRecurso=1),0),2) as 'Costo Materiales', round(ifnull((select 
        sum(d.CostoUnidad*d.Cantidad) from desgloceactividad d inner join recursos r on d.fkRecursos = 
        r.idRecursos inner join tiporecurso tp on r.fkTipoRecurso = tp.idTipoRecurso
        where d.fkActividad = a.idActividades and tp.idTipoRecurso=2),0),2) as 'Costo Equipo', round(ifnull((select 
        sum(d.CostoUnidad*d.Cantidad) from desgloceactividad d inner join 
        recursos r on d.fkRecursos = r.idRecursos inner join tiporecurso tp on r.fkTipoRecurso =
        tp.idTipoRecurso where d.fkActividad = a.idActividades and tp.idTipoRecurso=3),0),2) as 'Costo Mano de Obra' ,
        round(ifnull((select sum(d.CostoUnidad*d.Cantidad) from desgloceactividad d where 
        d.fkActividad = a.idActividades),0),2) as 'Costo Unitario' , round(ifnull(a.Cantidad * (select sum(d.CostoUnidad*
        d.Cantidad) from desgloceactividad d where d.fkActividad = a.idActividades),0),2) as 'Costo Total'
        from actividades a inner join unidadmedida u on u.idUnidadMedida = a.fkUnidadMedida where 
        a.fkProyecto = IDProyecto;
   END //
DELIMITER ;
select Nombre from proyecto where idProyecto = 2;
call verActividadesProyecto (2);

DELIMITER //
CREATE PROCEDURE verActividadesGantt (IDProyecto int)
   BEGIN
		Select a.idActividades as 'ID',a.Nombre, a.FechaInicio as 'Fecha Inicio',
        a.FechaFin as 'Fecha Fin' from actividades a where 
        a.fkProyecto = IDProyecto;
   END //
DELIMITER ;

select * from tiporecurso;
call verActividadesGantt(6);
call verActividadesProyecto(6);
select * from actividades;

DELIMITER //
CREATE PROCEDURE verFechasActividades (IDProyecto int)
   BEGIN
		Select  date_sub(min(a.FechaInicio),INTERVAL 10 DAY) as 'Fecha Inicio',
        date_add(max(a.FechaFin),INTERVAL 10 DAY) as 'Fecha Fin' from actividades a
        where a.fkProyecto = IDProyecto;
   END //
DELIMITER ;

call verFechasActividades(6);

DELIMITER //
CREATE PROCEDURE mostrarDesgloce (fkActivity int)
	BEGIN
		select d.idDesgloceActividad as 'ID',r.Nombre as 'Recurso',um.Nombre as 'Unidad',d.Cantidad,
        d.CostoUnidad as 'Costo Unitario', round(d.Cantidad * d.CostoUnidad,2) as 'Costo Total' from 
        desgloceactividad d inner join recursos r on d.fkRecursos=r.idRecursos inner join unidadmedida 
        um on r.fkUnidadMedida = um.idUnidadMedida inner join tiporecurso tr on r.fkTipoRecurso = 
        tr.idTipoRecurso where d.fkActividad 
= fkActivity order by tr.idTipoRecurso;
    END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE mostrarDesglocePorTipo (fkActivity int,tipo int)
	BEGIN
		select d.idDesgloceActividad as 'ID',r.Nombre as 'Recurso',um.Nombre as 'Unidad',d.Cantidad,
        d.CostoUnidad as 'Costo Unitario', round(d.Cantidad * d.CostoUnidad,2) as 'Costo Total', tr.Nombre
        as 'Tipo' from desgloceactividad d inner join recursos r on d.fkRecursos=r.idRecursos inner join 
        unidadmedida um on r.fkUnidadMedida = um.idUnidadMedida inner join tiporecurso tr on r.fkTipoRecurso =
        tr.idTipoRecurso where d.fkActividad = fkActivity and tr.idTipoRecurso=tipo;
    END //
DELIMITER ;

select * from tiporecurso;

call mostrarDesgloce(2);
call mostrarDesglocePorTipo(2,1);
select d.idDesgloceActividad as 'ID',r.Nombre as 'Recurso',um.Nombre as 'Unidad',d.Cantidad,
        d.CostoUnidad as 'Costo Unitario', round(d.Cantidad * d.CostoUnidad,1) as 'Costo Total' from 
        desgloceactividad d inner join recursos r on d.fkRecursos=r.idRecursos inner join unidadmedida um 
        on r.fkUnidadMedida = um.idUnidadMedida where d.fkActividad = 2 order by d.fkRecursos;
call mostrarDesgloce (2);

DELIMITER //
CREATE PROCEDURE verRecursos (tipoResource tinyint)
   BEGIN
		Select r.idRecursos,r.Nombre, r.Costo, um.Nombre from recursos r inner join unidadmedida um on 
        r.fkUnidadMedida = um.idUnidadMedida where r.fkTipoRecurso = tipoResource;
   END //
DELIMITER ;


DELIMITER //
CREATE PROCEDURE registrarProyecto(UsuarioProyecto int,nombreProyecto varchar(45),ubicacionProyecto varchar(500)
				, propietarioProyecto varchar(120), descripcionProyecto varchar(600), DateInicio date, DateFin date, curso int)
   BEGIN
		if curso = 0 then
        
			Insert into proyecto (fkUsuarioEncargado,Nombre,Ubicacion,Propietario,Descripcion,FechaInicio,FechaFin,fkCursoAsociado)
			values (UsuarioProyecto,nombreProyecto,ubicacionProyecto,propietarioProyecto,descripcionProyecto,DateInicio,DateFin,null);
		else
			Insert into proyecto (fkUsuarioEncargado,Nombre,Ubicacion,Propietario,Descripcion,FechaInicio,FechaFin,fkCursoAsociado)
			values (UsuarioProyecto,nombreProyecto,ubicacionProyecto,propietarioProyecto,descripcionProyecto,DateInicio,DateFin,curso);
			
		end if;
    
   END //
DELIMITER ;

select *  from proyecto;
call registrarProyecto(1,'a','b','c','d','2017-08-01','2017-08-11',0);


DELIMITER //
CREATE PROCEDURE deleteProyecto (idProject int)
	BEGIN
		delete from proyecto where idProyecto = idProject;
    END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE mostrarRecursos (tipoResource tinyint)
	BEGIN
		if tipoResource = 0 then
			Select r.idRecursos as 'ID',r.Nombre, r.Costo, um.Nombre as 'Unidad' from recursos r inner join
            unidadmedida um on r.fkUnidadMedida = um.idUnidadMedida  order by r.fkTipoRecurso;
		elseif tipoResource = 1 then
			Select r.idRecursos as 'ID',r.Nombre, r.Costo, um.Nombre as 'Unidad' from recursos r inner join 
            unidadmedida um on r.fkUnidadMedida = um.idUnidadMedida where r.fkTipoRecurso = tipoResource 
            order by r.fkFamilia;
        else
			Select r.idRecursos as 'ID',r.Nombre, r.Costo, um.Nombre as 'Unidad' from recursos r inner join 
            unidadmedida um on r.fkUnidadMedida = um.idUnidadMedida where r.fkTipoRecurso = tipoResource;
       end if;
   END //
DELIMITER ;

call mostrarRecursos(0);

DELIMITER //
CREATE PROCEDURE deleteActividad (idActividad int)
	BEGIN
		delete from actividades where idActividades = idActividad;
    END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE updateActividad (IDActividad int, nombreAct varchar(45),unidad tinyint,cantidadAct int
,FechaInicioAct date, FechaFinAct date)
   BEGIN
		Update actividades set Nombre=nombreAct,fkUnidadMedida=unidad,
        Cantidad=cantidadAct, FechaInicio=FechaInicioAct,FechaFin=FechaFinAct
        where idActividades = IDActividad;
   END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE deleteDesgloce (idDesgloce int)
	BEGIN
		delete from desgloceactividad where idDesgloceActividad = idDesgloce;
    END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE updateDesgloce (idDesgloce int, costo float,cantity mediumint)
	BEGIN
		Update desgloceactividad set CostoUnidad = costo, Cantidad = cantity where idDesgloceActividad = 
        idDesgloce;
    END //
DELIMITER ;




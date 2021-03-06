-- MySQL Script generated by MySQL Workbench
-- 08/21/17 18:21:28
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema PICO-TEC
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema PICO-TEC
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `PICO-TEC` DEFAULT CHARACTER SET utf8 ;
USE `PICO-TEC` ;

-- -----------------------------------------------------
-- Table `PICO-TEC`.`tipoUsuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`tipoUsuario` (
  `idtipoUsuario` TINYINT NOT NULL AUTO_INCREMENT,
  `NombreTipo` CHAR NULL,
  PRIMARY KEY (`idtipoUsuario`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PICO-TEC`.`Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`Usuario` (
  `idUsuario` INT NOT NULL AUTO_INCREMENT,
  `fkTipoUsuario` TINYINT NULL,
  `Nombre` VARCHAR(45) NULL,
  `Apellido1` VARCHAR(45) NULL,
  `Apellido2` VARCHAR(45) NULL,
  `Email` NVARCHAR(80) NULL,
  `Carne` INT NULL,
  `Contrasena` BLOB NULL,
  PRIMARY KEY (`idUsuario`),
  INDEX `idtipoUsuario_idx` (`fkTipoUsuario` ASC),
  CONSTRAINT `tipoUsuarios`
    FOREIGN KEY (`fkTipoUsuario`)
    REFERENCES `PICO-TEC`.`tipoUsuario` (`idtipoUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PICO-TEC`.`Curso`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`Curso` (
  `idCurso` INT NOT NULL AUTO_INCREMENT,
  `fkProfesor` INT NULL,
  `Nombre` NVARCHAR(60) NULL,
  `Semestre` TINYINT(2) NULL,
  `Año` MEDIUMINT NULL,
  `NumeroGrupo` TINYINT NULL,
  PRIMARY KEY (`idCurso`),
  INDEX `FKProfesorEncargado_idx` (`fkProfesor` ASC),
  CONSTRAINT `FKProfesorEncargado`
    FOREIGN KEY (`fkProfesor`)
    REFERENCES `PICO-TEC`.`Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PICO-TEC`.`Proyecto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`Proyecto` (
  `idProyecto` INT NOT NULL AUTO_INCREMENT,
  `fkUsuarioEncargado` INT NULL,
  `Nombre` VARCHAR(45) NULL,
  `Ubicacion` VARCHAR(500) NULL,
  `Propietario` VARCHAR(120) NULL,
  `Descripcion` VARCHAR(500) NULL,
  `FechaInicio` DATE NULL,
  `FechaFin` DATE NULL,
  `fkCursoAsociado` INT NULL,
  PRIMARY KEY (`idProyecto`),
  INDEX `idUsuario_idx` (`fkUsuarioEncargado` ASC),
  INDEX `FKCurso_idx` (`fkCursoAsociado` ASC),
  CONSTRAINT `EstidUsuario`
    FOREIGN KEY (`fkUsuarioEncargado`)
    REFERENCES `PICO-TEC`.`Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FKCurso`
    FOREIGN KEY (`fkCursoAsociado`)
    REFERENCES `PICO-TEC`.`Curso` (`idCurso`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PICO-TEC`.`UnidadMedida`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`UnidadMedida` (
  `idUnidadMedida` TINYINT NOT NULL AUTO_INCREMENT,
  `Nombre` CHAR(15) NULL,
  PRIMARY KEY (`idUnidadMedida`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PICO-TEC`.`Actividades`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`Actividades` (
  `idActividades` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  `fkProyecto` INT NULL,
  `fkUnidadMedida` TINYINT NULL,
  `Cantidad` INT NULL,
  `FechaInicio` DATE NULL,
  `FechaFin` DATE NULL,
  PRIMARY KEY (`idActividades`),
  INDEX `FKProyecto_idx` (`fkProyecto` ASC),
  INDEX `FKUnidadMedida_idx` (`fkUnidadMedida` ASC),
  CONSTRAINT `FKProyecto`
    FOREIGN KEY (`fkProyecto`)
    REFERENCES `PICO-TEC`.`Proyecto` (`idProyecto`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FKUnidadMedida`
    FOREIGN KEY (`fkUnidadMedida`)
    REFERENCES `PICO-TEC`.`UnidadMedida` (`idUnidadMedida`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PICO-TEC`.`TipoRecurso`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`TipoRecurso` (
  `idTipoRecurso` TINYINT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NULL,
  PRIMARY KEY (`idTipoRecurso`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `PICO-TEC`.`familia`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`familia` (
  `idFamilia` TINYINT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(100) NULL,
  PRIMARY KEY (`idFamilia`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `PICO-TEC`.`Recursos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`Recursos` (
  `idRecursos` MEDIUMINT NOT NULL AUTO_INCREMENT,
  `Nombre` NVARCHAR(100) NULL,
  `Costo` FLOAT NULL,
  `fkUnidadMedida` TINYINT NULL,
  `fkTipoRecurso` TINYINT NULL,
  `fkFamilia` TINYINT NULL,
  PRIMARY KEY (`idRecursos`),
  INDEX `FKUnidadesMedida_idx` (`fkUnidadMedida` ASC),
  INDEX `FKTiposRecurso_idx` (`fkTipoRecurso` ASC),
  CONSTRAINT `FKUnidadesMedida`
    FOREIGN KEY (`fkUnidadMedida`)
    REFERENCES `PICO-TEC`.`UnidadMedida` (`idUnidadMedida`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FKTiposRecurso`
    FOREIGN KEY (`fkTipoRecurso`)
    REFERENCES `PICO-TEC`.`TipoRecurso` (`idTipoRecurso`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FKFamily`
    FOREIGN KEY (`fkFamilia`)
    REFERENCES `PICO-TEC`.`familia` (`idFamilia`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
  
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PICO-TEC`.`DesgloceActividad`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`DesgloceActividad` (
  `idDesgloceActividad` INT NOT NULL AUTO_INCREMENT,
  `fkActividad` INT NULL,
  `fkRecursos` MEDIUMINT NULL,
  `CostoUnidad` FLOAT NULL,
  `Cantidad` MEDIUMINT NULL,
  PRIMARY KEY (`idDesgloceActividad`),
  INDEX `FKActividad_idx` (`fkActividad` ASC),
  INDEX `FKRecursos_idx` (`fkRecursos` ASC),
  CONSTRAINT `FKActividad`
    FOREIGN KEY (`fkActividad`)
    REFERENCES `PICO-TEC`.`Actividades` (`idActividades`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FKRecursos`
    FOREIGN KEY (`fkRecursos`)
    REFERENCES `PICO-TEC`.`Recursos` (`idRecursos`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PICO-TEC`.`CursoXEstudiante`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PICO-TEC`.`CursoXEstudiante` (
  `idCursoXEstudiante` INT NOT NULL AUTO_INCREMENT,
  `fkCurso` INT NULL,
  `fkEstudiante` INT NULL,
  PRIMARY KEY (`idCursoXEstudiante`),
  INDEX `FKCursos_idx` (`fkCurso` ASC),
  INDEX `FKUsuarios_idx` (`fkEstudiante` ASC),
  CONSTRAINT `FKCursos`
    FOREIGN KEY (`fkCurso`)
    REFERENCES `PICO-TEC`.`Curso` (`idCurso`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FKUsuarios`
    FOREIGN KEY (`fkEstudiante`)
    REFERENCES `PICO-TEC`.`Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

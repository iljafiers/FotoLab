/*==============================================================*/
/* Database name:  FotoLab                                      */
/* DBMS name:      Microsoft SQL Server 2012                    */
/*==============================================================*/


use master
go

if db_id('fotolabdatabase') is not null
	drop database fotolabdatabase
go


/*==============================================================*/
/* Database: fotolabdatabase                                    */
/*==============================================================*/
create database fotolabdatabase
go


use fotolabdatabase
go


/*==============================================================*/
/* Tables                                                       */
/*==============================================================*/
create table klanten (
   id               	int           		primary key identity not null,
   naam					varchar(45)			not null,
   klant_key			varchar(50)			not null,
   straat				varchar(50)			,
   huisnummer			varchar(10)			,
   postcode				varchar(10)			,
   woonplaats			varchar(50)			
)
go

create table fotoseries (
	id 					int 				primary key identity not null,
	naam  			    varchar(50)			not null,
	klant_id			int 				,
	fotoproducent_id	int					,				
	datum				datetime 			not null,
	fotoserie_key		varchar(20)			unique not null
)
go

 -- MAX path limit: http://stackoverflow.com/questions/265769/maximum-filename-length-in-ntfs-windows-xp-and-windows-vista
create table fotos (
	id 					int 				primary key identity not null,
	fotoserie_id		int 				not null,
	md5					varchar(32)			not null
)
go

create table klanten_fotoseries (
	klant_id			int 				not null,
	fotoserie_id		int 				not null,
)
go

create table bestellingen (
	id 					int 				primary key identity not null,
	klant_id			int 				not null,
	datum				datetime 			not null
)
go

create table bestellingregels (
	bestelling_id		int 				not null,
	foto_id				int 				not null,
	fotoproduct_id		int 				not null
)
go

-- Op de site kunnen foto's en producten worden besteld. Hierin staan alle producten waarin een foto aangeleverd kan worden
-- Quote: "Na het inloggen kunnen klanten extra exemplaren van hun eigen foto's en producten online bestellen met daarop een gekozen foto. "
create table fotoproducten (
	id 					int 				primary key identity not null,
	naam				varchar(255)		unique not null,
	meerprijs			smallmoney			not null
)
go

create table fotoproducenten (
	id 					int 				primary key identity not null,
	naam				varchar(842)		not null,
	adres 				varchar(255)        not null,
	woonplaats 			varchar(255) 		not null
)
go

/*==============================================================*/
/* Stored Procedures                                            */
/*==============================================================*/

CREATE PROC sp_InsertFoto
@fotoserieId 		int,
@md5 				varchar(32)
AS 
BEGIN
	IF NOT EXISTS(SELECT 1 FROM fotos WHERE md5 = @md5 AND fotoserie_id = @fotoserieId)
	BEGIN 
		INSERT INTO fotos (fotoserie_id, md5) OUTPUT INSERTED.ID AS Id VALUES (@fotoserieId, @md5) 
	END
	ELSE
	BEGIN
		SELECT 0 AS Id;
	END
END
go
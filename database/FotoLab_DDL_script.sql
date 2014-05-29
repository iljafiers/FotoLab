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
/* Table: Gebruikers                                            */
/*==============================================================*/
create table klanten (
   id               	int           		primary key identity not null,
   naam					varchar(45)			not null
)
go

create table fotoserie (
	id 					int 				primary key identity not null,
	serie_key			varchar(50)			not null 				
)
go

 -- MAX path limit: http://stackoverflow.com/questions/265769/maximum-filename-length-in-ntfs-windows-xp-and-windows-vista
create table foto (
	id 					int 				primary key identity not null,
	fotoserie_id		int 				not null,
	path				varchar(260)		not null
)
go

create table klant_key (
	klant_id			int 				not null,
	fotoserie_id		int 				not null
)
go

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
/* Database: fotolabdatabase                                     */
/*==============================================================*/
create database fotolabdatabase
go


use fotolabdatabase
go


/*==============================================================*/
/* Table: Gebruikers                                       */
/*==============================================================*/
create table Klanten (
   id               	numeric(5)           primary key identity,
   naam					varchar(45)			 not null
)
go


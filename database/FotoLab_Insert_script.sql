
/* 
   Insert-script voor fotolab database
   SQL Server 2012
*/
use fotolabdatabase
go

-- sloop alles
TRUNCATE TABLE klanten;
TRUNCATE TABLE fotoserie;
TRUNCATE TABLE foto;
TRUNCATE TABLE klant_key;

/* Opmerking:
   Een enkel aanhalingsteken binnen een string (zoals in muziekschool 2 en stuk 12) moet
   twee keer worden genoteerd; anders wordt het opgevat als einde-string teken.
*/
INSERT INTO klanten VALUES ('Pieter Scheffers');
INSERT INTO klanten VALUES ('Ilja Fiers');
go
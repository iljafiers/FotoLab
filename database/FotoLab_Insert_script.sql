
/* 
   Insert-script voor fotolab database
   SQL Server 2012
*/
use fotolabdatabase
go

-- sloop alles
TRUNCATE Klanten;

/* Opmerking:
   Een enkel aanhalingsteken binnen een string (zoals in muziekschool 2 en stuk 12) moet
   twee keer worden genoteerd; anders wordt het opgevat als einde-string teken.
*/
INSERT INTO Klanten VALUES (1, 'Pieter Scheffers');
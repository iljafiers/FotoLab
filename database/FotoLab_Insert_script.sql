
/* 
   Insert-script voor fotolab database
   SQL Server 2012
*/
use fotolabdatabase
go

-- sloop alles
TRUNCATE TABLE klanten;
TRUNCATE TABLE fotoseries;
TRUNCATE TABLE fotos;
TRUNCATE TABLE klanten_fotoseries;
TRUNCATE TABLE fotoproducten;
TRUNCATE TABLE bestellingen;
TRUNCATE TABLE bestellingregels;
go

/* Opmerking:
   Een enkel aanhalingsteken binnen een string (zoals in muziekschool 2 en stuk 12) moet
   twee keer worden genoteerd; anders wordt het opgevat als einde-string teken.
*/
INSERT INTO klanten(naam, klant_key) VALUES ('Pieter Scheffers', 'PS');
INSERT INTO klanten(naam, klant_key) VALUES ('Ilja Fiers', 'IF');
INSERT INTO klanten(naam, klant_key) VALUES ('Marco Langenhuizen', 'ML');
GO

INSERT INTO fotoseries (naam, datum, klant_id) VALUES ('Klassefoto NOHi',		'2014-06-06',	0);
INSERT INTO fotoseries (naam, datum, klant_id) VALUES ('Vakantie',			    '2013-06-06',	1);
INSERT INTO fotoseries (naam, datum, klant_id) VALUES ('Groepsfoto teamuitje',	'2013-08-03',	1);
GO


INSERT INTO fotoproducten (naam, meerprijs) VALUES 	('Origineel', 0.00);
INSERT INTO fotoproducten (naam, meerprijs) VALUES 	('Fotolijst', 4.99);
INSERT INTO fotoproducten (naam, meerprijs) VALUES 	('Schilderij (60x40)', 29.99);
INSERT INTO fotoproducten (naam, meerprijs) VALUES 	('Schilderij (120x80)', 59.99);
INSERT INTO fotoproducten (naam, meerprijs) VALUES 	('Schilderij (240x160', 99.99);
INSERT INTO fotoproducten (naam, meerprijs) VALUES 	('Poster (30x20)', 9.99);
INSERT INTO fotoproducten (naam, meerprijs) VALUES 	('Poster (60x40)', 18.99);
INSERT INTO fotoproducten (naam, meerprijs) VALUES 	('Poster (90x60)', 24.99);
go



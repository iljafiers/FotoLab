
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

INSERT INTO fotoseries (fotoserie_key, datum, gepubliseerd) VALUES ('XAtC9lRSxmuiOjHJ7DGuPTFrhtO9MECY9Pgd1g2pdhUPKDGuwn', '2014-06-06', 1);
INSERT INTO fotoseries (fotoserie_key, datum, gepubliseerd) VALUES ('e4bnWKw2aDxBuM69J814YW2kf9XYUKdWG5kczqjPPUgk4KOWkS', '2013-06-06', 0);
INSERT INTO fotoseries (fotoserie_key, datum, gepubliseerd) VALUES ('x5pHg3Tg6x27v25LueHPLUXzer8jpnKcMBDhOvsizzCGVVMWkW', '2013-08-03', 0);
INSERT INTO fotoseries (fotoserie_key, datum, gepubliseerd) VALUES ('hUIl4Q5gRJpznL7K7Zs7fVtAh15nk67zxnaNqh95egk4S8VLpx', '2012-09-15', 0);
INSERT INTO fotoseries (fotoserie_key, datum, gepubliseerd) VALUES ('flappies_fotoserie_key', '2013-11-12', 0);
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



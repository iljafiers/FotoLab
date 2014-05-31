
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
TRUNCATE TABLE klant_fotoserie;

/* Opmerking:
   Een enkel aanhalingsteken binnen een string (zoals in muziekschool 2 en stuk 12) moet
   twee keer worden genoteerd; anders wordt het opgevat als einde-string teken.
*/
INSERT INTO klanten VALUES ('Pieter Scheffers');
INSERT INTO klanten VALUES ('Ilja Fiers');
GO

INSERT INTO fotoserie (serie_key) VALUES ('XAtC9lRSxmuiOjHJ7DGuPTFrhtO9MECY9Pgd1g2pdhUPKDGuwn'),
                                         ('e4bnWKw2aDxBuM69J814YW2kf9XYUKdWG5kczqjPPUgk4KOWkS'),
                                         ('x5pHg3Tg6x27v25LueHPLUXzer8jpnKcMBDhOvsizzCGVVMWkW'),
                                         ('hUIl4Q5gRJpznL7K7Zs7fVtAh15nk67zxnaNqh95egk4S8VLpx')
GO


INSERT INTO fotoproducten (naam, meerprijs) VALUES ('Origineel', 0.00),
                                               ('Fotolijst', 4.99),
                                               ('Schilderij (60x40)', 29.99),
                                               ('Schilderij (120x80)', 59.99),
                                               ('Schilderij (240x160', 99.99),
                                               ('Poster (30x20)', 9.99),
                                               ('Poster (60x40)', 18.99),
                                               ('Poster (90x60)', 24.99)
go



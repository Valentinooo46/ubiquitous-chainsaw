IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Animals')
                CREATE TABLE Animals (
                    Id INT PRIMARY KEY IDENTITY,
                    Name NVARCHAR(50),
                    Species NVARCHAR(50),
                    Age INT,
                    ArrivalDate DATE
                );


INSERT INTO Animals (Name, Species, Age, ArrivalDate)
                   VALUES ('Teddy', 'Dog', 3, '2025-11-25');


SELECT * FROM Animals;
DECLARE @UserIngestion TABLE (
    UserId VARCHAR(50),
    Password VARCHAR(255),
    Name VARCHAR(128),
    Role VARCHAR(20)
);

INSERT INTO @UserIngestion(UserId,Password,Name,Role)
VALUES ('admin1','admin1','user number 1','ADMIN'),
('admin2','admin2','admin number 2','ADMIN'),
('admin3','admin3','admin number 3','ADMIN'),
('guest1','guest1','guest number 1', 'GUEST'),
('guest2','guest2','guest number 2', 'GUEST'),
('guest3','guest3','guest number 3', 'GUEST')


MERGE [User] AS trg
    USING (
        SELECT UserId,Password,Name,Role
        FROM @UserIngestion
    ) 
    AS src
    ON trg.UserId = src.UserId
    WHEN NOT MATCHED BY TARGET THEN 
        INSERT (UserId,Password,Name,Role)
        VALUES (UserId,Password,Name,Role);
GO
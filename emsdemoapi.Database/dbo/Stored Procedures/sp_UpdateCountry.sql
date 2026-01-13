CREATE PROCEDURE sp_UpdateCountry 
    @Id int,
    @Name NVARCHAR(150)
AS 
BEGIN 
    Update Countries set Name = @Name where Id = @Id 
END
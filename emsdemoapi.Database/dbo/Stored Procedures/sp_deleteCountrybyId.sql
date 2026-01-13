CREATE PROCEDURE sp_deleteCountrybyId  
@Id int
AS   
BEGIN   
    Delete from Countries where Id = @Id;   
END
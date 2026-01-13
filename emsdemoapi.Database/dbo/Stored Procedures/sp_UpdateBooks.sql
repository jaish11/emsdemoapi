create   proc sp_UpdateBooks  
(@Id int,@Name nvarChar(max))  
as  
begin  
    update Books set Name = @Name where Id =@Id  
end
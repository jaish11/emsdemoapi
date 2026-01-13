create proc sp_UpdateStates
(@Id int,@Name nvarChar(max),@CountryId int)
as
begin
    update States set Name = @Name, CountryId=@CountryId where Id =@Id
end
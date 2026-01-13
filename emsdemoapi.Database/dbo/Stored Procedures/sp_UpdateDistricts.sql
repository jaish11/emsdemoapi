create proc sp_UpdateDistricts
(@Id int,@Name nvarChar(max),@CountryId int,@StateId int)
as
begin
    update Districts set Name = @Name, CountryId=@CountryId, StateId=@StateId where Id =@Id
end
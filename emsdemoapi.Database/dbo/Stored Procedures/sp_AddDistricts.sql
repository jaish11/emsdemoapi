create proc sp_AddDistricts
(@Name nvarChar(max),@CountryId int,@StateId int)
as
begin
    insert into Districts Values(@Name,@CountryId,@StateId)
end
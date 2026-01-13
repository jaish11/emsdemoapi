create proc sp_AddStates
(@Name nvarChar(max),@CountryId int)
as
begin
    insert into States Values(@Name,@CountryId)
end
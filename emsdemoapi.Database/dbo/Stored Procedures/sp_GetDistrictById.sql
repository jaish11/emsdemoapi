create proc sp_GetDistrictById
@Id int
as
begin
    select * from Districts where Id =@Id
end
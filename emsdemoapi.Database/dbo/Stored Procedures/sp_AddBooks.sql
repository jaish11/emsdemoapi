create proc sp_AddBooks
(@Name nvarChar(max))
as
begin
    insert into Books Values(@Name)
end
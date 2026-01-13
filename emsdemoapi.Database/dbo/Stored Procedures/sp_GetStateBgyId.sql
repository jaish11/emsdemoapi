create proc sp_GetStateBgyId
@Id int
as
begin
    select * from States where Id =@Id
end
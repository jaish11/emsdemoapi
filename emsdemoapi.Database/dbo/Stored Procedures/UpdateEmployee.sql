create proc UpdateEmployee(@Id int,@Name nvarchar(max),@Position nvarchar(max),@Salary decimal(18,2))
as
begin
	update Employees set Name= @Name,Position=@Position,Salary=@Salary where Id=@Id;
end
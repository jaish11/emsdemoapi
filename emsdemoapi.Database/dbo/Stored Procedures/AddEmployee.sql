
create proc AddEmployee(@Name nvarchar(max),@Position nvarchar(max),@Salary decimal(18,2))
as
begin
	insert into Employees values(@Name,@Position,@Salary);
end
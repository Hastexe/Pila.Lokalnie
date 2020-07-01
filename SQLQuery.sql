Create proc Sp_Users
@User varchar(255),
@Pass varchar(255),
@Isvalid BIT OUT

AS
BEGIN
SET @Isvalid = (SELECT	COUNT(1) FROM dbo.Users WHERE Login = @User AND Password = @Pass)
end
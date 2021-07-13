cd ./ASP_Labs
echo "Please, enter migration name:"
read migrationName

dotnet ef migrations add "${migrationName}" -c ApplicationDbContext
dotnet ef database update

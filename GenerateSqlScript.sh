cd ./WebApp

dotnet ef migrations script 0 -i -o ../Scripts/script.sql -c ApplicationDbContext
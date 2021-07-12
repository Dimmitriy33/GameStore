echo "Please, enter runtime identifier:"
read rid

dotnet publish -c Release -r $rid --self-contained true
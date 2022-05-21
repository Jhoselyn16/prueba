# hotel-santa-ursula-II
Rama Freddy
Remover la carpeta Migrations

Cambiar la cadena de conexion

dotnet ef migrations add Initial --context EsportshubApi.Models.ApplicationDbContext -o YourFolderPath

example:

dotnet ef migrations add OtherSchema --context hotel_santa_ursula_II.Data.ApplicationDbContext -o "G:\git 2021-so\hotel-santa-ursula-II\Data\Migrations"

dotnet ef database update
G:\git 2021-so\hotel-santa-ursula-II\Data
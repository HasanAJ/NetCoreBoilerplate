build:
	dotnet build ./src /p:Configuration=Debug /p:Platform="Any CPU"

clean:
	dotnet clean ./src /p:Configuration=Debug /p:Platform="Any CPU"

run:
	dotnet run -p ./src/NetCoreBoilerplate.Api /p:Configuration=Debug /p:Platform="Any CPU"

watch:
	dotnet watch run -p ./src/NetCoreBoilerplate.Api /p:Configuration=Debug /p:Platform="Any CPU"

init:
	docker-compose build

up:
	docker-compose up

up-infra:
	docker-compose up postgres

stop:
	docker-compose stop

add-migration:
	@read -p "Enter Migration Name: " migrationName; \
	dotnet ef migrations add $$migrationName --startup-project ./src/NetCoreBoilerplate.Api/NetCoreBoilerplate.Api.csproj --project ./src/NetCoreBoilerplate.Infrastructure/NetCoreBoilerplate.Infrastructure.csproj

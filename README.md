# BTW-Documents-Back
Utilicé SQL Server en lugar de PostgreSQL por las siguientes razones:
- Integración nativa y optimizada con Entity Framework Core.
- Facilidad de configuración en entornos locales (especialmente con SQL Server Express o Docker).
- Experiencia previa que permite un desarrollo más rápido y enfocado en la lógica de negocio.

  # 1. Clonar el repositorio
git clone https://github.com/andres7guillen/BTW-Documents-Back.git
cd <Ubicacion carpeta donde se clonó>/BTW.APP

# 2. Restaurar dependencias
dotnet restore

#3. Aplicar migraciones
verificar que la cadena de conexion del app settings json esté apuntando a la DB correcta.
luego se abre el package console se apunta al proyecto donde está mi DbContext(BTW.Application), el start up project que sea mi BTW.Api y ejecutar el siguiente comando:
Update-Database iniApp

# 3. Ejecutar la aplicación
dotnet run o Run desde VS

# Respuesta a la pregunta:Si este sistema fuera a procesar 50.000 documentos diarios en producción, que
cambiarias o reforzarías en tu implementación actual y por qué?
lo que mejoraria con mas tiempo, seria definir indices de columnas que son claves para el negocio en mis tablas de la DB, si esos 50.000 usuarios consumen mi api al mismo tiempo podria poner un balanceador de carga, usaria concurrencia y pondria validaciones para evitar los casos donde dos ususrios hagan cambios al mismo tiempo sobre un mismo documento, tambien se podria dejar las peticiones de forma asincrona con algun broker para evitar que se pierdan llamadas a mis endpoints.

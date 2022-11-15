# JF_Data_Utils
Data utilities

# Patrones a usar

- Services Pattern:
Nos permite organizar la lógica de nuestro negocio. Anteriormente, las consultas a la DB la armabamos en esta capa pero con este patrón de UnitOfWork lo que vamos a usar ahora son repositorios.

- Repository Pattern:
Acceso a los datos. Creación de las funciones de creación, actualización, borrado y consulta
nos permite manipular el acceso a la base de datos; es decir, mediante este podemos realizar las consultas a la DB. En el repositorio de GitHub he adjuntado un patrón repositorio genérico porque sino, nos queda chico o nos da la sensación que falta más para poder hacer consultas a la DB. De todas formas, esta clase del patrón genérico ha sido adaptado para un proyecto mío, por lo cual en tu proyecto deberías modificarlo, expandirlo.

- UnitOfWork Pattern:
Centraliza las conexiones a la base de datos y gestiona los cambios (context.SaveChanges();).

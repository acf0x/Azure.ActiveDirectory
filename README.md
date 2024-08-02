# Azure AD

Este repositorio contiene dos aplicaciones: EntraID.ConsoleApp1 y EntraID.WebApplication1.

## EntraID.ConsoleApp1
Aplicación de consola que interactúa con Microsoft Graph API para autenticar y listar usuarios en Azure Active Directory.

### Funcionamiento:
1. Configura la aplicación cliente con su ID, secreto y autoridad (tenant ID).
2. Genera un token de acceso utilizando los scopes necesarios para acceder a Microsoft Graph API.
3. Realiza una petición HTTP para listar usuarios de Azure AD y deserializa la respuesta en objetos de usuario.
4. Utiliza el cliente GraphServiceClient para obtener la lista de usuarios y los imprime en la consola.

## EntraID.WebApplication1
Aplicación web ASP.NET Core que implementa autenticación y autorización utilizando OpenID Connect y Microsoft Identity. Proporciona una interfaz de usuario protegida con políticas de autorización basadas en roles y grupos de Azure AD.

### Funcionamiento:
1. Configura los servicios de autenticación con OpenID Connect y Microsoft Identity.
2. Define políticas de autorización basadas en roles y grupos de Azure AD.
3. Configura los controladores y vistas para requerir que los usuarios estén autenticados.
4. Proporciona páginas de Razor con soporte de identidad de Microsoft.
5. Configura el pipeline de middleware de ASP.NET Core para manejar excepciones, redirección HTTPS, archivos estáticos, y enrutamiento.

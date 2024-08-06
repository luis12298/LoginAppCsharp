# LoginAppCsharp

Aplicación básica de inicio de sesión con Google usando C# que muestra el nombre de usuario y el correo electrónico después de la autenticación. La aplicación se ejecuta como una aplicación de Windows Forms y utiliza la autenticación OAuth 2.0 de Google.

## Características
* Autenticación de usuario mediante la API de OAuth 2.0 de Google.
* Recuperación de información de perfil del usuario (nombre y correo electrónico).
* Visualización de la información del usuario en un MessageBox.
## Requisitos
* .NET Framework 4.7.2 o superior
* UIAutomationClient y UIAutomationTypes agregados como referencias
* Una cuenta de Google Cloud con las credenciales de OAuth 2.0 configuradas
## Agregar referencias de UIAutomationClient y UIAutomationTypes
* Abre tu proyecto en Visual Studio.
* En el Explorador de Soluciones, haz clic derecho en el nombre del proyecto y
* selecciona Add > Reference.
* En la ventana Reference Manager, navega a Assemblies > Framework.
* Busca y selecciona UIAutomationClient y UIAutomationTypes.
* Haz clic en OK para agregar las referencias a tu proyecto.
Estas bibliotecas son necesarias para interactuar con la interfaz de usuario de Chrome y obtener el código de autorización automáticamente.

## Configuración de Google Cloud
* Paso 1: Crear un proyecto en Google Cloud
* Ve a la Consola de Google Cloud.
* Inicia sesión con tu cuenta de Google.
* Crea un nuevo proyecto o selecciona un proyecto existente:
* Haz clic en el menú desplegable del proyecto en la parte superior de la página.
*Haz clic en Nuevo Proyecto y sigue las instrucciones.

## Paso 2: Habilitar la API de Google People
* En el menú de navegación, ve a API & Services > Library.
* Busca "Google People API" en la barra de búsqueda.
* Haz clic en Google People API.
* Haz clic en Enable para habilitar la API para tu proyecto.
## Paso 3: Configurar las credenciales de OAuth 2.0
* En el menú de navegación, ve a API & Services > Credentials.
* Haz clic en Create Credentials y selecciona OAuth 2.0 Client ID.
* Configura la pantalla de consentimiento de OAuth si es necesario:
* Completa la información solicitada, como el nombre de la aplicación, el logotipo, etc.
* Guarda los cambios.
* En Application type, selecciona Desktop app.
Completa el formulario y haz clic en Create.
Copia el Client ID y el Client Secret que se generan. Los necesitarás en tu aplicación.


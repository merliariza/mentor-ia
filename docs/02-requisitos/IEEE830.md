# Especificación de Requisitos de Software - Mentor-IA

## 1. Introducción

### 1.1 Propósito

El presente documento establece la Especificación de Requisitos de Software (SRS) para Mentor-IA, una plataforma web orientada al apoyo del proceso de aprendizaje mediante el uso de inteligencia artificial.

El documento tiene como propósito definir de manera clara, organizada y verificable las funcionalidades, características, restricciones, interfaces y condiciones de calidad que debe cumplir la solución.

La especificación sirve como referencia para las etapas de análisis, diseño, construcción, pruebas, implementación y documentación del proyecto, permitiendo mantener trazabilidad entre las necesidades identificadas, las funcionalidades desarrolladas y las pruebas realizadas.

Mentor-IA permite a usuarios registrados interactuar con un asistente de inteligencia artificial para realizar consultas relacionadas con su proceso de aprendizaje y, a partir del contenido trabajado, disponer de recursos como flashcards y cuestionarios. La plataforma también permite realizar una evaluación, consultar su resultado y visualizar información relacionada con el progreso del proceso de aprendizaje.

### 1.2 Alcance

Mentor-IA es una aplicación web compuesta por un Frontend y un Backend que trabajan de manera integrada.

La solución permite:

* Registrar usuarios.
* Autenticar usuarios mediante credenciales.
* Gestionar el acceso a funcionalidades protegidas.
* Navegar mediante un menú principal.
* Consultar un panel principal.
* Interactuar con un asistente de inteligencia artificial.
* Generar recursos de aprendizaje relacionados con el contenido trabajado.
* Consultar flashcards.
* Generar y realizar cuestionarios.
* Consultar el resultado de la evaluación correspondiente al proceso vigente.
* Consultar información de progreso.
* Consultar y actualizar la información disponible del perfil.
* Cerrar sesión.
* Asociar la información de aprendizaje con el usuario correspondiente.
* Gestionar errores producidos durante las operaciones principales.

El sistema utiliza una base de datos relacional para almacenar la información requerida por las funcionalidades implementadas y un servicio externo de inteligencia artificial para el procesamiento de las consultas.

El alcance contempla la utilización de Mentor-IA mediante un navegador web y su disponibilidad en un entorno desplegado.

No forma parte del alcance una funcionalidad de historial de conversaciones para que el usuario consulte preguntas anteriores del chat. La interacción se centra en la consulta actual y en el proceso de aprendizaje asociado.

Tampoco se contempla dentro del alcance la gestión de múltiples historiales de evaluaciones como una funcionalidad independiente. El proceso se maneja sobre la evaluación correspondiente al proceso de aprendizaje vigente.

### 1.3 Definiciones, acrónimos y abreviaturas

| Término             | Definición                                                                                                                        |
| ------------------- | --------------------------------------------------------------------------------------------------------------------------------- |
| Mentor-IA           | Nombre de la solución web desarrollada como asistente de apoyo al aprendizaje mediante inteligencia artificial.                   |
| IA                  | Inteligencia Artificial.                                                                                                          |
| SRS                 | Software Requirements Specification o Especificación de Requisitos de Software.                                                   |
| HU                  | Historia de Usuario.                                                                                                              |
| RF                  | Requisito Funcional.                                                                                                              |
| RNF                 | Requisito No Funcional.                                                                                                           |
| CU                  | Caso de Uso.                                                                                                                      |
| API                 | Application Programming Interface.                                                                                                |
| Frontend            | Capa de la aplicación con la que interactúa directamente el usuario.                                                              |
| Backend             | Capa encargada de procesar las solicitudes, aplicar la lógica de negocio y comunicarse con la base de datos y servicios externos. |
| Base de datos       | Sistema utilizado para almacenar y consultar la información persistente de la aplicación.                                         |
| Flashcard           | Tarjeta de estudio utilizada para reforzar conceptos o contenidos académicos.                                                     |
| Evaluación          | Actividad mediante la cual se comprueba el nivel de comprensión del usuario sobre el contenido trabajado.                         |
| Progreso            | Información utilizada para representar el avance o desempeño del usuario dentro del proceso de aprendizaje.                       |
| Usuario autenticado | Usuario que ha iniciado sesión correctamente y cuenta con autorización para acceder a las funcionalidades privadas.               |
| JWT                 | JSON Web Token. Mecanismo utilizado para transportar información de autenticación y autorización.                                 |
| OpenRouter          | Servicio utilizado como intermediario para acceder al modelo de inteligencia artificial configurado por la aplicación.            |
| PostgreSQL          | Sistema gestor de bases de datos relacional utilizado por Mentor-IA.                                                              |
| EF Core             | Entity Framework Core, tecnología utilizada para la interacción entre el Backend y la base de datos.                              |

### 1.4 Referencias

Para la elaboración de esta especificación se consideran como referencia:

* Historias de Usuario de Mentor-IA.
* Código fuente del proyecto Mentor-IA.
* Arquitectura implementada en Frontend y Backend.
* Modelo de datos utilizado por la aplicación.
* Documentación técnica de las tecnologías empleadas.
* Especificación IEEE 830 como referencia para la estructura de la Especificación de Requisitos de Software.
* Requerimientos técnicos establecidos para el proyecto formativo del SENA.

### 1.5 Descripción general del documento

El documento se encuentra organizado en tres secciones principales.

La primera sección presenta el propósito, alcance, terminología, referencias y organización de la especificación.

La segunda sección describe la solución desde una perspectiva general, incluyendo su relación con el entorno, las funciones principales, los tipos de usuarios, las restricciones y las dependencias.

La tercera sección establece los requisitos específicos del sistema, incluyendo requisitos funcionales, no funcionales, de interfaz y de seguridad.

Los requisitos funcionales se encuentran identificados mediante códigos `RF-XXX`, mientras que los requisitos no funcionales utilizan códigos `RNF-XXX`. Esta identificación permitirá establecer la trazabilidad con casos de uso y casos de prueba.

# 2. Descripción general

## 2.1 Perspectiva del producto

Mentor-IA es una aplicación web desarrollada como una solución integrada de Frontend, Backend, base de datos y servicio externo de inteligencia artificial.

La arquitectura general está compuesta por los siguientes elementos:

**Usuario → Frontend → Backend → Base de datos / Servicio de IA**

El Frontend proporciona la interfaz de interacción con el usuario y permite acceder a las funcionalidades de autenticación, navegación, chat, recursos de aprendizaje, evaluación, progreso y perfil.

El Backend procesa las solicitudes provenientes del Frontend, aplica las reglas de negocio, gestiona la autenticación y autorización, administra la información persistente y establece la comunicación con los servicios externos requeridos.

La información persistente se administra mediante PostgreSQL.

La funcionalidad de inteligencia artificial utiliza un servicio externo mediante OpenRouter para procesar las consultas realizadas desde Mentor-IA.

La aplicación está diseñada para ejecutarse mediante un navegador web y puede utilizarse desde un entorno local durante el desarrollo o desde el entorno desplegado definido para el proyecto.

La solución mantiene la información de aprendizaje asociada al usuario correspondiente. Esta asociación permite que los recursos y resultados disponibles correspondan al usuario autenticado.

La aplicación no contempla un historial de conversaciones consultable por el usuario. Las consultas se procesan de acuerdo con la interacción actual y el proceso de aprendizaje vigente.

## 2.2 Funciones del producto

Las principales funciones de Mentor-IA son:

### Gestión de usuarios

* Registro de usuarios.
* Inicio de sesión.
* Gestión de sesión.
* Cierre de sesión.

### Navegación y acceso

* Visualización del menú principal.
* Acceso al panel principal.
* Navegación entre las funcionalidades disponibles.
* Protección de funcionalidades que requieren autenticación.

### Asistente de inteligencia artificial

* Ingreso de consultas.
* Envío de preguntas al asistente.
* Procesamiento de las consultas mediante el servicio de IA.
* Visualización de respuestas.
* Gestión de errores durante la comunicación con el servicio.

### Recursos de aprendizaje

* Generación de recursos relacionados con el contenido trabajado.
* Generación de flashcards.
* Consulta de flashcards.
* Generación de cuestionarios.

### Evaluación

* Presentación de preguntas.
* Selección de respuestas.
* Finalización de la evaluación.
* Procesamiento de respuestas.
* Obtención del resultado correspondiente.

### Progreso

* Registro de información relacionada con el proceso de aprendizaje.
* Consulta de información de progreso.
* Asociación del progreso con el usuario correspondiente.

### Persistencia

* Almacenamiento de información de usuarios.
* Almacenamiento de información relacionada con el proceso de aprendizaje.
* Almacenamiento de recursos y resultados que requieren persistencia.
* Consulta de información asociada al usuario autenticado.

### Gestión de errores

* Validación de información.
* Presentación de mensajes al usuario.
* Gestión de errores de comunicación.
* Gestión controlada de errores provenientes de servicios externos.

## 2.3 Características de los usuarios

### Usuario visitante

Es la persona que accede a Mentor-IA sin haber iniciado sesión.

Puede acceder a las funcionalidades públicas de la aplicación, como la pantalla inicial y el registro.

No puede acceder a funcionalidades que requieran autenticación.

### Usuario registrado

Es la persona que posee una cuenta en Mentor-IA y puede autenticarse mediante sus credenciales.

Una vez autenticado, puede utilizar las funcionalidades disponibles para el proceso de aprendizaje, incluyendo el asistente de IA, recursos de aprendizaje, evaluación, progreso y perfil.

El usuario no requiere conocimientos técnicos especializados para utilizar la plataforma.

### Administrador técnico

Para efectos de la arquitectura de la solución, existen responsabilidades técnicas relacionadas con la administración, configuración, mantenimiento y despliegue del sistema.

Estas responsabilidades corresponden a la gestión técnica de la aplicación y no constituyen una funcionalidad principal destinada al usuario final de Mentor-IA.

## 2.4 Restricciones

Mentor-IA presenta las siguientes restricciones:

* La solución requiere conexión a Internet para utilizar los servicios desplegados.
* La funcionalidad de inteligencia artificial depende de la disponibilidad del servicio externo configurado.
* El acceso a funcionalidades privadas requiere autenticación.
* La información debe mantenerse asociada al usuario correspondiente.
* La aplicación depende de la disponibilidad del Backend para realizar operaciones que requieran servicios de la API.
* La aplicación depende de la disponibilidad de PostgreSQL para las operaciones que requieran persistencia.
* El Frontend depende de la URL configurada para comunicarse con el Backend.
* El servicio de inteligencia artificial depende de la configuración y disponibilidad de OpenRouter.
* La aplicación se encuentra orientada a su utilización mediante navegador web.
* El alcance actual no contempla un historial de conversaciones consultable.
* El alcance actual no contempla una funcionalidad independiente para consultar múltiples historiales de evaluaciones.
* La información generada y almacenada está limitada a las funcionalidades implementadas en la versión actual del sistema.

## 2.5 Supuestos y dependencias

### Supuestos

Para la operación de Mentor-IA se consideran los siguientes supuestos:

* El usuario dispone de un dispositivo con acceso a un navegador web.
* El usuario dispone de conexión a Internet cuando utiliza el entorno desplegado.
* El usuario utiliza sus propias credenciales para acceder a la plataforma.
* El usuario realiza consultas relacionadas con su proceso de aprendizaje.
* Los servicios externos requeridos se encuentran disponibles.
* La configuración de los entornos se encuentra correctamente establecida.

### Dependencias

Mentor-IA depende de:

* Angular para el desarrollo del Frontend.
* ASP.NET Core y .NET para el Backend.
* PostgreSQL como sistema gestor de base de datos.
* Entity Framework Core para la interacción con la base de datos.
* Npgsql como proveedor de PostgreSQL.
* OpenRouter para la integración con el servicio de inteligencia artificial.
* JWT para los mecanismos de autenticación basados en tokens.
* Servicios y paquetes definidos en los archivos de configuración de Frontend y Backend.
* Infraestructura de despliegue utilizada para publicar el Frontend y Backend.

# 3. Requisitos específicos

## 3.1 Requisitos funcionales

### RF-001 - Registro de usuario

El sistema debe permitir que un visitante cree una cuenta en Mentor-IA mediante un formulario de registro.

**Criterios:**

* Debe permitir acceder al formulario de registro.
* Debe solicitar los datos obligatorios definidos para la creación de la cuenta.
* Debe validar los campos requeridos.
* Debe validar el formato de los datos correspondientes.
* Debe verificar que el correo electrónico no se encuentre registrado.
* Debe crear la cuenta cuando la información sea válida.

**Trazabilidad:** HU-001.

### RF-002 - Autenticación de usuarios

El sistema debe permitir que un usuario registrado inicie sesión utilizando sus credenciales.

**Criterios:**

* Debe permitir ingresar las credenciales.
* Debe validar las credenciales.
* Debe permitir el acceso cuando sean correctas.
* Debe informar cuando las credenciales no sean válidas.
* Debe establecer la sesión correspondiente al usuario autenticado.

**Trazabilidad:** HU-002.

### RF-003 - Control de acceso a funcionalidades

El sistema debe restringir el acceso a las funcionalidades que requieren autenticación.

**Criterios:**

* Los usuarios no autenticados no deben acceder a información privada.
* Las operaciones protegidas deben requerir autorización.
* El sistema debe identificar al usuario autenticado durante las operaciones correspondientes.
* La información de cada usuario debe mantenerse asociada a su cuenta.

**Trazabilidad:** HU-002, HU-011.

### RF-004 - Navegación mediante menú principal

El sistema debe proporcionar un menú de navegación para permitir al usuario autenticado acceder a las funcionalidades disponibles.

**Criterios:**

* Debe mostrar las opciones correspondientes.
* Cada opción debe dirigir a la sección definida.
* La navegación debe mantenerse disponible durante el uso de la aplicación según el diseño establecido.
* Las funcionalidades privadas no deben estar disponibles para usuarios no autenticados.

**Trazabilidad:** HU-003.

### RF-005 - Visualización del panel principal

El sistema debe mostrar un panel principal al usuario autenticado.

**Criterios:**

* Debe presentar los accesos principales.
* Debe mostrar la información correspondiente al usuario autenticado.
* Debe permitir navegar hacia las funcionalidades disponibles.
* Debe mantener la sesión durante la navegación mientras sea válida.

**Trazabilidad:** HU-004.

### RF-006 - Interacción con el asistente de inteligencia artificial

El sistema debe permitir al usuario realizar consultas mediante el asistente de inteligencia artificial.

**Criterios:**

* Debe proporcionar un espacio para ingresar una pregunta.
* Debe permitir enviar la consulta.
* Debe procesar la consulta mediante el servicio configurado.
* Debe mostrar la respuesta generada.
* Debe permitir realizar una nueva consulta.
* Debe informar cuando no sea posible procesar una solicitud.

**Consideración:** Las consultas se procesan como interacciones del proceso actual. El sistema no debe presentar un historial de conversaciones consultable.

**Trazabilidad:** HU-005.

### RF-007 - Generación de recursos de aprendizaje

El sistema debe permitir generar recursos de aprendizaje relacionados con el contenido trabajado.

**Criterios:**

* Debe permitir generar cuestionarios cuando corresponda.
* Debe permitir generar flashcards cuando corresponda.
* Los recursos deben relacionarse con el contenido académico trabajado.
* Los recursos deben asociarse al usuario correspondiente.
* Los recursos deben quedar disponibles para las funcionalidades destinadas a su consulta.

**Trazabilidad:** HU-006.

### RF-008 - Consulta de flashcards

El sistema debe permitir consultar las flashcards generadas para el proceso de aprendizaje del usuario.

**Criterios:**

* Debe permitir acceder al módulo de flashcards.
* Debe mostrar las flashcards asociadas al usuario.
* Debe presentar la información de manera clara.
* Las flashcards deben corresponder al contenido generado para el proceso de aprendizaje vigente.
* Cuando no existan flashcards disponibles, debe mostrar un estado informativo.

**Trazabilidad:** HU-007.

### RF-009 - Realización de evaluación

El sistema debe permitir al usuario realizar la evaluación correspondiente al proceso de aprendizaje.

**Criterios:**

* Debe mostrar las preguntas de la evaluación.
* Debe permitir seleccionar una respuesta.
* Debe permitir finalizar la evaluación.
* Debe procesar las respuestas.
* Debe determinar el resultado.
* Debe mostrar el resultado obtenido.

**Consideración:** El sistema trabaja sobre una evaluación correspondiente al proceso vigente y no contempla un historial de múltiples evaluaciones consultable por el usuario.

**Trazabilidad:** HU-008.

### RF-010 - Consulta del resultado de la evaluación

El sistema debe permitir consultar el resultado correspondiente a la evaluación del proceso vigente.

**Criterios:**

* Debe mostrar la información asociada al resultado.
* Debe permitir identificar el resultado obtenido.
* El resultado debe corresponder al usuario autenticado.
* Cuando no exista un resultado disponible, debe mostrar un estado informativo.

**Trazabilidad:** HU-009.

### RF-011 - Consulta del progreso de aprendizaje

El sistema debe permitir consultar la información de progreso disponible para el usuario.

**Criterios:**

* Debe disponer de una sección destinada al progreso.
* La información debe corresponder al usuario autenticado.
* Debe utilizar la información registrada durante las actividades implementadas.
* Debe presentar la información de manera comprensible.
* Cuando no existan datos suficientes, debe mostrar un estado informativo.

**Trazabilidad:** HU-010.

### RF-012 - Asociación de información al usuario

El sistema debe asociar la información generada durante el proceso de aprendizaje con el usuario autenticado.

**Criterios:**

* Los recursos generados deben asociarse al usuario correspondiente.
* La información de evaluación debe asociarse al usuario correspondiente.
* La información de progreso debe asociarse al usuario correspondiente.
* Las consultas deben respetar la asociación con el usuario autenticado.
* Un usuario no debe acceder a información privada perteneciente a otro usuario.

**Trazabilidad:** HU-011.

### RF-013 - Gestión del perfil de usuario

El sistema debe permitir al usuario autenticado consultar y actualizar la información disponible de su perfil.

**Criterios:**

* Debe permitir acceder al perfil.
* Debe mostrar la información asociada a la cuenta.
* Debe permitir modificar los datos habilitados.
* Debe validar la información antes de guardarla.
* Debe almacenar los cambios correctamente.

**Trazabilidad:** HU-012.

### RF-014 - Cierre de sesión

El sistema debe permitir al usuario autenticado cerrar su sesión.

**Criterios:**

* Debe proporcionar una opción para cerrar sesión.
* Debe finalizar el acceso autenticado.
* Debe redirigir a la vista de acceso definida.
* No debe permitir acceder directamente a funcionalidades protegidas después del cierre de sesión sin una nueva autenticación.

**Trazabilidad:** HU-013.

### RF-015 - Gestión de errores

El sistema debe gestionar los errores producidos durante las operaciones principales.

**Criterios:**

* Debe mostrar mensajes comprensibles.
* No debe exponer información técnica o sensible.
* Debe informar cuando una operación no pueda completarse.
* Cuando sea posible, debe orientar al usuario sobre cómo continuar.
* Debe gestionar de manera controlada los errores provenientes de servicios externos.

**Trazabilidad:** HU-014.

### RF-016 - Experiencia de aprendizaje integrada

El sistema debe permitir utilizar las diferentes funcionalidades como parte de un proceso integrado de aprendizaje.

**Criterios:**

* Debe permitir acceder al asistente de IA.
* Debe permitir utilizar los recursos de aprendizaje disponibles.
* Debe permitir realizar la evaluación correspondiente.
* Debe permitir consultar el resultado.
* Debe permitir consultar la información de progreso disponible.
* Las funcionalidades deben conservar la asociación con el usuario autenticado.

**Trazabilidad:** HU-015.

## 3.2 Requisitos no funcionales

### RNF-001 - Seguridad de autenticación

El sistema debe proteger los mecanismos utilizados para autenticar a los usuarios.

* Las contraseñas no deben almacenarse en texto plano.
* Las funcionalidades protegidas deben requerir autenticación.
* Las solicitudes a recursos protegidos deben utilizar mecanismos de autorización.
* La información de autenticación no debe exponerse innecesariamente.

### RNF-002 - Protección de información por usuario

El sistema debe garantizar la separación de la información perteneciente a diferentes usuarios.

* La información debe mantenerse asociada al usuario correspondiente.
* Las operaciones protegidas deben validar la identidad del usuario.
* Un usuario no debe recibir información privada de otro usuario.

### RNF-003 - Usabilidad

La aplicación debe permitir que un usuario pueda comprender y utilizar las funcionalidades principales sin conocimientos técnicos especializados.

* Los elementos de navegación deben ser comprensibles.
* Los formularios deben indicar los datos requeridos.
* Los mensajes deben ser claros.
* Las acciones principales deben ser identificables.
* La información debe presentarse de manera organizada.

### RNF-004 - Rendimiento

La aplicación debe proporcionar tiempos de respuesta adecuados para las operaciones realizadas por el usuario.

* La interfaz debe evitar bloqueos innecesarios durante las solicitudes.
* Deben mostrarse estados de carga cuando una operación pueda tardar.
* Las operaciones que dependan de servicios externos deben gestionar adecuadamente los tiempos de respuesta.

### RNF-005 - Disponibilidad

Los servicios necesarios para el funcionamiento de Mentor-IA deben mantenerse disponibles dentro del entorno configurado.

* El Frontend debe estar disponible.
* El Backend debe estar disponible para atender las solicitudes.
* Las fallas de servicios deben gestionarse de manera controlada.

### RNF-006 - Compatibilidad web

La aplicación debe funcionar mediante navegadores web compatibles con las tecnologías utilizadas.

* La interfaz debe cargarse correctamente.
* Las funcionalidades principales deben poder utilizarse desde el navegador.
* Los elementos visuales no deben impedir el uso de las funcionalidades principales.

### RNF-007 - Mantenibilidad

La solución debe mantener una estructura organizada que facilite su mantenimiento.

* El código debe conservar una separación clara de responsabilidades.
* Las dependencias deben encontrarse declaradas.
* La configuración de los diferentes entornos debe mantenerse diferenciada.
* La estructura del proyecto debe facilitar la incorporación de modificaciones.

### RNF-008 - Escalabilidad

La arquitectura debe permitir incorporar nuevas funcionalidades sin modificar completamente la estructura de la solución.

* Deben poder incorporarse nuevos recursos de aprendizaje.
* Deben poder agregarse nuevas funcionalidades mediante los componentes y servicios existentes.
* La separación de responsabilidades debe facilitar la evolución del sistema.

### RNF-009 - Integridad de la información

El sistema debe mantener la consistencia de la información almacenada.

* Los registros deben conservar las relaciones definidas.
* La información debe mantenerse asociada al usuario correspondiente.
* Las operaciones de almacenamiento deben validar la información recibida.
* Los errores de persistencia deben gestionarse de manera controlada.

### RNF-010 - Configuración por entorno

La aplicación debe permitir gestionar configuraciones dependientes del entorno sin incorporarlas directamente al código fuente.

* Las configuraciones sensibles no deben exponerse directamente.
* Las direcciones de los servicios deben poder configurarse según el entorno.
* Las configuraciones de desarrollo y producción deben mantenerse diferenciadas.

### RNF-011 - Interoperabilidad

El Frontend, Backend, base de datos y servicios externos deben comunicarse utilizando mecanismos compatibles con la arquitectura definida.

* El Frontend debe consumir los servicios disponibles del Backend.
* Las solicitudes y respuestas deben utilizar los formatos definidos.
* Los servicios externos deben integrarse mediante los mecanismos configurados.
* Los errores de comunicación deben gestionarse adecuadamente.

### RNF-012 - Privacidad

El sistema debe limitar la exposición de la información personal y académica del usuario.

* La información privada no debe mostrarse a otros usuarios.
* Los datos deben utilizarse para las funcionalidades correspondientes.
* Los mensajes de error no deben revelar información sensible.

### RNF-013 - Recuperación ante errores

La aplicación debe mantener un comportamiento controlado cuando una operación presente una falla.

* Los errores no deben provocar el cierre inesperado de la aplicación.
* El usuario debe recibir información sobre el estado de la operación.
* Las operaciones fallidas no deben generar información inconsistente.

### RNF-014 - Arquitectura y separación de responsabilidades

La solución debe mantener una arquitectura organizada y separada por responsabilidades.

* El Backend debe mantener separadas sus capas y responsabilidades.
* Los servicios de inteligencia artificial deben mantenerse diferenciados de los controladores.
* La persistencia debe gestionarse mediante la infraestructura definida.
* El Frontend debe mantener organizados sus componentes, servicios y configuraciones.

### RNF-015 - Trazabilidad

Los requisitos deben poder relacionarse con los demás artefactos del proyecto.

* Cada requisito funcional debe estar relacionado con una o más historias de usuario.
* Los requisitos deben poder relacionarse con casos de uso.
* Los requisitos deben poder verificarse mediante casos de prueba.
* Los cambios en los requisitos deben reflejarse en los artefactos relacionados.

## 3.3 Requisitos de interfaz

### 3.3.1 Interfaz de usuario

Mentor-IA debe proporcionar una interfaz web que permita al usuario interactuar con las funcionalidades definidas.

La interfaz debe contemplar como mínimo:

* Pantalla de registro.
* Pantalla de inicio de sesión.
* Menú principal.
* Panel principal.
* Interfaz de interacción con el asistente de IA.
* Sección de recursos de aprendizaje.
* Sección de flashcards.
* Sección de evaluación.
* Sección de resultados.
* Sección de progreso.
* Sección de perfil.
* Opción de cierre de sesión.

La interfaz debe utilizar elementos visuales coherentes y presentar mensajes de validación, estados de carga y mensajes de error cuando corresponda.

### 3.3.2 Interfaz entre Frontend y Backend

El Frontend debe comunicarse con el Backend mediante los servicios API definidos por la aplicación.

La interfaz debe permitir:

* Enviar solicitudes al Backend.
* Recibir y procesar las respuestas.
* Gestionar respuestas exitosas.
* Gestionar errores.
* Enviar la información de autorización requerida para recursos protegidos.
* Utilizar la dirección del Backend correspondiente al entorno configurado.

### 3.3.3 Interfaz con el servicio de inteligencia artificial

El Backend debe comunicarse con el servicio de inteligencia artificial configurado mediante OpenRouter.

Esta integración debe permitir:

* Enviar la consulta procesada por el usuario.
* Utilizar el modelo configurado para la generación de respuestas.
* Recibir la respuesta del servicio.
* Procesar la respuesta antes de entregarla al Frontend.
* Gestionar respuestas no exitosas o errores del servicio.

La disponibilidad de esta interfaz depende del servicio externo utilizado.

### 3.3.4 Interfaz con la base de datos

El Backend debe comunicarse con PostgreSQL mediante la infraestructura definida para la aplicación.

Esta interfaz debe permitir:

* Crear registros.
* Consultar información.
* Actualizar información.
* Mantener las relaciones entre los registros.
* Asociar la información con el usuario correspondiente.
* Gestionar errores producidos durante las operaciones de persistencia.

## 3.4 Requisitos de seguridad

### 3.4.1 Autenticación

El sistema debe autenticar al usuario antes de permitir el acceso a las funcionalidades privadas.

* Las credenciales deben validarse antes de establecer la sesión.
* Las credenciales incorrectas deben impedir el acceso.
* El sistema debe utilizar mecanismos de autenticación basados en tokens para proteger los recursos correspondientes.

### 3.4.2 Autorización

El sistema debe verificar que el usuario autenticado tenga autorización para acceder a los recursos protegidos.

* Las solicitudes protegidas deben incluir la información de autorización requerida.
* Los recursos privados no deben estar disponibles para usuarios no autenticados.
* Las solicitudes no autorizadas deben ser rechazadas o gestionadas mediante el mecanismo correspondiente.

### 3.4.3 Protección de información por usuario

La información asociada a un usuario debe permanecer restringida a dicho usuario.

* Los recursos de aprendizaje deben estar asociados a su propietario.
* Los resultados deben estar asociados al usuario correspondiente.
* La información de progreso debe estar asociada al usuario correspondiente.
* Las consultas no deben permitir acceder a información de otros usuarios.

### 3.4.4 Protección de credenciales

Las credenciales deben manejarse mediante mecanismos seguros.

* Las contraseñas no deben almacenarse en texto plano.
* La aplicación debe utilizar mecanismos de protección de contraseñas.
* Las credenciales no deben exponerse en mensajes de error.
* Las credenciales y secretos de integración no deben incorporarse directamente al código fuente.

### 3.4.5 Gestión de sesión

El sistema debe controlar el acceso durante la sesión autenticada.

* Debe identificar al usuario autenticado.
* Debe permitir utilizar las funcionalidades protegidas mientras la sesión sea válida.
* Debe impedir el acceso a recursos protegidos cuando la autenticación ya no sea válida.
* Debe permitir al usuario cerrar la sesión.

### 3.4.6 Protección de información sensible

El sistema debe evitar la exposición innecesaria de información sensible.

* Los mensajes mostrados al usuario no deben contener secretos de configuración.
* Los errores técnicos no deben exponer información interna innecesaria.
* Las configuraciones sensibles deben mantenerse fuera del código fuente cuando corresponda.
* La información privada de los usuarios debe mantenerse restringida.
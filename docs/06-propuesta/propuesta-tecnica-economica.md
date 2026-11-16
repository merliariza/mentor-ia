# Propuesta Técnica y Económica - Mentor-IA

## 1. Descripción del proyecto

Mentor-IA es una plataforma web orientada al acompañamiento de los procesos de aprendizaje mediante el uso de inteligencia artificial. Su propósito es ofrecer a los estudiantes un entorno digital en el que puedan consultar temas académicos, recibir orientación de un asistente inteligente y reforzar sus conocimientos mediante recursos de aprendizaje y evaluaciones.

La plataforma integra funcionalidades de autenticación de usuarios, interacción con un asistente de inteligencia artificial, generación y consulta de recursos educativos, realización de cuestionarios, visualización de resultados y seguimiento del progreso de aprendizaje.

El sistema está diseñado para asociar la información académica a cada usuario autenticado, permitiendo consultar sus recursos y resultados dentro de un entorno personalizado.

## 2. Solución propuesta

Se propone el desarrollo de una aplicación web con arquitectura cliente-servidor, compuesta por un frontend encargado de la interacción con el usuario y un backend responsable de la lógica de negocio, la gestión de datos y la comunicación con los servicios externos.

La solución contempla los siguientes módulos funcionales:

- **Gestión de usuarios:** registro, inicio y cierre de sesión, consulta y actualización del perfil.
- **Panel principal:** visualización de información general y accesos a las funcionalidades de la plataforma.
- **Asistente de inteligencia artificial:** permite realizar consultas académicas y recibir respuestas generadas mediante la integración con un servicio de IA.
- **Recursos de aprendizaje:** generación y consulta de cuestionarios y tarjetas de estudio (flashcards) relacionados con los temas trabajados.
- **Evaluaciones:** presentación de cuestionarios, registro de respuestas, cálculo de resultados y consulta de evaluaciones.
- **Seguimiento del aprendizaje:** consulta del progreso asociado a los temas y actividades realizadas por cada usuario.

La comunicación entre el frontend y el backend se realiza mediante servicios HTTP. El backend gestiona las solicitudes, consulta y almacena la información en la base de datos y, cuando corresponde, se comunica con el servicio de inteligencia artificial OpenRouter.

La solución incorpora autenticación mediante tokens JWT y mecanismos de asociación de información al usuario para controlar el acceso a los datos correspondientes a cada cuenta.

## 3. Tecnologías

Para la implementación de Mentor-IA se utilizaron las siguientes tecnologías:

| Tecnología | Aplicación dentro del proyecto |
|---|---|
| Angular 20 | Desarrollo de la interfaz de usuario y navegación de la aplicación web. |
| TypeScript | Lenguaje utilizado en el desarrollo del frontend. |
| ASP.NET Core (.NET 9) | Desarrollo de la API y lógica del backend. |
| C# | Lenguaje de programación del backend. |
| PostgreSQL | Sistema de gestión de bases de datos. |
| Entity Framework Core | Acceso y persistencia de información en la base de datos. |
| JWT | Autenticación y control de acceso mediante tokens. |
| BCrypt | Protección de contraseñas mediante funciones de hash. |
| OpenRouter | Integración con el servicio de inteligencia artificial. |
| Docker | Configuración y preparación del entorno de ejecución del backend. |
| Render | Plataforma utilizada para el despliegue de la aplicación. |

## 4. Recursos necesarios

El desarrollo y funcionamiento de Mentor-IA contempla recursos tecnológicos, herramientas de software, servicios externos y recurso humano.

**Recursos tecnológicos y de software:**

- Equipo portátil para las actividades de desarrollo, configuración, pruebas y administración de la plataforma.
- Monitor y periféricos para facilitar las actividades de trabajo.
- Licencias de software contempladas en el presupuesto.
- Servicios de alojamiento, infraestructura en la nube y dominio.
- Acceso al servicio de inteligencia artificial mediante API.
- Herramientas de conectividad y servicios digitales necesarios para la operación.

**Recurso humano:**

El modelo contempla una persona responsable de la dirección y ejecución de la iniciativa, con funciones de desarrollo backend y frontend, integración de servicios de inteligencia artificial, pruebas funcionales, administración de la plataforma, atención básica al usuario y seguimiento de las actividades del proyecto.

## 5. Tiempo estimado de desarrollo

El plan operativo del proyecto contempla la ejecución de los recursos presupuestados durante un periodo máximo de nueve meses, distribuidos de acuerdo con las actividades y necesidades de implementación.

Este periodo incluye la adquisición de equipos, licencias, servicios tecnológicos, actividades de formalización y promoción, así como la asignación de recursos para el desarrollo y operación de la iniciativa.

El cronograma de ejecución presupuestal se encuentra organizado por meses en el plan operativo del modelo financiero. Este periodo corresponde a la planeación de ejecución de recursos y no representa una medición independiente del tiempo efectivo dedicado exclusivamente al desarrollo de software.

## 6. Costos

El modelo financiero contempla una inversión inicial de **$28.300.000 COP**, distribuida entre inversiones fijas y gastos preoperativos.

| Recurso | Cantidad | Costo |
|---|---:|---:|
| Portátil gama media-alta | 1 | $4.000.000 |
| Monitor ergonómico | 1 | $700.000 |
| Kit de periféricos | 1 | $200.000 |
| Licencias de software | 1 | $600.000 |
| Estrategia de comunicación | 1 | $3.800.000 |
| Constitución de la empresa | 1 | $1.200.000 |
| Registro de marca Mentor-IA | 1 | $1.500.000 |
| Desarrollo de identidad de marca y UI Kit | 1 | $3.500.000 |
| Lanzamiento digital | 1 | $8.000.000 |
| Suscripción inicial a APIs de IA | 1 | $4.000.000 |
| Dominios .com / .co y certificados SSL | 1 | $800.000 |
| **Total de inversiones fijas** | | **$5.500.000** |
| **Total de gastos preoperativos** | | **$22.800.000** |
| **Inversión total proyectada** | | **$28.300.000** |

Los valores corresponden a la distribución presupuestal registrada en el modelo financiero de la iniciativa. Se trata de recursos proyectados para la puesta en marcha y operación inicial del proyecto.

Adicionalmente, el modelo contempla costos de operación y administración, entre ellos conectividad, telefonía, herramientas SaaS, plataformas de analítica y marketing, creación de contenido y capacitación. Estos conceptos se encuentran discriminados en las proyecciones financieras del primer año.

## 7. Viabilidad técnica

La viabilidad técnica de Mentor-IA se sustenta en la implementación de una arquitectura basada en tecnologías utilizadas para el desarrollo de aplicaciones web y servicios backend.

La separación entre frontend y backend permite organizar las responsabilidades del sistema y facilita la comunicación entre los módulos de la aplicación. Angular 20 permite construir la interfaz de usuario, mientras que ASP.NET Core con .NET 9 proporciona los servicios encargados de procesar las solicitudes y administrar la lógica de negocio.

La utilización de PostgreSQL y Entity Framework Core permite gestionar la persistencia de la información y las relaciones entre usuarios, registros de progreso, sesiones de evaluación y recursos de aprendizaje.

La integración con OpenRouter permite incorporar capacidades de inteligencia artificial a través del backend, manteniendo centralizada la comunicación con el servicio externo. Por su parte, la autenticación mediante JWT y el uso de BCrypt contribuyen a la protección de las cuentas y al control de acceso.

El proyecto cuenta con una arquitectura definida y una solución funcional que contempla los módulos principales establecidos en las historias de usuario. Por lo anterior, las tecnologías seleccionadas permiten implementar los requerimientos funcionales y mantener una estructura organizada para la evolución de la plataforma.

## 8. Viabilidad económica

La viabilidad económica se analiza a partir del presupuesto de inversión inicial, los costos de operación y las proyecciones de ingresos registradas en el modelo financiero de Mentor-IA.

La inversión inicial proyectada asciende a $28.300.000 COP, destinada principalmente a la adquisición de equipos, licencias, formalización de la iniciativa, posicionamiento de marca, lanzamiento digital y servicios de inteligencia artificial.

El modelo financiero establece un precio de venta proyectado de $19.900 por unidad y un costo variable de $2.000, con un margen de contribución de $17.900. A partir de estos valores, se calcula un punto de equilibrio aproximado de 25 unidades.

En cuanto a las proyecciones de ventas, el modelo contempla los siguientes valores:

| Periodo | Ventas proyectadas |
|---|---:|
| Año 1 | $21.492.000 |
| Año 2 | $44.655.600 |
| Año 3 | $99.293.040 |
| Año 4 | $178.982.796,96 |
| Año 5 | $297.811.033,34 |

Las proyecciones reflejan un crecimiento progresivo de los ingresos durante los cinco años evaluados. Sin embargo, el estado de resultados proyectado registra pérdidas netas durante los dos primeros años y resultados positivos a partir del tercer año.

Por lo tanto, la sostenibilidad económica de la iniciativa depende del cumplimiento de las metas comerciales, el control de los costos operativos y el crecimiento de la cantidad de usuarios o unidades comercializadas.

## 9. Conclusiones

Mentor-IA constituye una solución tecnológica orientada al acompañamiento del aprendizaje mediante la integración de inteligencia artificial y herramientas digitales de evaluación y seguimiento académico.

La arquitectura implementada, basada en Angular 20, ASP.NET Core (.NET 9) y PostgreSQL, proporciona una estructura organizada para el funcionamiento de los módulos principales de la plataforma. Asimismo, la integración con OpenRouter permite incorporar capacidades de inteligencia artificial dentro del flujo de interacción del usuario.

Desde el punto de vista técnico, el proyecto cuenta con los componentes necesarios para ofrecer sus funcionalidades principales y gestionar la información asociada a los usuarios. Desde el punto de vista económico, el modelo financiero establece una inversión inicial, identifica los recursos requeridos y presenta proyecciones de ingresos y costos para evaluar su desarrollo comercial.
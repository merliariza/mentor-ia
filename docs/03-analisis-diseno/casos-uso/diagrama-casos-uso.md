# Diagrama de Casos de Uso - Mentor-IA

## Actores

### Actor principal: Visitante

Persona que accede a Mentor-IA sin haber iniciado sesión.

Puede interactuar con las funcionalidades públicas de la plataforma, principalmente el registro y el inicio de sesión.

### Actor principal: Usuario autenticado

Persona que ha creado una cuenta y ha iniciado sesión correctamente.

Puede acceder a las funcionalidades privadas de Mentor-IA relacionadas con el proceso de aprendizaje, incluyendo el asistente de inteligencia artificial, recursos de aprendizaje, evaluación, resultados, progreso y perfil.

### Actor secundario: Servicio de inteligencia artificial

Servicio externo utilizado por Mentor-IA para procesar las consultas realizadas al asistente y apoyar la generación de recursos de aprendizaje.

### Actor secundario: Base de datos

Sistema utilizado para almacenar y consultar la información persistente de Mentor-IA, incluyendo información de usuarios, recursos, evaluaciones, resultados y progreso.

## Casos de uso

| Código | Caso de uso                                | Actor principal                | Requisito relacionado |
| ------ | ------------------------------------------ | ------------------------------ | --------------------- |
| CU-001 | Registrar usuario                          | Visitante                      | RF-001                |
| CU-002 | Iniciar sesión                             | Visitante / Usuario registrado | RF-002, RF-003        |
| CU-003 | Navegar por Mentor-IA                      | Usuario autenticado            | RF-004                |
| CU-004 | Consultar panel principal                  | Usuario autenticado            | RF-005                |
| CU-005 | Realizar consulta al asistente IA          | Usuario autenticado            | RF-006                |
| CU-006 | Generar recursos de aprendizaje            | Usuario autenticado            | RF-007                |
| CU-007 | Consultar flashcards                       | Usuario autenticado            | RF-008                |
| CU-008 | Realizar evaluación                        | Usuario autenticado            | RF-009                |
| CU-009 | Consultar resultado de evaluación          | Usuario autenticado            | RF-010                |
| CU-010 | Consultar progreso de aprendizaje          | Usuario autenticado            | RF-011                |
| CU-011 | Gestionar perfil                           | Usuario autenticado            | RF-013                |
| CU-012 | Cerrar sesión                              | Usuario autenticado            | RF-014                |
| CU-013 | Gestionar errores durante la interacción   | Usuario de Mentor-IA           | RF-015                |
| CU-014 | Gestionar proceso de aprendizaje integrado | Usuario autenticado            | RF-016                |

## Relaciones principales

* El **Visitante** puede registrar una cuenta mediante `CU-001`.
* El **Visitante / Usuario registrado** puede iniciar sesión mediante `CU-002`.
* El **Usuario autenticado** puede acceder a las funcionalidades privadas de Mentor-IA.
* El **Usuario autenticado** puede navegar por la plataforma mediante `CU-003`.
* El **Usuario autenticado** puede consultar el panel principal mediante `CU-004`.
* El **Usuario autenticado** puede realizar consultas al asistente mediante `CU-005`.
* El **Servicio de inteligencia artificial** participa en `CU-005` y `CU-006`.
* El **Usuario autenticado** puede generar recursos de aprendizaje mediante `CU-006`.
* El **Usuario autenticado** puede consultar flashcards mediante `CU-007`.
* El **Usuario autenticado** puede realizar la evaluación vigente mediante `CU-008`.
* El **Usuario autenticado** puede consultar el resultado de la evaluación mediante `CU-009`.
* El **Usuario autenticado** puede consultar su progreso mediante `CU-010`.
* El **Usuario autenticado** puede gestionar su perfil mediante `CU-011`.
* El **Usuario autenticado** puede cerrar sesión mediante `CU-012`.
* La **Base de datos** participa en las operaciones que requieren persistencia o consulta de información.
* `CU-014` representa la utilización integrada de las funcionalidades que conforman el proceso de aprendizaje.

## Diagrama

## Descripción general

El diagrama de casos de uso representa las principales interacciones entre los actores y Mentor-IA.

El visitante interactúa principalmente con las funcionalidades relacionadas con el acceso a la plataforma, mientras que el usuario autenticado puede utilizar las funcionalidades correspondientes al proceso de aprendizaje.

El asistente de inteligencia artificial participa como servicio externo en las operaciones que requieren procesamiento mediante IA.

La base de datos interviene en las operaciones que requieren almacenar o consultar información persistente.

El proceso general de aprendizaje contempla la interacción con el asistente de IA, la generación y consulta de recursos, la realización de la evaluación correspondiente, la consulta del resultado y la visualización del progreso.

Mentor-IA no contempla un historial de conversaciones consultable por el usuario. De igual manera, la versión actual trabaja con la evaluación correspondiente al proceso vigente y no contempla un historial independiente de múltiples evaluaciones.

## Trazabilidad

Los casos de uso definidos se derivan de las Historias de Usuario y Requisitos Funcionales establecidos en la Especificación de Requisitos de Software de Mentor-IA.

La relación entre estos artefactos permite mantener trazabilidad desde la necesidad del usuario hasta el comportamiento esperado del sistema y posteriormente hacia los casos de prueba.
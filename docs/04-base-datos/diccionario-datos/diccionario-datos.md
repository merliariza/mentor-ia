# Diccionario de Datos - Mentor-IA

El diccionario de datos describe los campos que componen las tablas de la base de datos de Mentor-IA, indicando sus tipos de datos, restricciones de nulabilidad, claves primarias y claves foráneas. Su finalidad es facilitar la comprensión de la información almacenada y su organización.

## Tabla: UserMember

Almacena la información de los usuarios registrados en la plataforma.

| Campo | Tipo | PK | FK | Nulo | Descripción |
|---|---|---|---|---|---|
| Id | integer | Sí | No | No | Identificador único del usuario. |
| FullName | text | No | No | No | Nombre completo del usuario. |
| Email | text | No | No | No | Correo electrónico del usuario. |
| PasswordHash | text | No | No | No | Hash de la contraseña utilizado para la autenticación. |

## Tabla: Progress

Registra el seguimiento del aprendizaje de los usuarios por tema de estudio.

| Campo | Tipo | PK | FK | Nulo | Descripción |
|---|---|---|---|---|---|
| Id | integer | Sí | No | No | Identificador único del registro de progreso. |
| Score | integer | No | No | No | Puntuación asociada al progreso del usuario. |
| Topic | text | No | No | No | Tema de estudio asociado al progreso. |
| Feedback | text | No | No | No | Retroalimentación relacionada con el aprendizaje. |
| UserMemberId | integer | No | Sí | No | Identificador del usuario al que pertenece el registro de progreso. |

## Tabla: EvaluationSession

Almacena los resultados de las sesiones de evaluación realizadas por los usuarios.

| Campo | Tipo | PK | FK | Nulo | Descripción |
|---|---|---|---|---|---|
| Id | integer | Sí | No | No | Identificador único de la sesión de evaluación. |
| Score | integer | No | No | No | Puntuación obtenida en la evaluación. |
| Feedback | text | No | No | No | Retroalimentación correspondiente al resultado de la evaluación. |
| ProgressId | integer | No | Sí | No | Identificador del registro de progreso relacionado con la evaluación. |

## Tabla: Flashcard

Almacena los recursos de repaso asociados a las sesiones de evaluación.

| Campo | Tipo | PK | FK | Nulo | Descripción |
|---|---|---|---|---|---|
| Id | integer | Sí | No | No | Identificador único de la flashcard. |
| Question | text | No | No | No | Pregunta utilizada como recurso de repaso. |
| Answer | text | No | No | No | Respuesta asociada a la pregunta. |
| EvaluationSessionId | integer | No | Sí | No | Identificador de la sesión de evaluación a la que pertenece la flashcard. |

## Tabla: RefreshToken

Almacena los tokens utilizados para gestionar la renovación de las sesiones de autenticación.

| Campo | Tipo | PK | FK | Nulo | Descripción |
|---|---|---|---|---|---|
| Id | integer | Sí | No | No | Identificador único del registro del token. |
| MemberId | integer | No | No | No | Identificador adicional del miembro asociado al registro. |
| UserId | integer | No | Sí | No | Identificador del usuario propietario del token, relacionado con UserMember.Id. |
| Token | text | No | No | No | Valor del token de renovación. |
| Expires | timestamp with time zone | No | No | No | Fecha y hora de expiración del token. |
| Created | timestamp with time zone | No | No | No | Fecha y hora de creación del token. |
| Revoked | timestamp with time zone | No | No | Sí | Fecha y hora de revocación del token, cuando aplica. |
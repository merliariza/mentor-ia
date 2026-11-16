# Modelo Relacional - Mentor-IA

## 1. Tablas

El modelo relacional de Mentor-IA está conformado por las siguientes tablas, encargadas de almacenar y organizar la información necesaria para el funcionamiento de la plataforma.

| Tabla | Descripción |
|---|---|
| UserMember | Almacena la información de los usuarios registrados en la plataforma. |
| Progress | Registra el progreso de aprendizaje de los usuarios por tema de estudio. |
| EvaluationSession | Almacena los resultados y la retroalimentación de las sesiones de evaluación. |
| Flashcard | Almacena las preguntas y respuestas de los recursos de repaso asociados a las evaluaciones. |
| RefreshToken | Registra los tokens utilizados para la renovación de las sesiones de autenticación. |

## 2. Relaciones

Las tablas se relacionan mediante asociaciones de uno a muchos (1:N), utilizando claves foráneas para establecer la integridad referencial:

- **UserMember – Progress:** un usuario puede tener varios registros de progreso, pero cada registro pertenece a un único usuario.
- **Progress – EvaluationSession:** un registro de progreso puede estar relacionado con varias sesiones de evaluación, pero cada sesión pertenece a un único registro de progreso.
- **EvaluationSession – Flashcard:** una sesión de evaluación puede tener varias flashcards asociadas, pero cada flashcard pertenece a una única sesión.
- **UserMember – RefreshToken:** un usuario puede tener varios registros de tokens de renovación, pero cada registro está relacionado con un único usuario mediante el campo `UserId`.

## 3. Claves primarias

Cada tabla cuenta con una clave primaria denominada `Id`, que permite identificar de manera única cada registro. En la implementación, este campo se hereda de la clase `BaseEntity`.

| Tabla | Clave primaria |
|---|---|
| UserMember | Id |
| Progress | Id |
| EvaluationSession | Id |
| Flashcard | Id |
| RefreshToken | Id |

## 4. Claves foráneas

Las claves foráneas permiten establecer las relaciones entre las tablas y mantener la integridad referencial de la información almacenada.

| Tabla | Clave foránea | Tabla relacionada | Campo relacionado |
|---|---|---|---|
| Progress | UserMemberId | UserMember | Id |
| EvaluationSession | ProgressId | Progress | Id |
| Flashcard | EvaluationSessionId | EvaluationSession | Id |
| RefreshToken | UserId | UserMember | Id |
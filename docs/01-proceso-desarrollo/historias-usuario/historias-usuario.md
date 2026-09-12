# Historias de Usuario - Mentor-IA

## HU-001 - Registro de usuario

**Como:** visitante

**Quiero:** crear una cuenta en Mentor-IA

**Para:** acceder a las funcionalidades personalizadas de la plataforma y contar con un espacio de aprendizaje asociado a mis datos.

#### Criterios de aceptación

* El sistema debe permitir acceder al formulario de registro.
* El formulario debe solicitar los datos definidos como obligatorios para crear la cuenta.
* El sistema debe validar que los campos obligatorios hayan sido diligenciados.
* El sistema debe validar el formato de los datos que lo requieran.
* El sistema debe validar que el correo electrónico no se encuentre asociado a otra cuenta.
* Si la información es válida, el sistema debe crear la cuenta del usuario.
* Una vez completado el registro, el sistema debe permitir al usuario acceder mediante sus credenciales.

#### Prioridad

Alta

#### Estimación

5 puntos

#### Estado

Completada

## HU-002 - Inicio de sesión

**Como:** usuario registrado

**Quiero:** iniciar sesión en Mentor-IA

**Para:** acceder de forma segura a las funcionalidades asociadas a mi cuenta.

#### Criterios de aceptación

* El sistema debe permitir ingresar las credenciales de acceso.
* El sistema debe validar las credenciales proporcionadas.
* El sistema debe permitir el acceso cuando las credenciales sean correctas.
* El sistema debe informar cuando las credenciales ingresadas no sean válidas.
* El sistema debe establecer la sesión correspondiente al usuario autenticado.
* La información y funcionalidades privadas deben estar disponibles únicamente para usuarios autenticados.

#### Prioridad

Alta

#### Estimación

5 puntos

#### Estado

Completada

## HU-003 - Visualización del menú principal

**Como:** usuario autenticado

**Quiero:** visualizar un menú con las opciones disponibles de Mentor-IA

**Para:** acceder de manera organizada a las diferentes funcionalidades de la plataforma.

#### Criterios de aceptación

* El sistema debe mostrar el menú de navegación al usuario autenticado.
* El menú debe presentar las opciones correspondientes a las funcionalidades disponibles.
* El usuario debe poder seleccionar una opción del menú.
* Cada opción debe dirigir a la sección correspondiente.
* Las opciones de navegación deben mantenerse disponibles durante la navegación de acuerdo con el diseño establecido.
* El menú no debe mostrar opciones que requieran autenticación a usuarios no autenticados.

#### Prioridad

Alta

#### Estimación

3 puntos

#### Estado

Completada

## HU-004 - Acceso al panel principal

**Como:** usuario autenticado

**Quiero:** visualizar un panel principal con información y accesos relevantes

**Para:** tener una vista general de mi espacio de aprendizaje y acceder rápidamente a las funcionalidades de Mentor-IA.

#### Criterios de aceptación

* El sistema debe mostrar el panel principal después del inicio de sesión.
* El panel debe presentar los accesos definidos para las funcionalidades principales.
* La información mostrada debe corresponder al usuario autenticado.
* El usuario debe poder navegar desde el panel hacia las funcionalidades disponibles.
* El sistema debe mantener la sesión del usuario durante la navegación.

#### Prioridad

Alta

#### Estimación

3 puntos

#### Estado

Completada

## HU-005 - Interacción con el asistente de inteligencia artificial

**Como:** usuario autenticado

**Quiero:** realizar preguntas mediante el chat de Mentor-IA

**Para:** recibir apoyo y orientación relacionada con mis necesidades de aprendizaje.

#### Criterios de aceptación

* El sistema debe proporcionar un espacio para ingresar una pregunta.
* El usuario debe poder enviar su consulta al asistente.
* El sistema debe procesar la consulta mediante el servicio de inteligencia artificial configurado.
* El sistema debe mostrar la respuesta generada por el asistente.
* La respuesta debe corresponder a la consulta realizada.
* El sistema debe permitir realizar una nueva consulta.
* Cuando se produzca un error durante el procesamiento, el sistema debe informar al usuario que no fue posible obtener la respuesta.

#### Prioridad

Alta

#### Estimación

8 puntos

#### Estado

Completada

## HU-006 - Generación de recursos de aprendizaje

**Como:** usuario autenticado

**Quiero:** generar recursos de aprendizaje a partir del contenido trabajado

**Para:** disponer de materiales que me permitan reforzar los conocimientos adquiridos.

#### Criterios de aceptación

* El sistema debe permitir generar recursos de aprendizaje a partir del proceso de estudio.
* El sistema debe generar cuestionarios cuando corresponda.
* El sistema debe generar tarjetas de estudio (flashcards) cuando corresponda.
* Los recursos generados deben estar relacionados con el contenido académico trabajado.
* Los recursos deben quedar asociados al usuario correspondiente.
* Los recursos generados deben poder ser consultados desde los módulos destinados para ellos.

#### Prioridad

Alta

#### Estimación

8 puntos

#### Estado

Completada

## HU-007 - Consulta de flashcards

**Como:** usuario autenticado

**Quiero:** consultar las flashcards generadas para mi proceso de aprendizaje

**Para:** repasar conceptos y reforzar los contenidos estudiados.

#### Criterios de aceptación

* El sistema debe permitir acceder al módulo de flashcards.
* El sistema debe mostrar las flashcards disponibles para el usuario.
* Las flashcards deben presentar la información del último tema de estudio de manera clara.
* El usuario debe poder consultar el contenido de las tarjetas.
* Las flashcards mostradas deben corresponder a los recursos asociados al usuario.
* Cuando no existan flashcards disponibles, el sistema debe mostrar un estado informativo.

#### Prioridad

Alta

#### Estimación

5 puntos

#### Estado

Completada

## HU-008 - Realización de cuestionarios

**Como:** usuario autenticado

**Quiero:** realizar los cuestionarios generados por Mentor-IA

**Para:** comprobar mi comprensión de los contenidos trabajados.

#### Criterios de aceptación

* El sistema debe permitir acceder a los cuestionarios disponibles.
* El sistema debe mostrar las preguntas correspondientes al cuestionario.
* El usuario debe poder seleccionar una respuesta para cada pregunta.
* El sistema debe permitir finalizar el cuestionario.
* El sistema debe procesar las respuestas seleccionadas.
* El sistema debe determinar el resultado de acuerdo con las respuestas registradas.
* El sistema debe mostrar el resultado obtenido al finalizar.

#### Prioridad

Alta

#### Estimación

8 puntos

#### Estado

Completada

## HU-009 - Consulta de resultados

**Como:** usuario autenticado

**Quiero:** consultar los resultados de los cuestionarios realizados

**Para:** conocer mi desempeño en las actividades de evaluación.

#### Criterios de aceptación

* El sistema debe mostrar la información asociada al resultado de la evaluación.
* El usuario debe poder identificar el resultado obtenido en su última evaluación.
* Cuando el usuario no tenga resultados registrados, el sistema debe mostrar un estado informativo.

#### Prioridad

Media

#### Estimación

5 puntos

#### Estado

Completada

## HU-010 - Consulta del progreso de aprendizaje

**Como:** usuario autenticado

**Quiero:** consultar mi progreso dentro de Mentor-IA

**Para:** hacer seguimiento a mi desempeño durante el proceso de aprendizaje.

#### Criterios de aceptación

* El sistema debe disponer de una sección para consultar el progreso.
* La información presentada debe corresponder al usuario autenticado.
* El sistema debe mostrar los datos de progreso disponibles.
* El progreso debe considerar la información registrada durante las actividades de aprendizaje implementadas.
* La información debe presentarse de forma clara y comprensible.
* Cuando no existan datos suficientes para mostrar progreso, el sistema debe presentar un estado informativo.

#### Prioridad

Alta

#### Estimación

5 puntos

#### Estado

Completada

## HU-011 - Asociación de información al usuario

**Como:** usuario autenticado

**Quiero:** que la información generada durante mi proceso de aprendizaje quede asociada a mi cuenta

**Para:** mantener separados mis datos, resultados y recursos de los correspondientes a otros usuarios.

#### Criterios de aceptación

* El sistema debe identificar al usuario autenticado durante las operaciones que requieran asociación de información.
* La información registrada debe quedar relacionada con el usuario correspondiente.
* Los recursos de aprendizaje generados deben asociarse al usuario que los genera.
* Los resultados de las evaluaciones deben asociarse al usuario correspondiente.
* El usuario no debe consultar información privada perteneciente a otro usuario.
* La información debe conservar su relación con el usuario durante las consultas posteriores.

#### Prioridad

Alta

#### Estimación

8 puntos

#### Estado

Completada

## HU-012 - Consulta y actualización del perfil

**Como:** usuario autenticado

**Quiero:** consultar y actualizar la información disponible de mi perfil

**Para:** mantener mis datos personales actualizados dentro de la plataforma.

#### Criterios de aceptación

* El sistema debe permitir acceder al perfil del usuario.
* El sistema debe mostrar la información asociada a la cuenta.
* El usuario debe poder modificar los datos que se encuentren habilitados para edición.
* El sistema debe validar la información antes de guardar los cambios.
* El sistema debe guardar correctamente la información actualizada.
* Los cambios deben reflejarse al consultar nuevamente el perfil.

#### Prioridad

Media

#### Estimación

5 puntos

#### Estado

Completada

## HU-013 - Cierre de sesión

**Como:** usuario autenticado

**Quiero:** cerrar mi sesión

**Para:** finalizar de manera segura mi acceso a Mentor-IA.

#### Criterios de aceptación

* El sistema debe disponer de una opción para cerrar sesión.
* El usuario debe poder ejecutar la opción de cierre de sesión desde la interfaz correspondiente.
* Al cerrar sesión, el sistema debe finalizar el acceso autenticado.
* El sistema debe redirigir al usuario a la vista de acceso definida.
* El usuario no debe poder acceder directamente a funcionalidades protegidas después de cerrar sesión sin volver a autenticarse.

#### Prioridad

Alta

#### Estimación

3 puntos

#### Estado

Completada

## HU-014 - Gestión de errores durante la interacción

**Como:** usuario de Mentor-IA

**Quiero:** recibir mensajes claros cuando una operación no pueda completarse

**Para:** comprender la situación y saber cómo continuar utilizando la plataforma.

#### Criterios de aceptación

* El sistema debe identificar los errores que se produzcan durante las operaciones principales.
* El sistema debe mostrar mensajes comprensibles para el usuario.
* Los mensajes no deben exponer información técnica o sensible.
* Cuando sea posible, el mensaje debe orientar sobre la acción que puede realizar el usuario.
* Los errores relacionados con servicios externos deben gestionarse sin provocar un cierre inesperado de la aplicación.
* La interfaz debe conservar un estado controlado después de un error.

#### Prioridad

Media

#### Estimación

5 puntos

#### Estado

Completada

## HU-015 - Experiencia de aprendizaje integrada

**Como:** usuario autenticado

**Quiero:** utilizar las diferentes herramientas de Mentor-IA como parte de un mismo proceso de aprendizaje

**Para:** consultar contenidos, generar recursos y evaluar mis conocimientos desde una misma plataforma.

#### Criterios de aceptación

* El usuario debe poder acceder al asistente de IA desde la plataforma.
* El usuario debe poder utilizar los recursos de aprendizaje disponibles.
* El usuario debe poder realizar los cuestionarios disponibles.
* El usuario debe poder consultar los resultados y la información de progreso disponible.
* Las funcionalidades deben conservar la asociación con el usuario autenticado.
* La navegación entre las funcionalidades debe permitir continuar utilizando la plataforma sin necesidad de autenticarse nuevamente mientras la sesión sea válida.

#### Prioridad

Alta

#### Estimación

8 puntos

#### Estado

Completada
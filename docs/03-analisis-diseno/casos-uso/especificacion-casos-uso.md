# Especificación de Casos de Uso - Mentor-IA

## CU-001 - Registrar usuario

**Código:** CU-001

**Actor principal:** Visitante

**Actores secundarios:** Base de datos

**Historia de Usuario relacionada:** HU-001 - Registro de usuario

**Requisito funcional relacionado:** RF-001 - Registro de usuario

**Objetivo:**

Permitir que un visitante cree una cuenta en Mentor-IA para acceder posteriormente a las funcionalidades personalizadas de la plataforma.

**Precondiciones:**

* El visitante debe poder acceder al formulario de registro.
* No es necesario que el visitante esté autenticado.

**Postcondiciones:**

* La cuenta queda registrada cuando la información es válida.
* La información del usuario queda almacenada.
* El usuario puede utilizar sus credenciales para iniciar sesión.

### Flujo principal

1. El visitante accede a la opción de registro.
2. El sistema muestra el formulario.
3. El visitante ingresa la información solicitada.
4. El visitante envía el formulario.
5. El sistema valida los campos requeridos.
6. El sistema valida los formatos correspondientes.
7. El sistema verifica que el correo electrónico no se encuentre registrado.
8. El sistema registra la información del usuario.
9. El sistema confirma que el registro fue realizado correctamente.

### Flujos alternativos

**FA-001 - Campos obligatorios incompletos**

1. El visitante envía el formulario.
2. El sistema identifica campos obligatorios sin diligenciar.
3. El sistema muestra el mensaje de validación correspondiente.
4. El visitante completa la información.
5. El flujo continúa con la validación.

**FA-002 - Formato inválido**

1. El sistema identifica información con formato inválido.
2. El sistema muestra el mensaje correspondiente.
3. El visitante corrige la información.
4. El flujo continúa con la validación.

**FA-003 - Correo electrónico ya registrado**

1. El sistema verifica el correo.
2. Identifica que ya existe una cuenta asociada.
3. El sistema informa la situación.
4. El visitante puede utilizar otro correo.

### Excepciones

**E-001 - Error durante el registro**

Si ocurre un error durante el almacenamiento de la información, el sistema no completa el registro y muestra un mensaje controlado.

### Reglas de negocio

* El correo electrónico debe ser único.
* Los campos obligatorios deben estar diligenciados.
* La información debe cumplir con las validaciones establecidas.

---

## CU-002 - Iniciar sesión

**Código:** CU-002

**Actor principal:** Visitante / Usuario registrado

**Actores secundarios:** Base de datos

**Historia de Usuario relacionada:** HU-002 - Inicio de sesión

**Requisito funcional relacionado:** RF-002 - Autenticación de usuarios / RF-003 - Control de acceso

**Objetivo:**

Permitir que un usuario registrado acceda de forma autenticada a las funcionalidades privadas de Mentor-IA.

**Precondiciones:**

* El usuario debe contar con una cuenta registrada.
* El usuario debe encontrarse en la pantalla de acceso.

**Postcondiciones:**

* El usuario queda autenticado.
* Se establece la sesión correspondiente.
* El usuario puede acceder a las funcionalidades protegidas.

### Flujo principal

1. El usuario accede a la pantalla de inicio de sesión.
2. El sistema muestra los campos de acceso.
3. El usuario ingresa sus credenciales.
4. El usuario envía el formulario.
5. El sistema valida las credenciales.
6. El sistema autentica al usuario.
7. El sistema establece la sesión.
8. El sistema permite el acceso a las funcionalidades privadas.
9. El sistema dirige al usuario al espacio principal.

### Flujos alternativos

**FA-001 - Credenciales inválidas**

1. El usuario ingresa credenciales incorrectas.
2. El sistema rechaza la autenticación.
3. El sistema muestra un mensaje informativo.
4. El usuario puede intentar nuevamente.

**FA-002 - Campos incompletos**

1. El usuario intenta iniciar sesión con campos requeridos vacíos.
2. El sistema muestra las validaciones correspondientes.
3. El usuario completa la información.

### Excepciones

**E-001 - Error durante la autenticación**

Si ocurre un error durante el proceso, el sistema informa que no fue posible procesar la solicitud y mantiene al usuario sin autenticar.

### Reglas de negocio

* Las credenciales deben ser válidas.
* Las funcionalidades privadas requieren autenticación.
* La información de autenticación no debe exponerse.

---

## CU-003 - Navegar por Mentor-IA

**Código:** CU-003

**Actor principal:** Usuario autenticado

**Actores secundarios:** Ninguno

**Historia de Usuario relacionada:** HU-003 - Visualización del menú principal

**Requisito funcional relacionado:** RF-004 - Navegación mediante menú principal

**Objetivo:**

Permitir al usuario acceder de forma organizada a las funcionalidades disponibles.

**Precondiciones:**

* El usuario debe estar autenticado.

**Postcondiciones:**

* El usuario accede a la sección seleccionada.
* La sesión continúa disponible mientras sea válida.

### Flujo principal

1. El usuario visualiza el menú principal.
2. El sistema muestra las opciones disponibles.
3. El usuario selecciona una opción.
4. El sistema identifica la sección solicitada.
5. El sistema dirige al usuario a la sección correspondiente.

### Flujos alternativos

**FA-001 - Intento de acceso sin autenticación**

1. Un usuario no autenticado intenta acceder a una funcionalidad protegida.
2. El sistema verifica el estado de autenticación.
3. El sistema impide el acceso.
4. El sistema dirige al usuario al mecanismo de acceso.

### Excepciones

**E-001 - Ruta no disponible**

Si la ruta solicitada no se encuentra disponible, el sistema debe manejar la situación de manera controlada.

### Reglas de negocio

* Las funcionalidades privadas requieren autenticación.
* El menú debe mostrar las opciones correspondientes al usuario autenticado.

---

## CU-004 - Consultar panel principal

**Código:** CU-004

**Actor principal:** Usuario autenticado

**Actores secundarios:** Base de datos

**Historia de Usuario relacionada:** HU-004 - Acceso al panel principal

**Requisito funcional relacionado:** RF-005 - Visualización del panel principal

**Objetivo:**

Permitir al usuario consultar el panel principal de Mentor-IA y acceder a las funcionalidades relevantes.

**Precondiciones:**

* El usuario debe haber iniciado sesión correctamente.

**Postcondiciones:**

* El panel principal se muestra correctamente.
* La información presentada corresponde al usuario autenticado.

### Flujo principal

1. El usuario inicia sesión.
2. El sistema valida la sesión.
3. El sistema carga el panel principal.
4. El sistema muestra los accesos disponibles.
5. El usuario puede seleccionar una funcionalidad.

### Flujos alternativos

**FA-001 - Información insuficiente**

1. El sistema identifica que no existe información suficiente para mostrar algún elemento.
2. El sistema presenta un estado informativo.
3. El usuario puede continuar utilizando las funcionalidades disponibles.

### Excepciones

**E-001 - Error al cargar información**

Si ocurre un error durante la consulta, el sistema muestra un mensaje controlado.

### Reglas de negocio

* La información presentada debe corresponder al usuario autenticado.

---

## CU-005 - Realizar consulta al asistente IA

**Código:** CU-005

**Actor principal:** Usuario autenticado

**Actores secundarios:** Servicio de inteligencia artificial

**Historia de Usuario relacionada:** HU-005 - Interacción con el asistente de inteligencia artificial

**Requisito funcional relacionado:** RF-006 - Interacción con el asistente de inteligencia artificial

**Objetivo:**

Permitir al usuario realizar preguntas relacionadas con su aprendizaje y recibir una respuesta generada mediante inteligencia artificial.

**Precondiciones:**

* El usuario debe estar autenticado.
* El servicio de inteligencia artificial debe estar configurado.

**Postcondiciones:**

* El usuario recibe la respuesta generada.
* El usuario puede realizar una nueva consulta.

### Flujo principal

1. El usuario accede al asistente de IA.
2. El sistema muestra el espacio de consulta.
3. El usuario ingresa una pregunta.
4. El usuario envía la consulta.
5. El Backend recibe la solicitud.
6. El Backend procesa la consulta.
7. El Backend envía la solicitud al servicio de IA.
8. El servicio procesa la consulta.
9. El servicio devuelve una respuesta.
10. El Backend procesa la respuesta.
11. El sistema muestra la respuesta al usuario.
12. El usuario puede realizar una nueva consulta.

### Flujos alternativos

**FA-001 - Nueva consulta**

1. El usuario realiza una nueva pregunta.
2. El sistema procesa la nueva solicitud.
3. El sistema muestra la respuesta correspondiente.

### Excepciones

**E-001 - Servicio de IA no disponible**

Si el servicio externo no responde correctamente, el sistema informa que no fue posible procesar la consulta.

**E-002 - Error de comunicación**

Si ocurre un error durante la comunicación, el sistema controla la respuesta y muestra un mensaje comprensible.

### Reglas de negocio

* El acceso al asistente requiere autenticación.
* Las consultas deben ser procesadas mediante el servicio de IA configurado.
* Los errores del servicio externo no deben provocar el cierre inesperado de la aplicación.
* No existe un historial de conversaciones consultable.

---

## CU-006 - Generar recursos de aprendizaje

**Código:** CU-006

**Actor principal:** Usuario autenticado

**Actores secundarios:** Servicio de inteligencia artificial / Base de datos

**Historia de Usuario relacionada:** HU-006 - Generación de recursos de aprendizaje

**Requisito funcional relacionado:** RF-007 - Generación de recursos de aprendizaje

**Objetivo:**

Permitir generar recursos de aprendizaje relacionados con el contenido trabajado.

**Precondiciones:**

* El usuario debe estar autenticado.
* Debe existir contenido suficiente para generar el recurso.
* El servicio requerido debe estar disponible.

**Postcondiciones:**

* Los recursos son generados.
* Los recursos quedan asociados al usuario correspondiente.
* Los recursos quedan disponibles para su consulta.

### Flujo principal

1. El usuario trabaja contenido dentro de Mentor-IA.
2. El sistema dispone del contenido necesario.
3. El usuario solicita la generación de recursos.
4. El sistema procesa la solicitud.
5. El sistema genera los recursos correspondientes.
6. El sistema valida la información generada.
7. El sistema asocia los recursos al usuario.
8. El sistema almacena la información.
9. El usuario puede acceder a los recursos.

### Flujos alternativos

**FA-001 - Generación de flashcards**

1. El usuario solicita flashcards.
2. El sistema procesa el contenido.
3. Se generan las flashcards.
4. El sistema las asocia al usuario.

**FA-002 - Generación de cuestionario**

1. El usuario solicita un cuestionario.
2. El sistema procesa el contenido.
3. Se genera la evaluación correspondiente.
4. El sistema deja disponible el recurso.

### Excepciones

**E-001 - Contenido insuficiente**

Si no existe información suficiente para generar el recurso, el sistema informa que no es posible completar la operación.

**E-002 - Error del servicio de IA**

Si el servicio de IA presenta un error, el sistema controla la respuesta e informa al usuario.

### Reglas de negocio

* Los recursos deben estar relacionados con el contenido trabajado.
* Los recursos generados deben asociarse al usuario correspondiente.

---

## CU-007 - Consultar flashcards

**Código:** CU-007

**Actor principal:** Usuario autenticado

**Actores secundarios:** Base de datos

**Historia de Usuario relacionada:** HU-007 - Consulta de flashcards

**Requisito funcional relacionado:** RF-008 - Consulta de flashcards

**Objetivo:**

Permitir al usuario consultar las flashcards disponibles para reforzar el contenido trabajado.

**Precondiciones:**

* El usuario debe estar autenticado.

**Postcondiciones:**

* El usuario visualiza las flashcards disponibles correspondientes a su cuenta.

### Flujo principal

1. El usuario accede al módulo de flashcards.
2. El sistema identifica al usuario autenticado.
3. El sistema consulta las flashcards asociadas.
4. El sistema obtiene la información disponible.
5. El sistema muestra las flashcards.
6. El usuario consulta su contenido.

### Flujos alternativos

**FA-001 - No existen flashcards**

1. El sistema consulta la información.
2. No encuentra flashcards disponibles.
3. El sistema muestra un estado informativo.

### Excepciones

**E-001 - Error de consulta**

Si ocurre un error durante la consulta, el sistema informa que no fue posible cargar la información.

### Reglas de negocio

* El usuario solo debe consultar las flashcards asociadas a su cuenta.

---

## CU-008 - Realizar evaluación

**Código:** CU-008

**Actor principal:** Usuario autenticado

**Actores secundarios:** Base de datos

**Historia de Usuario relacionada:** HU-008 - Realización de cuestionarios

**Requisito funcional relacionado:** RF-009 - Realización de evaluación

**Objetivo:**

Permitir al usuario realizar la evaluación correspondiente al proceso de aprendizaje y obtener un resultado.

**Precondiciones:**

* El usuario debe estar autenticado.
* Debe existir una evaluación disponible para el proceso vigente.

**Postcondiciones:**

* Las respuestas son procesadas.
* Se determina el resultado.
* La información correspondiente queda asociada al usuario.

### Flujo principal

1. El usuario accede al módulo de evaluación.
2. El sistema verifica la evaluación disponible.
3. El sistema muestra las preguntas.
4. El usuario selecciona las respuestas.
5. El usuario finaliza la evaluación.
6. El sistema valida las respuestas.
7. El sistema procesa la información.
8. El sistema determina el resultado.
9. El sistema registra la información correspondiente.
10. El sistema muestra el resultado.

### Flujos alternativos

**FA-001 - Respuestas incompletas**

1. El usuario intenta finalizar la evaluación.
2. El sistema identifica respuestas faltantes.
3. El sistema solicita completar la información.
4. El usuario completa las respuestas.

### Excepciones

**E-001 - Evaluación no disponible**

Si no existe una evaluación disponible, el sistema muestra un estado informativo.

**E-002 - Error durante el procesamiento**

Si ocurre un error durante el procesamiento, el sistema informa que no fue posible completar la evaluación.

### Reglas de negocio

* La evaluación debe corresponder al proceso de aprendizaje vigente.
* Las respuestas deben procesarse antes de determinar el resultado.
* El resultado debe quedar asociado al usuario.
* No se contempla un historial independiente de múltiples evaluaciones.

---

## CU-009 - Consultar resultado de evaluación

**Código:** CU-009

**Actor principal:** Usuario autenticado

**Actores secundarios:** Base de datos

**Historia de Usuario relacionada:** HU-009 - Consulta de resultados

**Requisito funcional relacionado:** RF-010 - Consulta del resultado de la evaluación

**Objetivo:**

Permitir al usuario consultar el resultado obtenido en la evaluación correspondiente al proceso vigente.

**Precondiciones:**

* El usuario debe estar autenticado.

**Postcondiciones:**

* El usuario visualiza su resultado cuando este se encuentra disponible.

### Flujo principal

1. El usuario accede a la sección de resultados.
2. El sistema identifica al usuario.
3. El sistema consulta el resultado correspondiente.
4. El sistema obtiene la información.
5. El sistema muestra el resultado.

### Flujos alternativos

**FA-001 - No existe resultado**

1. El sistema consulta la información.
2. No encuentra un resultado disponible.
3. El sistema muestra un estado informativo.

### Excepciones

**E-001 - Error de consulta**

Si ocurre un error durante la consulta, el sistema muestra un mensaje controlado.

### Reglas de negocio

* El resultado debe corresponder al usuario autenticado.
* La consulta corresponde al proceso de evaluación vigente.

---

## CU-010 - Consultar progreso de aprendizaje

**Código:** CU-010

**Actor principal:** Usuario autenticado

**Actores secundarios:** Base de datos

**Historia de Usuario relacionada:** HU-010 - Consulta del progreso de aprendizaje

**Requisito funcional relacionado:** RF-011 - Consulta del progreso de aprendizaje

**Objetivo:**

Permitir al usuario consultar la información disponible sobre su progreso de aprendizaje.

**Precondiciones:**

* El usuario debe estar autenticado.

**Postcondiciones:**

* El usuario visualiza la información de progreso disponible.

### Flujo principal

1. El usuario accede al módulo de progreso.
2. El sistema identifica al usuario.
3. El sistema consulta la información registrada.
4. El sistema procesa la información disponible.
5. El sistema presenta el progreso.

### Flujos alternativos

**FA-001 - Información insuficiente**

1. El sistema consulta la información.
2. Determina que no existen datos suficientes.
3. El sistema muestra un estado informativo.

### Excepciones

**E-001 - Error de consulta**

Si ocurre un error durante la consulta, el sistema informa que no fue posible cargar el progreso.

### Reglas de negocio

* La información debe corresponder al usuario autenticado.
* El progreso debe basarse en la información registrada durante las actividades implementadas.

---

## CU-011 - Gestionar perfil

**Código:** CU-011

**Actor principal:** Usuario autenticado

**Actores secundarios:** Base de datos

**Historia de Usuario relacionada:** HU-012 - Consulta y actualización del perfil

**Requisito funcional relacionado:** RF-013 - Gestión del perfil de usuario

**Objetivo:**

Permitir al usuario consultar y actualizar la información disponible de su perfil.

**Precondiciones:**

* El usuario debe estar autenticado.

**Postcondiciones:**

* La información válida queda actualizada.
* Los cambios pueden visualizarse en consultas posteriores.

### Flujo principal

1. El usuario accede al perfil.
2. El sistema identifica al usuario.
3. El sistema consulta la información.
4. El sistema muestra los datos disponibles.
5. El usuario modifica los campos habilitados.
6. El usuario solicita guardar los cambios.
7. El sistema valida la información.
8. El sistema almacena los cambios.
9. El sistema confirma la actualización.

### Flujos alternativos

**FA-001 - Información inválida**

1. El usuario ingresa información no válida.
2. El sistema identifica el error.
3. El sistema muestra la validación.
4. El usuario corrige la información.

### Excepciones

**E-001 - Error de actualización**

Si ocurre un error al guardar los cambios, el sistema informa que no fue posible completar la actualización.

### Reglas de negocio

* Solo el usuario autenticado puede modificar su información.
* Los campos deben cumplir las validaciones definidas.

---

## CU-012 - Cerrar sesión

**Código:** CU-012

**Actor principal:** Usuario autenticado

**Actores secundarios:** Ninguno

**Historia de Usuario relacionada:** HU-013 - Cierre de sesión

**Requisito funcional relacionado:** RF-014 - Cierre de sesión

**Objetivo:**

Permitir al usuario finalizar de forma controlada su sesión en Mentor-IA.

**Precondiciones:**

* El usuario debe estar autenticado.

**Postcondiciones:**

* La sesión finaliza.
* El usuario deja de tener acceso a las funcionalidades protegidas.
* El sistema dirige al usuario a la vista de acceso correspondiente.

### Flujo principal

1. El usuario selecciona la opción de cerrar sesión.
2. El sistema ejecuta el proceso de cierre.
3. El sistema finaliza la sesión.
4. El sistema dirige al usuario a la vista de acceso.

### Flujos alternativos

**FA-001 - Intento de acceder después del cierre**

1. El usuario intenta acceder a una funcionalidad protegida.
2. El sistema verifica que no existe una sesión válida.
3. El sistema impide el acceso.
4. El sistema solicita autenticación.

### Excepciones

No se contemplan excepciones específicas para el flujo actual.

### Reglas de negocio

* Las funcionalidades privadas requieren una sesión válida.
* El cierre de sesión debe impedir el acceso posterior a recursos protegidos.

---

## CU-013 - Gestionar errores durante la interacción

**Código:** CU-013

**Actor principal:** Usuario de Mentor-IA

**Actores secundarios:** Backend / Servicio de inteligencia artificial / Base de datos

**Historia de Usuario relacionada:** HU-014 - Gestión de errores durante la interacción

**Requisito funcional relacionado:** RF-015 - Gestión de errores

**Objetivo:**

Permitir que Mentor-IA gestione de manera controlada los errores que puedan ocurrir durante las operaciones.

**Precondiciones:**

* El usuario debe estar ejecutando una operación.

**Postcondiciones:**

* El usuario recibe información clara sobre el error.
* La aplicación mantiene un estado controlado.

### Flujo principal

1. El usuario ejecuta una operación.
2. El sistema procesa la solicitud.
3. Se produce un error.
4. El sistema identifica el error.
5. El sistema procesa la respuesta correspondiente.
6. El sistema muestra un mensaje comprensible.
7. El usuario puede continuar utilizando la aplicación cuando sea posible.

### Flujos alternativos

**FA-001 - Error de servicio externo**

1. El sistema realiza una solicitud a un servicio externo.
2. El servicio presenta un error.
3. El sistema controla la respuesta.
4. El sistema muestra un mensaje informativo.

### Excepciones

**E-001 - Error no controlado**

Si se presenta una situación no contemplada, el sistema debe evitar el cierre inesperado de la aplicación y proporcionar una respuesta controlada cuando sea posible.

### Reglas de negocio

* Los mensajes no deben exponer información técnica o sensible.
* Los errores deben comunicarse de forma comprensible.
* Cuando sea posible, el mensaje debe orientar al usuario sobre cómo continuar.

---

## CU-014 - Gestionar proceso de aprendizaje integrado

**Código:** CU-014

**Actor principal:** Usuario autenticado

**Actores secundarios:** Servicio de inteligencia artificial / Base de datos

**Historia de Usuario relacionada:** HU-015 - Experiencia de aprendizaje integrada

**Requisito funcional relacionado:** RF-016 - Integración del proceso de aprendizaje

**Objetivo:**

Permitir que el usuario utilice las funcionalidades de Mentor-IA como parte de un proceso integrado de aprendizaje.

**Precondiciones:**

* El usuario debe estar autenticado.

**Postcondiciones:**

* El usuario puede utilizar las funcionalidades disponibles del proceso.
* La información generada permanece asociada a su cuenta.

### Flujo principal

1. El usuario inicia su proceso de aprendizaje.
2. El usuario realiza una consulta al asistente de IA.
3. El sistema procesa la consulta.
4. El sistema presenta la respuesta.
5. El usuario puede generar recursos de aprendizaje.
6. El sistema genera los recursos disponibles.
7. El usuario consulta los recursos.
8. El usuario realiza la evaluación correspondiente.
9. El sistema procesa las respuestas.
10. El sistema presenta el resultado.
11. El usuario puede consultar su progreso.
12. El sistema presenta la información disponible.

### Flujos alternativos

**FA-001 - Error durante una etapa**

1. Una de las operaciones presenta un error.
2. El sistema controla la situación.
3. El sistema muestra un mensaje informativo.
4. El usuario puede continuar cuando la funcionalidad se encuentre disponible.

### Excepciones

**E-001 - Servicio externo no disponible**

Si el servicio de inteligencia artificial no está disponible, las funcionalidades que dependan directamente de este servicio no podrán completarse hasta que se restablezca la comunicación.

### Reglas de negocio

* Las funcionalidades deben estar disponibles según el estado de autenticación del usuario.
* La información generada debe mantenerse asociada al usuario.
* El proceso contempla las funcionalidades implementadas en la versión actual.
* No se considera dentro del proceso un historial consultable de conversaciones.
* No se considera dentro del proceso un historial independiente de múltiples evaluaciones.

# Matriz de trazabilidad

| HU     | RF             | CU                                     | Funcionalidad             |
| ------ | -------------- | -------------------------------------- | ------------------------- |
| HU-001 | RF-001         | CU-001                                 | Registro                  |
| HU-002 | RF-002, RF-003 | CU-002                                 | Inicio de sesión y acceso |
| HU-003 | RF-004         | CU-003                                 | Menú y navegación         |
| HU-004 | RF-005         | CU-004                                 | Panel principal           |
| HU-005 | RF-006         | CU-005                                 | Asistente IA              |
| HU-006 | RF-007         | CU-006                                 | Generación de recursos    |
| HU-007 | RF-008         | CU-007                                 | Flashcards                |
| HU-008 | RF-009         | CU-008                                 | Evaluación                |
| HU-009 | RF-010         | CU-009                                 | Resultado                 |
| HU-010 | RF-011         | CU-010                                 | Progreso                  |
| HU-011 | RF-012         | CU-006, CU-007, CU-008, CU-009, CU-010 | Asociación de información |
| HU-012 | RF-013         | CU-011                                 | Perfil                    |
| HU-013 | RF-014         | CU-012                                 | Cierre de sesión          |
| HU-014 | RF-015         | CU-013                                 | Gestión de errores        |
| HU-015 | RF-016         | CU-014                                 | Proceso integrado         |
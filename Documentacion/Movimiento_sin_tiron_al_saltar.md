# Ajuste del tirón al saltar hacia delante

En ControlJugador no hay una fuerza que empuje hacia atrás: la velocidad horizontal usa direccion por rapidez. El seguimiento de la cámara sí tenía retraso, y el Rigidbody2D se mostraba sin interpolación. Esas dos cosas pueden dar una sensación de retroceso en pantalla aunque el cuerpo avance.

## Qué cambié

En Jugador > Rigidbody2D activé Interpolate. Unity sigue calculando las físicas por pasos, pero muestra posiciones intermedias entre esos pasos para que el dibujo no avance a saltitos.

También cambié Collision Detection a Continuous. Esto comprueba el trayecto entre posiciones de física y reduce que el cuerpo entre en una esquina antes de que Unity corrija el contacto. Tocar el lateral de una plataforma sigue frenando el avance, como corresponde a un obstáculo sólido.

En CamaraSigue añadí ultimaXJugador y encuadrePreparado. Al empezar, la cámara se coloca a dos unidades por delante del jugador. Después, LateUpdate calcula cuánto avanzó la posición visible desde el fotograma anterior y desplaza la cámara esa misma distancia. SmoothDamp queda para corregir un posible desajuste, en lugar de perseguir con retraso todo el movimiento.

Así el personaje mantiene su sitio horizontal en la pantalla mientras el escenario pasa detrás. La cámara conserva su altura y no sube y baja con el salto.

No cambié ControlJugador, la rapidez, la fuerza del salto, la gravedad ni los colliders. Las animaciones también se conservaron. Interpolate suaviza la presentación y CamaraSigue ajusta el encuadre; no añaden impulso al personaje.

Para revisar los valores en Unity, selecciona Jugador y busca Rigidbody2D > Interpolate. El código de cámara está en Assets/Scripts/CamaraSigue.cs y tiene comentarios encima de cada bloque.

## Comprobación

Se probó una secuencia de caminar, saltar hacia delante y soltar la dirección en el nivel, usando ControlJugador y las físicas reales. En el salto libre no se registró retroceso del cuerpo; la posición horizontal del jugador en pantalla permaneció estable (variación inferior a 0.000002 del ancho visible). Se conservaron los valores actuales del usuario: rapidez 5 y salto 10. El informe quedó en Temp/ResultadoTironSaltoFinal.txt. El código temporal de prueba se retiró del proyecto.

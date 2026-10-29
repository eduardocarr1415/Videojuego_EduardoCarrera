# Contador de hojas con estilo de selva

El contador del nivel ahora está dentro de Canvas > PanelContadorHojas. El panel tiene fondo verde oscuro, borde oliva y una sombra suave. IconoHoja usa el mismo sprite que las hojas coleccionables. El número se lee en color crema.

Se conservó el mismo TMP_Text conectado a ControlNivel.textoHojas. No añadí otro contador ni cambié cómo se recogen las hojas. ActualizarContador sigue escribiendo Hojas: 0 / 10, Hojas: 1 / 10 y así hasta diez.

El panel está anclado arriba a la izquierda, con margen de 28 píxeles respecto al Canvas. Sus imágenes tienen Raycast Target desactivado para no interceptar clics. Está detrás de los paneles de victoria y derrota.

Para cambiarlo tú: abre juegofuncional fuera de Play, despliega Canvas > PanelContadorHojas y selecciona el panel, IconoHoja o el texto. El componente Image cambia los colores; TextMeshPro cambia tamaño, color y alineación de las letras. Guarda con Ctrl+S.

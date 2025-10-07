__PAGINA DEL JUEGO:__ https://xastias.github.io/Aventura-2D---Jugable/


__LO QUE FALTA POR PROGRAMAR Y AGREGAR:__

- Programar sistema de guardado.
- Programar sistema de comprar y almacenar pociones de vida, daño y aumento de vida. 
- Agregar música en el lobby y efectos de sonidos (de espada para el ataque con espada, salto, bola de fuego y recibir daño).
- Programar/agregar ventana o Escena de Gamer Over al tener 0 de vida y boton de volver a jugar y volver al menu.
- Agregar ventana para el boton de "Opciones" donde esté lo siguiente: Quitar la música, Quitar los efectos de sonido, cambiar resolución y limitar fps
- Programar que el jugador reciba daño al caer al vacio que debe saltar en el nivel/escena tutorial.


__ACTUALIZACIÓN:__

__07/10/2025__
Se corrigió la referencia visual en GameManager: la barra de fondo estaba asignada al slot de la barra de vida roja del player.

Se arregló la lógica para que el Collider2D con la propiedad isTrigger que inflige daño al jugador no se destruya automáticamente tras ejecutar el evento de daño.

Se eliminó un evento(sonido del walk) no referenciado en el clip de animación Walk del enemigo esqueleto. Este evento generaba mensajes de advertencia en consola.

Reestructuración de clases jugables. Se actualizó el sistema de clases eliminando la clase Berserker y reemplazándola por Arcano. Esto corrige la visualización incorrecta del nombre compuesto "berserker mago" y mejora la claridad del sistema de selección de clase.

Se corrigió un error donde las variables usedSword y usedFireball permanecían activas indefinidamente tras atacar, bloqueando la selección de otras clases. Se implementó un temporizador de 5 segundos que desactiva ambas variables si no se detecta actividad ofensiva, permitiendo al jugador cambiar entre Caballero/Asesino y Arcano/Mago.

El botón "Continuar" ahora cierra la ventana de clase, permitiendo al jugador avanzar sin interrupciones en el flujo de juego.

Se solucionó en el GameManager un código para que el objeto Player no sea trasladado automáticamente a la nueva escena al realizar un cambio de escena


__06/07/2025__
Se agregaron clases al juego y según el daño recibido, se asigna una clase:
•	Caballero Mágico: Se obtiene usando la espada y bola de fuego

•	Arcano: Se obtiene usando la bola de fuego y recibió daño
•	Mago: Se obtiene usando la bola de fuego y NO recibió daño

•	Caballero: Se obtiene usando la  espada y recibió daño
•	Asesino: Se obtiene usando la espada y NO recibió daño


__05/07/2025__
Ahora el player al llegar al final del nivel Tutorial, es llevado a una escena donde aparece un mensaje agradeciendo por a ver jugado el prototipo. Tambien se colocó un botón para volver al lobby/menu principal


__??/06/2025__
- Sea colocado ventanas instructivas sobre los controles en el nivel tutorial para enseñarle al jugador como jugar
- Ahora en vez dede hacer el combo con R, lo haces con el click izquierdo
- Ahora en vez dede tirar la bola de fuego con el click izquierdo, lo haces con la R
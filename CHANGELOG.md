Registro de cambios

## [0.8.11] - 2026-10-10
- BotonDeEntrega agregado en EstacionDeEntrega.
- Creacion de clase CalculadorDePuntuacion para calcular la puntuacion de la torta.
- Finalizacion de logica de CargadorDePuntuacion.
- nueva animacion de cliente hablando cuando se apreta el boton de entregar añadida.
- Refactorizacion de EstacionDeOrdenes y EstacionDeEntrega, todo se unio en GestorDeClientes.
## [0.7.11] - 2026-10-09
- Arreglo de bug en GestorDeTickets.
## [0.7.10] - 2026-10-07
- Metodo para gestionar las actualizaciones del ticket de la EstacionDeEntrega creado en GestorDeTickets.
## [0.7.9] - 2026-10-06
- Logica de arrastre de torta en EstacionDeEntrega corregida.
## [0.7.8] - 2026-10-05
- Comienzo de logica de EstacionDeEntrega.
- Las tortas ya se dibujan en esta estacion.
- los clientes de la fila de entregas en EstacionDeOrdenes en primera posicion aparecen en la estacion de entrega.
## [0.7.7] - 2026-10-04
- Creacion de texturas de torta con cobertura.
- logica de EstacionDeDecoracion terminada: Contiene boton para volver atras, Boton siguiente, botones para agregar los enums a la torta y un array de toppings para crear toppins.
- Correccion de desplazamiento en arrastrar en EstacionDeHorneado.
- nueva fila de entrega agregada.
- logica de nueva fila en EstacionDeOrdenes agregada.
- Solucion de problema de arrastre de ticket.
- Creacion de 5 texturas mas para clientes.
## [0.6.7] - 2026-10-03
- Creacion de la clase GestorDePedidos para gestionar las tortas en todas las estaciones.
- Creacion de la clase BotonForma para seleccionar la forma del molde en EstacionDeMezcla.
- Logica de GestorDePedidos añadida en todas las esatciones.
- Creacion del enum EstacionActual para saber en que estacion esta la torta.
- Creacion e implementacion de las imagenes de cada molde con cada sabor de bizcochuelo y estado de coccion
- Logica de EstacionDeMezcla terminada.
- Logica de EstacionDeHorneado terminada
- Creacion de la clase Topping.
- Creacion de la clase ObjetoArrastrable.
- Logica de rellenar el bizcochuelo y colocar los toppings completada.
- Correcion de pantallaPausa por tema de botones pausado y reanudado.
## [0.5.7] - 2026-09-30
- Incorporacion de botones de pausa y reanuacion de musica en PantallaJuego.
## [0.5.6] - 2026-09-28
- Reestructuracion de de logica de GestorDeTickets --> ya no es mas static.
- Logica de dibujadorDeTicket completa.
- Creacion de la clase repositorioImagenes.
- Botones de estacionMezcla agregados.
- Texturas de Tazon agregadas en repositorioImagenes.
## [0.5.5] - 2026-09-27
- Creacion de la clase statica GestorDeTickets.
- Logica de colision de tickets corregida.
- Ahora los tickets se ven en todas las estaciones
## [0.5.4] - 2026-09-20
- Creacion de la clase ConfiguracionSkin.
- Problema de animacion esperando resuelto.
- Implementacion de animacion de personaje saliendo de la tienda.
## [0.5.3] - 2026-09-11
- Creacion de GestorDePantalla, PantallaJuego, MenuPrincipal y PantallaPausa.
- Creacion de fondos e imagenes para cada una de esas clases.
- Creacion de GestorDeAudio.
- Funcion actualizar en BotonBase para que cambie de color.
- Refactorizacion de Game1 con la creacion de AdministradorDePestañas y GestorDePantalla.
## [0.4.3] - 2026-09-10
- Creacion de la clase DetectorDeColisiones.
- Logica de colision entre personaje y posicion de orden agregada.
- Logica de arrastre en EstacionDeOrdenes agregada.
- Logica de personajo pidiendo el pedido agregada.
## [0.3.3] - 2026-09-09
- Creacion de clases GeneradorDeClientes, FabricacionDePedidos, CargadorDeSkins y SkinsCliente.
- Creacion del enum EstadoCliente.
-  Logica de EstacionDeOrdenes hecha hasta la mitad.
-  Agregacion de 2 personajes.
-  Agregacion de texturas de objetos de la EstacionDeMezcla.
## [0.2.3] - 2026-09-08
- Creacion de clase Animacion, AdaptadorDeResoluciones y Constante.
- Implementacion de prueba de Sprite en movimiento.
## [0.1.3] - 2026-09-02
### Agregado
-  Creacion de las clases Tazon, BotonBase, BotonSiguiente y BotonSabor.
-  implementacion de la logica de EstacionDeMezcla.
## [0.1.2] - 2026-08-07
### Agregado
-  Sistema de pestañas interactivas en la parte inferior para cambiar en tiempo real entre las 5 estaciones del juego.
-  Implementación de las clases base y derivadas para `EstacionDeOrdenes`, `EstacionDeMezcla`, `EstacionDeHorneado`, `EstacionDeDecoracion` y `EstacionDeEntrega`.
-  Manejo de la lógica de colisión (hitbox) y detección de clics del mouse para la navegación.
-  Carga e integración de las imágenes y fondos de cada estación en el pipeline de contenido de MonoGame (`Content.mgcb`).  
-  Creación de las clases principales y enums.
## [0.0.1] - 2026-07-14
### Corregido
- Reorganización del README.
- Corrección de como compilar en el README.
- Corrección del CHANGELOG 
## [0.1.0] - 2026-07-02
### Agregado
- Inicialización del proyecto base en MonoGame y C#.
- Creación de la solución en Visual Studio 2026.
- Archivo `.gitignore` para el control de versiones.
- Archivos `README.md` y `CHANGELOG.md` con la documentación inicial.
- Propuesta subida a la wiki.

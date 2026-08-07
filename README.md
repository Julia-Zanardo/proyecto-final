## Julpajulparaiso Pasteleria
Nombre del juego: Julpajulparaiso Pasteleria.  
Integrante del proyecto: Julia Garcia Cruz Zanardo.

**Descripcion:**  
En este proyecto se desarrollará Julpajulparaiso, un videojuego de simulación y gestión de tiempo en 2D
ambientado en una pastelería. El jugador asumirá el rol de un repostero encargado de atender clientes y preparar
tortas según los pedidos realizados. Para completar cada pedido deberá recorrer distintas estaciones de trabajo,
donde llevará a cabo la preparación, el horneado, la decoración y la entrega de las tortas. El objetivo será
completar los pedidos con rapidez y precisión para obtener una mayor puntuación y satisfacer a los clientes. La
partida avanzará a lo largo de diferentes días de trabajo, incorporando nuevos desafíos mediante el desbloqueo de
ingredientes, moldes, sabores y decoraciones.

El programa se va a desarrollar en lenguaje C# versión 12.0 bajo la plataforma .NET versión 9.0, utilizando el entorno de desarrollo integrado Visual Studio 2026 y el framework de desarrollo de videojuegos MonoGame versión 3.8.4.1. Asimismo, para la gestión de la persistencia de datos y el guardado del progreso se utilizará un motor de base de datos relacional local integrado mediante SQLite. La comunicación con la base de datos se realizará de manera directa mediante sentencias SQL nativas utilizando el conector relacional nativo de ADO.NET, lo que permitirá un control preciso sobre las consultas y la estructura de almacenamiento.

**Cómo Compilar y Ejecutar**  
Requisitos Previos

Antes de empezar, asegurate de tener instalado lo siguiente en tu sistema:
* **SDK de .NET 9.0 (x64)** - [Descargar desde el sitio oficial de Microsoft](https://dotnet.microsoft.com/es-es/download/dotnet/9.0) (Fijate de instalar el **SDK**).
* **Visual Studio 2022 o posterior** con la carga de trabajo **"Desarrollo de escritorio de .NET"** activa.
* **Git** instalado en tu sistema.

Pasos para la Ejecución  
**1. Clonar el repositorio**  
Abrí tu terminal en la carpeta donde quieras descargar el juego y ejecutá:
```bash
git clone https://github.com/Julia-Zanardo/proyecto-final.git
```
**2. Acceder al directorio del proyecto**
```bash
cd proyecto-final
```
**3. Compilar y Ejecutar**  
Ejecuta el siguiente comando para restaurar dependencias, compilar y lanzar el juego automáticamente:
```bash
dotnet run
```

Estado Actual del Proyecto: Configuracion inicial y estructura del proyecto.

**Enlace a la wiki del proyecto:** 
[Ver la propuesta del proyecto](https://github.com/Julia-Zanardo/proyecto-final/wiki/Propuesta-del-Proyecto-%E2%80%90-Julpajulparaiso-Pasteleria)

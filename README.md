# Práctica 9 - Interfaces Multimodales 

**Asignatura:** Interfaces Inteligentes  
**Curso:** 2026-2027  
**Autora:** Sonia Frías Jiménez

## Descripción de la práctica

El objetivo de esta práctica es utilizar los componentes de entrada multimodal disponibles en `UnityEngine` (micrófono y cámara web) para capturar señales del mundo real e integrarlas dentro de escenas interactivas en Unity3D.

La práctica se divide en 3 ejercicios en dos escenas:
1. **Escena ya existente:** Reproducción de efectos de sonido pregrabados mediante eventos de colisión.
2. **Escena Multimodal:** Espacio con una pantalla central y altavoces donde se captura y reproduce audio desde el micrófono y se proyecta vídeo en directo desde la webcam, permitiendo además guardar fotogramas en disco.

## 1. Sonido pregrabado en la escena ya existente

Se pedía utilizar una escena de los guerreros realizada en la práctica 3, sin embargo ante la imposibilidad de recuperarla he utilizado el juego implementado en esa misma práctica. El juego trata de un astronauta que recoge minerales en la luna y debe evitar que unos fantasmas lo encuentren.

He buscado dos pistas de sonido gratuitas acordes para reproducir en las siguientes situaciones:
1. Al recolectar un mineral
2. Al colisionar con un fantasma
> Recordatorio: los fantasmas tienen dos collider. Uno es isTrigger para detectar cuando el astronauta pasa cerca y otro collider no trigger para detectar la colisión.

Para añadir el audio he añadido un componente `AudioSource` a los minerales, vinculando el archivo de audio en el campo AudioClip y desactivando la opción *Play On Awake*. Luego, en los [scripts](Scripts/Ejercicio1/) correspondientes, dentro de los métodos [`OnTriggerEnter`](Scripts/Ejercicio1/Chaser.cs) y [`OnCollisionEnter`](Scripts/Ejercicio1/Respawn.cs) se dispara la reproducción del sonido mediante `AudioSource.Play()`.

### Prueba
Se presenta un vídeo como prueba, ya que los gifs no tienen sonido.
[video](videos/Ejercicio1.mp4)

## 2. Escena Multimodal (Micrófono y Cámara)

Se ha creado una escena en un espacio abierto compuesta por un plano a modo de suelo, un Quad central que actúa como pantalla de televisión y se ha importado un asset para los altavoces.

### 2.1. Captura y reproducción de audio 

El script `Recorder.cs` está asignado al objeto de los altavoces junto con un componente `AudioSource`, sin asignar ninguna pista de audio.

Al iniciar, el script verifica mediante `Microphone.devices` si hay micrófonos disponibles en el sistema.
**Controles implementados:**
  * **Tecla R:** Inicia la grabación de audio desde el micrófono principal del sistema mediante `Microphone.Start("", false, 10, 44100)`.
  * **Tecla P:** Detiene la captación de audio con `Microphone.End("")` y reproduce el `AudioClip` grabado a través del `AudioSource`.

> He optado por reproducir el clip inmediatamente después de finalizar la grabación en lugar de hacerlo de forma simultánea debido a problemas con el volumen de entrada y salida

### Prueba

### 2.2. Proyección de cámara y captura de fotogramas

El script `TV.cs` está asignado al objeto Quad y accede a su `Renderer.material` para proyectar en tiempo real la textura obtenida de `WebCamTexture`.

En el inicio se comprueba `WebCamTexture.devices`, se muestra por consola el nombre de la cámara detectada y se inicializa la ruta de guardado de capturas en `Application.dataPath`. **Controles implementados:**
  * **Tecla S:** Asigna `webCamTexture` a `tvMaterial.mainTexture` e inicia el flujo de vídeo mediante `webCamTexture.Play()`.
  * **Tecla X:** Captura el fotograma actual volcándolo a un objeto `Texture2D` y lo guarda en disco en formato `.png`.
  * **Tecla Espacio:** Detiene la reproducción de la cámara web con `webCamTexture.Stop()`.

### Prueba
En el vídeo se muestran las distintas funcionalidades, para que se pueda escuchar el sonido se ha grabado un video en lugar de un gif y se han añadido comentarios para que se entienda correctamente. 

[video](videos/Ejercicio2.mp4)

La captura tomada:
![captura](videos/Captura_2.png)

- El vídeo solo ha capturado la salida del ordenador por lo que la entrada del micrófono no se aprecia en el momento que se toma la grabación.
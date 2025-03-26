# DemoLapjv

DemoLapjv es una aplicación de demostración desarrollada en C# y WPF que usa el algoritmo Jonker-Volgenant para resolver el problema de asignación óptima. Este demo utiliza la librería [LapjvCSharp](https://github.com/Rainbowdashx1/LapjvCSharp) para optimizar la asignación de una nube de puntos aleatorios a una figura geométrica (círculo o cuadrado), mostrando en tiempo real tanto la animación de los puntos en un Canvas como la matriz de costos en un DataGrid.

![DemoLapjv](https://raw.githubusercontent.com/Rainbowdashx1/DemoLapjv/master/demo.gif)

## Características

- **Visualización en tiempo real:**  
  Animación de puntos que se mueven desde posiciones aleatorias hasta conformar una figura geométrica.
  
- **Optimización global:**  
  Uso del algoritmo Jonker-Volgenant (a través de LapjvCSharp) para minimizar la suma total de distancias, optimizando la asignación de cada punto.

- **Explicación técnica:**  
  El demo resalta cómo algunos puntos pueden parecer recorrer mayores distancias, lo cual el algoritmo hace para optimizar globalmente el costo total, lo que resulta en una solución óptima a nivel del sistema.

## Requisitos

- [.NET Core/8](https://dotnet.microsoft.com/download) compatible con aplicaciones WPF.
- [Visual Studio](https://visualstudio.microsoft.com/) (o IDE de tu preferencia) para compilar y ejecutar el proyecto.
- Referencia a la librería [LapjvCSharp](https://github.com/Rainbowdashx1/LapjvCSharp) (se puede instalar vía NuGet o incluir el proyecto en la solución).

## Instalación

1. Clona el repositorio:
    ```bash
    git clone https://github.com/Rainbowdashx1/DemoLapjv.git
    ```

2. Abre la solución `DemoLapjv.sln` en Visual Studio.

## Uso

- **Ejecutar la aplicación:**  
  Al iniciar, se genera una nube de puntos aleatoria y una figura destino (círculo o cuadrado) se dibuja en el Canvas. La aplicación calcula la asignación óptima de cada punto mediante LapjvCSharp y anima la transición de cada punto desde su posición inicial hasta su destino asignado.

- **Matriz de Costos:**  
  En el panel lateral se muestra un DataGrid con el detalle de cada asignación: posición de origen, posición destino y el costo (distancia euclidiana) de dicha asignación.

- **Reiniciar la simulación:**  
  Utiliza el botón "Reiniciar" para limpiar el Canvas y el DataGrid y correr nuevamente la simulación.

## Explicación Técnica

El algoritmo Jonker-Volgenant implementado en **LapjvCSharp** optimiza la asignación global de puntos minimizando la suma total de distancias. Esto significa que, en algunos casos, un punto puede terminar asignado a un destino que no es el más cercano individualmente. Este compromiso permite alcanzar una solución global óptima, donde se minimiza el costo total del sistema, aunque a simple vista algunos puntos parezcan recorrer una distancia mayor.

Este comportamiento es importante de destacar en demostraciones profesionales, ya que ilustra cómo la optimización global puede diferir de una asignación "greedy" local, aportando valor añadido a problemas de asignación en la vida real.


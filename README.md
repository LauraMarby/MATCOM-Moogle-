!!!!! MANUAL DE USUARIO !!!!

Moogle! es un explorador diseñado para recibir una búsqueda relacionada con archivos de una biblioteca de textos (.txt)
y devolver los archivos más relacionados a la búsqueda de la biblioteca. Para ello utiliza un modelo vectorial que ejecuta 
un cálculo de TF-IDF y halla la similitud del coseno entre los vectores documentos y el vector consulta.

Para usar Moogle, abra su navegador e introduzca en la página principal una consulta. Al realizar la primera búsqueda, el
navegador procesará todos los documentos .txt que se encuentren en la carpeta Content, que está dentro de la carpeta raíz.
Tras haber realizado la primera búsqueda, el navegador habrá realizado todos los cálculos importantes y por tanto las búsquedas
restantes serán bastante rápidas.

Otra funcionalidad del Moogle! son sus operadores de búsqueda:
--> El operador ! se usa para que ningún documento devuelto tenga esta palabra (la forma correcta de usarla es -> !unwantedword)

--> El operador ^ se usa para que todos los documentos devueltos tengan esta palabra (^onlythisword)

--> El operador * aumenta la importancia que se le debe dar a un vocablo específico. Mientras más * precedan a la palabra, más
    importancia se le da a la misma (***veryimportant, **notveryimportant, *justalittlemoreimportant)

--> El operador ~ aumenta la importancia de cada documento que contenga a un par de palabras. Mientras más cercanas sean estas
    palabras en dicho documento, mayor será la importancia.

La última funcionalidad del Moogle! es su sistema de corrección y recomendación: si usted escribe una palabra que no se encuentra
en la biblioteca (faltas ortográficas, palabras incompletas, etc) el sistema de respuesta le brindará una recomendación con la
palabra más parecida a la suya que se encontró en la biblioteca.

Para ejecutar el navegador:
SO: Linux --> Abrir con Visual Studio Code e insertar en la terminal "make dev"
SO: Windows --> Abrir con Visual Studio Code e insertar en la terminal "dotnet watch run --project MoogleServer"

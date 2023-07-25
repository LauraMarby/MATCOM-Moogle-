#!/bin/bash

if [ $1 == "run" ]; then
    cd ../
    make dev
elif [ $1 == "clean" ]; then
    cd ../Informe/
    find . ! -name Informe.tex -type f -delete
    cd ../Presentacion/
    find . ! -name Presentacion.tex -type f -delete
    echo "Los ficheros de compilación auxiliar de los PDF han sido eliminados satisfactoriamente"
    cd ../MoogleEngine
    rm -r bin
    rm -r obj
    cd ../MoogleServer
    rm -r bin
    rm -r obj
    echo "Los ficheros de compilación del proyecto han sido eliminados satisfactoriamente"
elif [ $1 == "report" ]; then
    cd ../Informe/
    pdflatex Informe
elif [ $1 == "slides" ]; then
    cd ../Presentacion/
    pdflatex Presentacion
elif [ $1 == "show_report" ]; then
    cd ../Informe/
    if [ ! -f Informe.pdf ]; then
        echo Creando Informe...
        pdflatex Informe
        echo Abriendo Informe...

    else
        echo Abriendo Informe...

    fi
    evince Informe.pdf
elif [ $1 == "show_slides" ]; then
    cd ../Presentacion/
    if [ ! -f Presentacion.pdf ]; then
        echo Creando Presentación...
        pdflatex Presentacion
        echo Abriendo Presentación...

    else
        echo Abriendo Presentación...

    fi
    evince Presentacion.pdf
else
    echo "Comando inválido. Por favor reinicie el programa e introduzca un comando válido"
fi
#!/bin/bash

if [ $1 == "run" ]; then
    cd ../
    make dev
elif [ $1 == "clean" ]; then
    cd ../Informe/
    find . ! -name Informe.tex -type f -delete
    cd ../Presentación/
    find . ! -name Presentación.tex -type f -delete
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
    cd ../Presentación/
    pdflatex Presentación
elif [ $1 == "show_report" ]; then
    cd ../Informe/
    if [ ! -f Informe.pdf ]; then
        echo Creando Informe...
        pdflatex Informe
        echo Abriendo Informe...
    else
        echo Abriendo Informe...
    fi
    if [ -z "$2" ]; then
        evince Informe.pdf
    else
        $2 Informe.pdf
    fi
elif [ $1 == "show_slides" ]; then
    cd ../Presentación/
    if [ ! -f Presentación.pdf ]; then
        echo Creando Presentación...
        pdflatex Presentación
        echo Abriendo Presentación...
    else
        echo Abriendo Presentación...
    fi
    if [ -z "$2" ]; then
        evince Presentación.pdf
    else
        $2 Presentación.pdf
    fi
else
    echo "Comando inválido"
fi
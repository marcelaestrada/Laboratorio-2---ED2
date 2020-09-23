using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio2ED2
{
    interface iArbol<T>
    {
        void Insertar(T value, int grado, int idCorrespondiente);
        void Eliminar(T value);
        T Buscar(T value);
    }
}

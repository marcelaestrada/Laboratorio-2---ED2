using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio2ED2
{
    interface iArbol<T>
    {
        int Insertar(T value, int idCorrespondiente);
        bool Eliminar(string value);
        T Buscar(T value);
    }
}

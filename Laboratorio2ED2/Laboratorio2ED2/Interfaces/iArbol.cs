﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio2ED2
{
    interface iArbol<T>
    {
        void Insertar(T value, int idCorrespondiente);
        void Eliminar(string value);
        T Buscar(T value);
    }
}

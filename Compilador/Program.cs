using System;
using System.Collections.Generic;

namespace Compilador
{
    class Program
    {
        static void Main(string[] args)
        {
            string entrada = @"
            BST arbol = nuevo;
            insertar arbol = 12;
            
            eliminar arbol = 12;
            buscar arbol = 12;
            ";

            AnalizadorLexico analizadorLexico = new AnalizadorLexico(entrada);
            AnalizadorSintactico analizadorSintactico = new AnalizadorSintactico(analizadorLexico);
            List<Declaracion> declaraciones = analizadorSintactico.Analizar();

            var mapaArboles = new Dictionary<string, ArbolBinarioBusqueda>();

            foreach (var declaracion in declaraciones)
            {
                if (declaracion is NuevaDeclaracionBST nuevaBST)
                {
                    mapaArboles[nuevaBST.Identificador] = new ArbolBinarioBusqueda();
                    Console.WriteLine($"Se creó un nuevo BST: {nuevaBST.Identificador}");
                }
                else if (declaracion is DeclaracionInsertar insertar)
                {
                    if (mapaArboles.TryGetValue(insertar.Identificador, out var arbol))
                    {
                        arbol.Insertar(insertar.Numero);
                        Console.WriteLine($"Se insertó {insertar.Numero} en el BST: {insertar.Identificador}");
                    }
                }
                else if (declaracion is DeclaracionEliminar eliminar)
                {
                    if (mapaArboles.TryGetValue(eliminar.Identificador, out var arbol))
                    {
                        arbol.Eliminar(eliminar.Numero);
                    }
                }
                else if (declaracion is DeclaracionBuscar buscar)
                {
                    if (mapaArboles.TryGetValue(buscar.Identificador, out var arbol))
                    {
                        bool encontrado = arbol.Buscar(buscar.Numero);
                        Console.WriteLine($"Búsqueda de {buscar.Numero} en el BST {buscar.Identificador}: {encontrado}");
                    }
                }
            }
        }
    }
}

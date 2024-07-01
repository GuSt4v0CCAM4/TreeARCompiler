using System;
using System.Collections.Generic;

namespace Compilador
{
    public class AnalizadorSintactico
    {
        private readonly AnalizadorLexico _analizadorLexico;
        private Token _tokenActual;
        private HashSet<string> _arbolesCreados;

        public AnalizadorSintactico(AnalizadorLexico analizadorLexico)
        {
            _analizadorLexico = analizadorLexico;
            _tokenActual = _analizadorLexico.SiguienteToken();
            _arbolesCreados = new HashSet<string>();
        }

        private void Consumir(TipoToken tipoEsperado)
        {
            if (_tokenActual.Tipo == tipoEsperado)
            {
                _tokenActual = _analizadorLexico.SiguienteToken();
            }
            else
            {
                throw new Exception($"Se esperaba el token {tipoEsperado} pero se obtuvo {_tokenActual.Tipo}");
            }
        }

        public List<Declaracion> Analizar()
        {
            var declaraciones = new List<Declaracion>();

            while (_tokenActual.Tipo != TipoToken.EOF)
            {
                declaraciones.Add(AnalizarDeclaracion());
            }

            return declaraciones;
        }

        private Declaracion AnalizarDeclaracion()
        {
            switch (_tokenActual.Tipo)
            {
                case TipoToken.BST:
                    return AnalizarNuevaDeclaracionBST();
                case TipoToken.INSERTAR:
                    return AnalizarDeclaracionInsertar();
                case TipoToken.ELIMINAR:
                    return AnalizarDeclaracionEliminar();
                case TipoToken.BUSCAR:
                    return AnalizarDeclaracionBuscar();
                default:
                    throw new Exception($"Token inesperado {_tokenActual.Tipo}");
            }
        }

        private Declaracion AnalizarNuevaDeclaracionBST()
        {
            Consumir(TipoToken.BST);
            string identificador = _tokenActual.Valor;
            Consumir(TipoToken.IDENTIFICADOR);
            Consumir(TipoToken.ASIGNACION);
            Consumir(TipoToken.NUEVO);
            Consumir(TipoToken.PUNTO_Y_COMA);
            _arbolesCreados.Add(identificador);
            return new NuevaDeclaracionBST(identificador);
        }

        private Declaracion AnalizarDeclaracionInsertar()
        {
            Consumir(TipoToken.INSERTAR);
            string identificador = _tokenActual.Valor;
            if (!_arbolesCreados.Contains(identificador))
            {
                throw new Exception($"Error semántico: El árbol '{identificador}' no ha sido creado.");
            }
            Consumir(TipoToken.IDENTIFICADOR);
            Consumir(TipoToken.ASIGNACION);
            int numero = int.Parse(_tokenActual.Valor);
            Consumir(TipoToken.NUMERO);
            Consumir(TipoToken.PUNTO_Y_COMA);
            return new DeclaracionInsertar(identificador, numero);
        }

        private Declaracion AnalizarDeclaracionEliminar()
        {
            Consumir(TipoToken.ELIMINAR);
            string identificador = _tokenActual.Valor;
            if (!_arbolesCreados.Contains(identificador))
            {
                throw new Exception($"Error semántico: El árbol '{identificador}' no ha sido creado.");
            }
            Consumir(TipoToken.IDENTIFICADOR);
            Consumir(TipoToken.ASIGNACION);
            int numero = int.Parse(_tokenActual.Valor);
            Consumir(TipoToken.NUMERO);
            Consumir(TipoToken.PUNTO_Y_COMA);
            return new DeclaracionEliminar(identificador, numero);
        }

        private Declaracion AnalizarDeclaracionBuscar()
        {
            Consumir(TipoToken.BUSCAR);
            string identificador = _tokenActual.Valor;
            if (!_arbolesCreados.Contains(identificador))
            {
                throw new Exception($"Error semántico: El árbol '{identificador}' no ha sido creado.");
            }
            Consumir(TipoToken.IDENTIFICADOR);
            Consumir(TipoToken.ASIGNACION);
            int numero = int.Parse(_tokenActual.Valor);
            Consumir(TipoToken.NUMERO);
            Consumir(TipoToken.PUNTO_Y_COMA);
            return new DeclaracionBuscar(identificador, numero);
        }
    }

    public abstract class Declaracion { }

    public class NuevaDeclaracionBST : Declaracion
    {
        public string Identificador { get; }

        public NuevaDeclaracionBST(string identificador)
        {
            Identificador = identificador;
        }
    }

    public class DeclaracionInsertar : Declaracion
    {
        public string Identificador { get; }
        public int Numero { get; }

        public DeclaracionInsertar(string identificador, int numero)
        {
            Identificador = identificador;
            Numero = numero;
        }
    }

    public class DeclaracionEliminar : Declaracion
    {
        public string Identificador { get; }
        public int Numero { get; }

        public DeclaracionEliminar(string identificador, int numero)
        {
            Identificador = identificador;
            Numero = numero;
        }
    }

    public class DeclaracionBuscar : Declaracion
    {
        public string Identificador { get; }
        public int Numero { get; }

        public DeclaracionBuscar(string identificador, int numero)
        {
            Identificador = identificador;
            Numero = numero;
        }
    }
}

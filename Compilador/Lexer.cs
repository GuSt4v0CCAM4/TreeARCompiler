using System;
using System.Collections.Generic;

namespace Compilador
{
    public enum TipoToken
    {
        BST,
        NUEVO,
        INSERTAR,
        ELIMINAR,
        BUSCAR,
        IDENTIFICADOR,
        NUMERO,
        ASIGNACION,
        PUNTO_Y_COMA,
        EOF,
        DESCONOCIDO
    }

    public class Token
    {
        public TipoToken Tipo { get; }
        public string Valor { get; }

        public Token(TipoToken tipo, string valor)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public override string ToString()
        {
            return $"{Tipo}: {Valor}";
        }
    }

    public class AnalizadorLexico
    {
        private readonly string _entrada;
        private int _posicion;

        private static readonly Dictionary<string, TipoToken> _palabrasClave = new Dictionary<string, TipoToken>
        {
            {"BST", TipoToken.BST},
            {"nuevo", TipoToken.NUEVO},
            {"insertar", TipoToken.INSERTAR},
            {"eliminar", TipoToken.ELIMINAR},
            {"buscar", TipoToken.BUSCAR}
        };

        public AnalizadorLexico(string entrada)
        {
            _entrada = entrada;
            _posicion = 0;
        }
        public Token SiguienteToken()
        {
            if (_posicion >= _entrada.Length)
            {
                return new Token(TipoToken.EOF, string.Empty);
            }

            char actual = _entrada[_posicion];

            if (char.IsWhiteSpace(actual))
            {
                _posicion++;
                return SiguienteToken();
            }

            if (char.IsLetter(actual))
            {
                return LeerPalabraClaveOIdentificador();
            }

            if (char.IsDigit(actual))
            {
                return LeerNumero();
            }

            switch (actual)
            {
                case '=':
                    _posicion++;
                    return new Token(TipoToken.ASIGNACION, "=");
                case ';':
                    _posicion++;
                    return new Token(TipoToken.PUNTO_Y_COMA, ";");
                default:
                    _posicion++;
                    return new Token(TipoToken.DESCONOCIDO, actual.ToString());
        
            }
        }

        private Token LeerPalabraClaveOIdentificador()
        {
            int inicio = _posicion;

            while (_posicion < _entrada.Length && (char.IsLetterOrDigit(_entrada[_posicion]) || _entrada[_posicion] == '_'))
            {
                _posicion++;
            }

            string valor = _entrada.Substring(inicio, _posicion - inicio);

            if (_palabrasClave.TryGetValue(valor, out TipoToken tipo))
            {
                return new Token(tipo, valor);
            }

            return new Token(TipoToken.IDENTIFICADOR, valor);
        }

        private Token LeerNumero()
        {
            int inicio = _posicion;

            while (_posicion < _entrada.Length && char.IsDigit(_entrada[_posicion]))
            {
                _posicion++;
            }

            string valor = _entrada.Substring(inicio, _posicion - inicio);
            return new Token(TipoToken.NUMERO, valor);
        }
    }
}

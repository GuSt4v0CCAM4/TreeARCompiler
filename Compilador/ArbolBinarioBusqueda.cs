namespace Compilador
{
    public class Nodo
    {
        public int Valor { get; set; }
        public Nodo? Izquierda { get; set; }
        public Nodo? Derecha { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierda = null;
            Derecha = null;
        }
    }

    public class ArbolBinarioBusqueda
    {
        public Nodo? Raiz { get; private set; }

        public bool Insertar(int valor)
        {
            if (Buscar(valor))
            {
                Console.WriteLine($"Nodo {valor} ya está insertado en el árbol.");
                return false;
            }
            Raiz = Insertar(Raiz, valor);
            return true;
        }

        private Nodo Insertar(Nodo? nodo, int valor)
        {
            if (nodo == null)
            {
                return new Nodo(valor);
            }

            if (valor < nodo.Valor)
            {
                nodo.Izquierda = Insertar(nodo.Izquierda, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecha = Insertar(nodo.Derecha, valor);
            }

            return nodo;
        }

        public bool Eliminar(int valor)
        {
            if (!Buscar(valor))
            {
                Console.WriteLine($"Nodo {valor} no encontrado en el árbol.");
                return false;
            }
            Raiz = Eliminar(Raiz, valor);
            Console.WriteLine($"Se eliminó {valor} del BST");
            return true;
        }

        private Nodo? Eliminar(Nodo? nodo, int valor)
        {
            if (nodo == null)
            {
                return null;
            }

            if (valor < nodo.Valor)
            {
                nodo.Izquierda = Eliminar(nodo.Izquierda, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecha = Eliminar(nodo.Derecha, valor);
            }
            else
            {
                if (nodo.Izquierda == null)
                {
                    return nodo.Derecha;
                }

                if (nodo.Derecha == null)
                {
                    return nodo.Izquierda;
                }

                Nodo temp = EncontrarMinimo(nodo.Derecha);
                nodo.Valor = temp.Valor;
                nodo.Derecha = Eliminar(nodo.Derecha, temp.Valor);
            }

            return nodo;
        }

        private Nodo EncontrarMinimo(Nodo nodo)
        {
            while (nodo.Izquierda != null)
            {
                nodo = nodo.Izquierda;
            }

            return nodo;
        }

        public bool Buscar(int valor)
        {
            bool encontrado = Buscar(Raiz, valor);
            return encontrado;
        }

        private bool Buscar(Nodo? nodo, int valor)
        {
            if (nodo == null)
            {
                return false;
            }

            if (valor == nodo.Valor)
            {
                return true;
            }

            if (valor < nodo.Valor)
            {
                return Buscar(nodo.Izquierda, valor);
            }
            else
            {
                return Buscar(nodo.Derecha, valor);
            }
        }
    }
}

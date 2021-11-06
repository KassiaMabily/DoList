using System;

namespace DoList
{
    class Program
    {
        static void Main(string[] args)
        {
            Usuario usuario = new Usuario("Kassia");
            
            Tarefa t1 = new Tarefa();
            Tarefa t2 = new Tarefa();

            t1.setIdentificador(1);
            t2.setIdentificador(2);

            usuario.adicionarTarefa(t1);
            usuario.adicionarTarefa(t2);

            usuario.printQuadro();
        }
    }
}

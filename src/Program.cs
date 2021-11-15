using System;
using System.IO;
using System.Collections.Generic;
namespace DoList
{
    class Program
    {
        
        static void Main(string[] args)
        {   

            string caminhoArquivo = "./data.csv";
            Boolean arquivoExiste = File.Exists(caminhoArquivo);

            if(arquivoExiste) 
            {
                Console.WriteLine("Seja bem-vindo novamente!");
                Console.WriteLine("Você deseja continuar o progresso?\n1 - Sim\n2 - Não, desejo apagar meu progresso");
                Console.Write("DIGITE A SUA ESCOLHA: ");
                string resposta = Console.ReadLine();
                if(resposta == "1")
                {
                    Usuario usuario = new Usuario("Fulaninho");
                    Menu(usuario);
                } else {
                    File.Delete(caminhoArquivo);
                    iniciar();
                }

            }
            else {
                iniciar();
            }

            
        }

        static void iniciar() {
            Console.WriteLine("Olá, seja bem-vindx!");
            Console.WriteLine("Como devo te chamar? ");
            string nomeUsuario = Console.ReadLine();

            Usuario usuario = new Usuario(nomeUsuario);

            Console.WriteLine(string.Format(
                "\nVocê ainda não possui tarefas, deseja adicionar agora?\n"+
                "1 - Sim\n"+
                "2 - Não"
            ));
            Console.Write("DIGITE A SUA ESCOLHA: ");
            string resposta = Console.ReadLine();

            if(resposta == "1") {
                criarTarefa(usuario);

                Menu(usuario);
            }
        }

        static void criarTarefa(Usuario usuario) {
            Console.WriteLine("\nADICIONAR TAREFA");
            Console.Write("Titulo: ");
            string titulo = Console.ReadLine();

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Prioridade (1 - Baixa, 2 - Media, 3 - Urgente) [1]: ");
            string prioridade = Console.ReadLine();

            Console.Write("Numero de dias para conclusão [7]: ");
            string dias = Console.ReadLine();

            Tarefa novaTarefa = new Tarefa(
                !String.IsNullOrEmpty(dias) ? int.Parse(dias) : 7, 
                titulo, 
                descricao, 
                !String.IsNullOrEmpty(prioridade) ? Constants.PRIORIDADES[int.Parse(prioridade) - 1] : Constants.PRIORIDADES[0]
            );

            usuario.adicionarTarefa(novaTarefa);
        }

        static void arquivarTarefa(Usuario usuario) {
            Console.WriteLine("\nSEU QUADRO:");
            usuario.printQuadro();

            Console.WriteLine("DICA: O identificador fica entre parenteses");
            Console.Write("Informe o identificador da tarefa: ");
            int identificador = int.Parse(Console.ReadLine());

            usuario.arquivarTarefa(identificador);
        }

        static void moverTarefa(Usuario usuario) {
            Console.WriteLine("\nSEU QUADRO:");
            usuario.printQuadro();

            Console.WriteLine("DICA: O identificador fica entre parenteses");
            Console.Write("Informe o identificador da tarefa: ");
            int identificador = int.Parse(Console.ReadLine());

            Tarefa tarefa = usuario.getTarefa(identificador);
            Console.WriteLine(string.Format("Sua tarefa está na coluna {0}.", tarefa.getEtapa()));

            Console.Write("Informe a nova etapa da tarefa (1 - Planejada, 2 - Em andamento, 3 - Finalizada ): ");
            int indexNovaEtapa = int.Parse(Console.ReadLine());

            usuario.moverTarefa(identificador, Constants.ETAPAS[indexNovaEtapa - 1]);
        }

        static void visualizarTarefa(Usuario usuario) {
            Console.WriteLine("\nSEU QUADRO:");
            usuario.printQuadro();

            Console.WriteLine("DICA: O identificador fica entre parenteses");
            Console.Write("Informe o identificador da tarefa: ");
            int identificador = int.Parse(Console.ReadLine());

            Tarefa tarefa = usuario.getTarefa(identificador);
            tarefa.printTarefa();
        }

        static void Menu(Usuario usuario) {
            string resposta = "1";

            while(true)
            {
                Console.WriteLine(string.Format(
                    "\nComo posso te ajudar?\n" +
                    "1 - Desejo ver meu quadro\n"+
                    "2 - Adicionar tarefa\n"+
                    "3 - Deletar tarefa\n"+
                    "4 - Mover tarefa\n"+
                    "5 - Visualizar tarefas urgentes\n"+
                    "6 - Visualizar tarefas atrasadas\n"+
                    "7 - Visualizar tarefa\n"+
                    "0 - Sair"
                ));
                Console.Write("DIGITE A SUA ESCOLHA: ");
                resposta = Console.ReadLine();
                switch (resposta)
                {
                    case "1":
                        Console.WriteLine("\nSEU QUADRO:");
                        usuario.printQuadro();
                        break;
                    case "2":
                        criarTarefa(usuario);
                        break;
                    case "3":
                        arquivarTarefa(usuario);
                        break;
                    case "4":
                        moverTarefa(usuario);
                        break;
                    case "5":
                        List<Tarefa> tarefasUrgentes = usuario.getTarefasUrgentes();
                        usuario.printListaTarefas(tarefasUrgentes);
                        break;
                    case "6":
                        List<Tarefa> tarefasAtrasadas = usuario.getTarefasAtradas();
                        usuario.printListaTarefas(tarefasAtrasadas);
                        break;
                    case "7":
                        visualizarTarefa(usuario);
                        break;
                    default:
                        usuario.salvarTarefas();

                        break;
                }

                if(resposta == "0") {

                    break;
                }
            }
        }
    }
}

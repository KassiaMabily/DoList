using System;
using System.Collections.Generic;
using System.IO;
namespace DoList
{
    class Program
    {
        
        static void Main(string[] args)
        {   
            string nomeUsuario;

            string caminhoArquivo = "./data.csv";
            Boolean arquivoExiste = File.Exists(caminhoArquivo);

            if(arquivoExiste) 
            {
                Console.WriteLine("Seja bem-vindo novamente!");
                Console.WriteLine("Você deseja continuar o progresso?\n1 - Sim\n2 - Não, desejo apagar meu progresso");
                string resposta = Console.ReadLine();
                if(resposta == "1")
                {
                    Usuario usuario = new Usuario("Fulaninho");
                    Menu(usuario);
                }

            }
            else {
                Console.WriteLine("Olá, seja bem-vindx!");
                Console.WriteLine("Como devo te chamar? ");
                nomeUsuario = Console.ReadLine();

                Usuario usuario = new Usuario(nomeUsuario);

                Console.WriteLine(string.Format(
                    @"Você ainda não possui tarefas, deseja adicionar agora?
                    1 - Sim
                    2 - Não"
                ));

                string resposta = Console.ReadLine();

                if(resposta == "1") {
                    criarTarefa(usuario);

                    Menu(usuario);
                }
            }

            
        }

        static void criarTarefa(Usuario usuario) {
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
            usuario.printQuadro();

            Console.WriteLine("DICA: O identificador fica entre parenteses");
            Console.Write("Informe o identificador da tarefa: ");
            int identificador = int.Parse(Console.ReadLine());

            usuario.arquivarTarefa(identificador);
        }

        static void moverTarefa(Usuario usuario) {
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
                    @"Como posso te ajudar?
                    1 - Desejo ver meu quadro
                    2 - Adicionar tarefa
                    3 - Deletar tarefa
                    4 - Mover tarefa
                    5 - Visualizar tarefas urgentes
                    6 - Visualizar tarefas atrasadas
                    7 - Visualizar tarefa
                    0 - Sair"
                ));

                resposta = Console.ReadLine();
                switch (resposta)
                {
                    case "1":
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
                        string caminhoArquivo = "./data.csv";
                        Stream arquivo = File.Open(caminhoArquivo, FileMode.OpenOrCreate);
                        StreamReader leitor = new StreamReader(arquivo);

                        usuario.printQuadro();

                        break;
                }

                if(resposta == "0") {

                    break;
                }
            }
        }
    }
}

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
class Usuario {
    private string nome;
    private List<Tarefa> listaTarefas = new List<Tarefa>();

    /// <summary>
    /// Construtor padrão.
    /// </summary>
    public Usuario(string n) {
        nome = n;
    }

    public void salvarTarefas(){
        FileStream meuArq = new FileStream("./data.csv", FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(meuArq, Encoding.UTF8);
        
        for(int i = 0; i < listaTarefas.Count(); i++) {
            sw.WriteLine(listaTarefas[i].formatLineCSV());
        }

        sw.Close();
        meuArq.Close();
    }


    public string getNome(){
        return nome;
    }

    public void setNome(string novoNome){
        nome = novoNome;
    }

    /// <summary>
    /// Adiciona um objeto Tarefa a lista de tarefas
    /// </summary>
    /// <param name="novaTarefa">Nova tarefa</param>
    public void adicionarTarefa(Tarefa novaTarefa) {
        novaTarefa.setIdentificador(listaTarefas.Count()+1);
        listaTarefas.Add(novaTarefa);
        
    }

    /// <summary>
    /// Arquiva uma tarefa da lista de tarefas para ser ignorada no print do quadro kanban
    /// </summary>
    /// <param name="identificadorTarefa">ID único da tarefa</param>
    public Tarefa getTarefa(int identificadorTarefa) {
        Tarefa tarefa = listaTarefas.Find(t => t.getIdentificador() == identificadorTarefa);

        return tarefa;
    }

    /// <summary>
    /// Arquiva uma tarefa da lista de tarefas para ser ignorada no print do quadro kanban
    /// </summary>
    /// <param name="identificadorTarefa">ID único da tarefa</param>
    public void arquivarTarefa(int identificadorTarefa) {
        Tarefa tarefa = getTarefa(identificadorTarefa);
        tarefa.arquivarTarefa(true);
    }

    /// <summary>
    /// Arquiva uma tarefa da lista de tarefas para ser ignorada no print do quadro kanban
    /// </summary>
    /// <param name="identificadorTarefa">ID único da tarefa</param>
    public void moverTarefa(int identificadorTarefa, string novaEtapa) {
        Tarefa tarefa = getTarefa(identificadorTarefa);
        tarefa.moverTarefa(novaEtapa);
    }

    /// <summary>
    /// Filtra a lista de tarefas por etapa
    /// </summary>
    /// <returns> Lista de Tarefas que estão na etapa "Planejada"</returns>
    public List<Tarefa> getTarefasPlanejadas() {
        return listaTarefas.FindAll(tarefa => tarefa.getEtapa() == Constants.ETAPAS[0]);
    }

    /// <summary>
    /// Filtra a lista de tarefas por etapa
    /// </summary>
    /// <returns> Lista de Tarefas que estão na etapa "Em andamento"</returns>
    public List<Tarefa> getTarefasEmAndamento() {
        return listaTarefas.FindAll(tarefa => tarefa.getEtapa() == Constants.ETAPAS[1]);
    }

    /// <summary>
    /// Filtra a lista de tarefas por etapa
    /// </summary>
    /// <returns> Lista de Tarefas que estão na etapa "Finalizadas"</returns>
    public List<Tarefa> getTarefasFinalizadas() {
        return listaTarefas.FindAll(tarefa => tarefa.getEtapa() == Constants.ETAPAS[2]);
    }

    /// <summary>
    /// Filtra a lista de tarefas por prazo de entrega
    /// </summary>
    /// <returns> Lista de Tarefas que estão fora do prazo de entrega</returns>
    public List<Tarefa> getTarefasAtradas() {
        return listaTarefas.FindAll(tarefa => !tarefa.estaDentroPrazo());
    }

    /// <summary>
    /// Filtra a lista de tarefas por prioridade
    /// </summary>
    /// <returns> Lista de Tarefas que possuem prioridade urgente</returns>
    public List<Tarefa> getTarefasUrgentes() {
        return listaTarefas.FindAll(tarefa => tarefa.getPrioridade() == "Urgente");
    }

    /// <summary>
    /// Ordena uma lista de tarefas de acordo com o prazo e a exibe para o usuário
    /// </summary>
    /// <param name="lista">Lista de tarefas a ser exibida</param>
    public void printListaTarefas(List<Tarefa> lista) {

        foreach (var item in lista.OrderBy(tarefa => tarefa.getPrazo()).ToList() )
        {
            Console.WriteLine(item.formatCard());
        }
    }

    /// <summary>
    /// Calcula qual o maior numero de um array de inteiros
    /// </summary>
    /// <param name="numeros">Array de inteiros a serem comparados</param>
    /// <returns>Retorna o maior inteiro no array de inteiros</returns>
    public int getNumeroMaior(int[] numeros) {
        int maior = numeros[0];

        for(int i = 0; i < numeros.Length; i++) {
            if(numeros[i] > maior) {
                maior = numeros[i];
            }
        }

        return maior;
    }

    /// <summary>
    /// Agrupa, ordena e exibe o quadro Kanban
    /// </summary>
    public void printQuadro() {
        // Cria listas de tarefas de acordo com as etapas
        List<Tarefa> tarefasPlanejadas = getTarefasPlanejadas();
        List<Tarefa> tarefasEmAndamento = getTarefasEmAndamento();
        List<Tarefa> tarefasFinalizadas = getTarefasFinalizadas();

        // Pega o tamanho da maior lista
        int[] tamanhos = { tarefasPlanejadas.Count(), tarefasEmAndamento.Count(), tarefasPlanejadas.Count() };
        int qtdLinhas = getNumeroMaior(tamanhos); 

        // Cria uma matriz de tarefas onde as colunas da matriz são as etapas
        List<Tarefa>[] tarefas = new List<Tarefa>[] { tarefasPlanejadas, tarefasEmAndamento, tarefasFinalizadas };

        // Printa o cabeçalho do quadro
        Console.WriteLine("{0, -20} {1, -20} {2, -20}\n", "Planejadas", "Em andamento", "Finalizadas");
        
        // Percorre a matriz
        for(int linha = 0; linha < qtdLinhas ; linha++)
        {
            // Cria uma linha com 3 colunas de valores
            Tarefa[] valores = new Tarefa[] { null, null, null };

            for(int coluna = 0; coluna < 2; coluna++)
            {
                // Verifica se a linha existe na coluna atual da matriz
                if(linha < tarefas[coluna].Count())
                {   
                    // Verifica se a tarefa não está arquivada
                    if(!tarefas[coluna][linha].getEstaArquivada())
                    {
                        // Atribui a tarefa a linha que será printada posteriormente
                        valores[coluna] = tarefas[coluna][linha];
                    }
                }
            
            }

            // Exibe a linha
            Console.WriteLine(
                "{0,-20} {1,-20} {2, -20}\n", 
                valores[0] != null ? valores[0].formatCard() : "", 
                valores[1] != null ? valores[1].formatCard() : "", 
                valores[2] != null ? valores[2].formatCard() : ""
            );
        }
    }
}
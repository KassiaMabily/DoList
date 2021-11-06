using System;
using System.Collections.Generic;
using System.Linq;

class Usuario {
    private string nome;
    private List<Tarefa> listaTarefas = new List<Tarefa>();

    public Usuario(string n) {
        nome = n;
    }

    public string getNome(){
        return nome;
    }

    public void setNome(string novoNome){
        nome = novoNome;
    }

    public void adicionarTarefa(Tarefa novaTarefa) {
        listaTarefas.Add(novaTarefa);
    }

    public void arquivarTarefa(int identificadorTarefa) {
        Tarefa tarefa = listaTarefas.Find(t => t.getIdentificador() == identificadorTarefa);
        tarefa.arquivarTarefa(true);
    }

    public List<Tarefa> getTarefasPlanejadas() {
        return listaTarefas.FindAll(tarefa => tarefa.getEtapa() == "Planejada");
    }

    public List<Tarefa> getTarefasEmAndamento() {
        return listaTarefas.FindAll(tarefa => tarefa.getEtapa() == "Em andamento");
    }

    public List<Tarefa> getTarefasFinalizadas() {
        return listaTarefas.FindAll(tarefa => tarefa.getEtapa() == "Finalizada");
    }

    public List<Tarefa> getTarefasAtradas() {
        return listaTarefas.FindAll(tarefa => !tarefa.estaDentroPrazo());
    }
    public List<Tarefa> getTarefasUrgentes() {
        return listaTarefas.FindAll(tarefa => tarefa.getPrioridade() == "Urgente");
    }

    public void printListaTarefas(List<Tarefa> lista) {
        foreach (var item in lista.OrderBy(tarefa => tarefa.getPrazo()).ToList() )
        {
            Console.WriteLine(item);
        }
    }

    public int getNumeroMaior(int[] numeros) {
        int maior = numeros[0];

        for(int i = 0; i < numeros.Length; i++) {
            if(numeros[i] > maior) {
                maior = numeros[i];
            }
        }

        return maior;
    }

    public void printQuadro() {
        List<Tarefa> tarefasPlanejadas = getTarefasPlanejadas();
        List<Tarefa> tarefasEmAndamento = getTarefasEmAndamento();
        List<Tarefa> tarefasFinalizadas = getTarefasFinalizadas();

        int[] tamanhos = { tarefasPlanejadas.Count(), tarefasEmAndamento.Count(), tarefasPlanejadas.Count() };
        int qtdLinhas = getNumeroMaior(tamanhos); 

        List<Tarefa>[] tarefas = new List<Tarefa>[] { tarefasPlanejadas, tarefasEmAndamento, tarefasFinalizadas };

        Console.WriteLine("{0,-20} {1,-20} {2, -20}\n", "Planejadas", "Em andamento", "Finalizadas");

        for(int linha = 0; linha < qtdLinhas ; linha++)
        {
            Tarefa[] valores = new Tarefa[] { null, null, null };

            for(int coluna = 0; coluna < 2; coluna++)
            {
                if(linha < tarefas[coluna].Count())
                {
                    if(!tarefas[coluna][linha].getEstaArquivada())
                    {
                        valores[coluna] = tarefas[coluna][linha];
                    }
                }
            
            }

            Console.WriteLine(
                "{0,-20} {1,-20} {2, -20}\n", 
                valores[0] != null ? valores[0].formatCard() : "", 
                valores[1] != null ? valores[1].formatCard() : "", 
                valores[2] != null ? valores[2].formatCard() : ""
            );
        }
    }
}
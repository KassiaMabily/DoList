using System;
using System.Linq;
// prioridade: baixa, média e urgente.
// etapa: planejadas, em andamento e finalizadas.
public static class Constants
{
  // DÚVIDA-ver com Prof: const não poderia ser usado nesse caso [no lugar de readonly]? 
  public readonly static string[] ETAPAS = { "Planejadas", "Em andamento", "Finalizadas"};
  public readonly static string[] PRIORIDADES = {"Baixa", "Media", "Urgente"};
}
class Tarefa {
  // private int identificador;
  private int identificador;
  private string titulo;
  private string descricao;
  private string prioridade;
  private DateTime prazo;
  private string etapa;
  private DateTime criadoEm;
  private DateTime atualizadoEm;
  private Boolean estaArquivada;

  public Tarefa()
  {
    //criar um metodo de numeros aleatorios para substituir  0.
    identificador = 0;
    criadoEm = DateTime.Now;
    atualizadoEm = DateTime.Now;
    prazo = DateTime.Now.AddDays(7);
    titulo = "";
    descricao = "";
    prioridade = "Média";
    etapa = Constants.ETAPAS[0];
  }
  
  public Tarefa(int dias, string ptitulo, string desc, string pr)
  {
    //criar um metodo de numeros aleatorios para substituir  0.
    identificador = 0;
    criadoEm = DateTime.Now;
    atualizadoEm = DateTime.Now;
    prazo = DateTime.Now.AddDays(dias);
    titulo = ptitulo;
    descricao = desc;
    prioridade = pr;
    etapa = Constants.ETAPAS[0];
  }
//vanessa - fazer todos getset [para todos atributos privados] 
  
  // Essa operação não deve existir, apenas para fins de teste antes de gerar um id aleatório
  public void setIdentificador(int i){
    identificador = i;
  }

  public string getTitulo() {
    return titulo;
  }

  public int getIdentificador(){
    return identificador;
  }

  public DateTime getPrazo() {
    return prazo;
  }

  public string getPrioridade() {
    return prioridade;
  }

  public string getEtapa() {
    return etapa;
  }

  public Boolean getEstaArquivada() {
    return estaArquivada;
  }

  public void arquivarTarefa(Boolean arquivar) {
    estaArquivada = arquivar;
  }

  public Boolean estaDentroPrazo() {
    int comparacao = DateTime.Compare(prazo, DateTime.Now);
  
    if (comparacao >= 0){
      return true;
    }
      return false;
    }

  public Boolean moverTarefa(string novaEtapa)
  {
    if (Constants.ETAPAS.Contains(novaEtapa))
    {
      etapa = novaEtapa;
      atualizadoEm = DateTime.Now;
      return true;
    }
    else
    {
      Console.WriteLine("Etapa Inválida. Utilize: 'Planejada', 'Em Andamento' ou 'Finalizada'");
      return false;
    }
  }
  public string formatCard ()
  {
    return String.Format("#({0}) {1}", identificador, titulo);
  }
  public void printTarefa (){
    string printTaref = string.Format("#({0}) {1} \n Descricao:{2} \n Prioridade {3} \n Prazo {4} \n Etapa {5} \n Criado em {6} \n Última Atualização {7}",
    identificador, titulo, descricao, prioridade, prazo, etapa, criadoEm, atualizadoEm);
    
    Console.WriteLine(printTaref);
  }
  
}
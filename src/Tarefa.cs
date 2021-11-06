using System;
// prioridade: baixa, média e urgente.
// etapa: planejadas, em andamento e finalizadas.
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
    etapa = "Planejada";
  }
  
  public Tarefa(DateTime dataPrazo, string ptitulo, string desc, string pr)
  {
    //criar um metodo de numeros aleatorios para substituir  0.
    identificador = 0;
    criadoEm = DateTime.Now;
    atualizadoEm = DateTime.Now;
    prazo = dataPrazo;
    titulo = ptitulo;
    descricao = desc;
    prioridade = pr;
    etapa = "Planejada";
  }
//vanessa - fazer todos getset [para todos atributos privados] - visual studio tem padrao para getset 'automatico'. Terminar a instalacao do visual studio
  
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
    return true;
  }

  public void moverTarefa(string novaEtapa){}

  public string formatCard ()
  {
    return String.Format("#({0}) {1}", identificador, titulo);
  }
  public void printTarefa ()
  {
    Console.WriteLine (identificador);
  }
  
}
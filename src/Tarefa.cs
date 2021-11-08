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
    int comparacao = DateTime.Compare(prazo, DateTime.Now);
  
    if (comparacao >= 0){
      return true;
    }
      return false;
    }

  public void moverTarefa(string novaEtapa){}
    //Não consegui fazer esse método. Deixei abaixo comentado a minha última tentativa, mas nao sei se o 'indexof' pode ser usado...
    //string[] etapas = {"planejada", "em andamento", "finalizada"};

    //var search = novaEtapa;
    //if (etapas.IndexOf(search) >= 0){
      //etapa = novaEtapa;
      //atualizadoEm = DateTime.Now;
    //}
      //Console.WriteLine("Etapa Inválida. Utilize: 'planejada', 'em andamento' ou 'finalizada'");      
    //}
  //}

  public string formatCard ()
  {
    return String.Format("#({0}) {1}", identificador, titulo);
  }
  public string printTarefa (){
    string printTaref = string.Format("Id:{0} - Título:{1} / Descricao:{2} / Prioridade {3} / Prazo {4} / Etapa {5} / Criado em {6} / Última Atualização {7}",
    identificador, titulo, descricao, prioridade, prazo, etapa, criadoEm, atualizadoEm);
    
    return printTaref;
  }
  
}
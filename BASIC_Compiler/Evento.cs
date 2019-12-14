    namespace BASIC_Compiler
{
    public class Evento
    {
        public int InstanteProgramado { get; set; }
        public TipoEvento Tipo { get; set; }
        public string Tarefa { get; set; }
        public object Conteudo { get; set; }

        public Evento(int instanteProgramado, TipoEvento tipo, string tarefa, object conteudo)
        {
            InstanteProgramado = instanteProgramado;
            Tipo = tipo;
            Tarefa = tarefa;
            Conteudo = conteudo;
        }
    }
}

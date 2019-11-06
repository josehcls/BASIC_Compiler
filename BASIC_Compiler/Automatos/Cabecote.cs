namespace BASIC_Compiler.Automatos
{
    public class Cabecote
    {
        public string EstadoAtual { get; set; }
        public bool Aceito { get; set; }

        public Cabecote(string estadoInicial)
        {
            EstadoAtual = estadoInicial;
            Aceito = false;
        }
    }
}

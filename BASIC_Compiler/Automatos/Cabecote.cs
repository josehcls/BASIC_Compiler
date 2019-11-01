using System.Collections.Generic;

namespace BASIC_Compiler.Automatos
{
    public class Cabecote
    {
        public LinkedList<string> Fita { get; set; }
        public LinkedListNode<string> PosicaoAtual { get; set; }
        public string EstadoAtual { get; set; }
        public bool Aceito { get; set; }
        public bool Erro { get; set; }

        public Cabecote()
        {
            Aceito = false;
            Erro = false;
        }

        public void MoveParaDireita()
        {
            PosicaoAtual = PosicaoAtual.Next;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BASIC_Compiler
{
    public class Evento
    {
        public int InstanteProgramado { get; set; }
        public string Tipo { get; set; }
        public string Tarefa { get; set; }
        public object Conteudo { get; set; }

        public Evento(int instanteProgramado, string tipo, string tarefa, object conteudo)
        {
            InstanteProgramado = instanteProgramado;
            Tipo = tipo;
            Tarefa = tarefa;
            Conteudo = conteudo;
        }
    }
}

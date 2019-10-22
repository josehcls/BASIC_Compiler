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
        public Object Conteúdo { get; set; }
    }
}

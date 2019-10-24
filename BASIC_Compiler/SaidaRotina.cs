using System;
using System.Collections.Generic;
using System.Text;

namespace BASIC_Compiler
{
    public class SaidaRotina
    {
        public List<Evento> EventosInternos {get; set;}
        public List<Evento> EventosPrioritarios {get; set;}
        public List<Evento> EventosExternos {get; set;}

        public SaidaRotina(List<Evento> enventosInternos, List<Evento> eventosPrioritarios, List<Evento> eventosExternos)
        {
            EventosInternos = eventosExternos;
            EventosPrioritarios = eventosPrioritarios;
            EventosExternos = eventosExternos;
        }
    }
}
        
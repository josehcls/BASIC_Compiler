using System.Collections.Generic;

namespace BASIC_Compiler.MotorDeEventos
{
    public class SaidaRotina
    {
        public List<Evento> EventosInternos { get; set; }
        public List<Evento> EventosPrioritarios { get; set; }
        public List<Evento> EventosExternos { get; set; }

        public SaidaRotina(List<Evento> eventosInternos, List<Evento> eventosPrioritarios, List<Evento> eventosExternos)
        {
            EventosInternos = eventosInternos;
            EventosPrioritarios = eventosPrioritarios;
            EventosExternos = eventosExternos;
        }
    }
}

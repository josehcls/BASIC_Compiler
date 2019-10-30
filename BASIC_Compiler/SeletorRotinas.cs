using System;
using System.Collections.Generic;

namespace BASIC_Compiler
{
    public abstract class SeletorRotinas
    {
        public Dictionary<string, Func<Evento, SaidaRotina>> Rotinas;

        public SeletorRotinas()
        {
            Rotinas = new Dictionary<string, Func<Evento, SaidaRotina>>();
        }

        public SaidaRotina ProcessarEvento(Evento evento)
        {
            return Rotinas[evento.Tipo](evento);
        }

    }
}
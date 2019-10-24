using System;
using System.Collections.Generic;

namespace BASIC_Compiler
{
    public abstract class SeletorRotinas
    {
        public Dictionary<string, Func<Evento, SaidaRotina>> Rotinas;

        public abstract SaidaRotina ProcessarEvento (Evento evento);

    }
}
﻿using System.Collections.Generic;

namespace BASIC_Compiler.Automatos
{
    public abstract class Transicao
    {
        public string Origem;
        public string Destino;

        public abstract bool TransicaoValida(string estadoAtual, object simbolo);
    }
}

﻿using BASIC_Compiler.AnalisadorLexico.Utils;
using System;

namespace BASIC_Compiler.Automatos
{
    public class TransicaoRecategorizadorLexico : Transicao
    {
        public CategoriaTokenLexico Simbolo { get; set; }

        public TransicaoRecategorizadorLexico(string origem, string destino, CategoriaTokenLexico simbolo)
        {
            Origem = origem;
            Destino = destino;
            Simbolo = simbolo;
        }

        public override bool TransicaoValida(string estadoAtual, object simbolo)
        {
            if (Simbolo == CategoriaTokenLexico.WILDCARD)
            {
                // Transição Coringa
                return Origem == estadoAtual;
            }
            else
                return Origem == estadoAtual && Simbolo == (CategoriaTokenLexico)Enum.Parse(typeof(CategoriaTokenLexico), simbolo.ToString());
        }
    }
}

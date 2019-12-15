using BASIC_Compiler.Automatos;
using System;
using System.Collections.Generic;

namespace BASIC_Compiler.AnaliseLexica.Processos.ExtracaoTokens
{
    public class TransicaoExtratorTokens : Transicao
    {
        public List<char> Simbolos { get; set; }

        public TransicaoExtratorTokens(string origem, string destino, List<char> simbolos)
        {
            Origem = origem;
            Destino = destino;
            Simbolos = simbolos;
        }

        public override bool TransicaoValida(string estadoAtual, object simbolo)
        {
            return Origem == estadoAtual && Simbolos.Contains(Convert.ToChar(simbolo));
        }
    }
}

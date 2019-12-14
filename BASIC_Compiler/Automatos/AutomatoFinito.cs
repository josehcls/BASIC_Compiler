using System.Collections.Generic;
using System.Linq;

namespace BASIC_Compiler.Automatos
{
    public class AutomatoFinito
    {
        public List<string> Estados { get; set; }
        public string EstadoInicial { get; set; }
        public List<Transicao> Transicoes { get; set; }
        public List<string> EstadosFinais { get; set; }

        public Transicao BuscaTransicao(string estadoAtual, object simbolo)
        {
            return Transicoes.Where(t => t.TransicaoValida(estadoAtual, simbolo)).SingleOrDefault();
        }

        public bool ConfereEstadoFinal(string estado)
        {
            return EstadosFinais.Contains(estado);
        }

    }
 }


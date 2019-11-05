using BASIC_Compiler.AnalisadorLexico.Utils;
using BASIC_Compiler.Automatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASIC_Compiler.AnalisadorLexico.ExtratorTokens
{
    public class ExtratorTokens : SeletorRotinas
    {
        private List<CaracterClassificado> Acumulador { get; set; }
        private AutomatoFinito AutomatoFinito { get; set; }

        public ExtratorTokens()
        {
            Acumulador = new List<CaracterClassificado>();
            AutomatoFinito = new AutomatoFinito("C://Files/a.txt");
            //Rotinas.Add("ASCII", new Func<Evento, SaidaRotina>(LerLinha));
        }


        /// <summary>
        /// Recebe um Evento contendo uma CaracterCaracterizado
        /// </summary>
        /// <param name="evento">Tipo: ARQUIVO, Conteudo: String (caminho do arquivo)</param>
        /// <returns></returns>


            //  Token possui Tipo (Identificador, Número, Especial) e Valor
        public SaidaRotina RecebeCaracter(Evento evento)
        {
            LinkedList<string> fita = new LinkedList<string>(Acumulador.Select(x => x.Tipo).ToList());



            Cabecote resultadoAutomato = AutomatoFinito.Simulacao(fita);
            if (resultadoAutomato.Aceito)
            {
                string token = Acumulador.Select()
                return new SaidaRotina(
                    new List<Evento>(),
                    new List<Evento> { new Evento(evento.InstanteProgramado + 1, "ASCII", evento.Tarefa, linha) },
                    new List<Evento> { new Evento(evento.InstanteProgramado + 1, "ASCII", evento.Tarefa, caracterClassificado) }
                );
            }
            else if (resultadoAutomato.Erro)
            {

            }
            else
            {
                Acumulador.Add((CaracterClassificado)evento.Conteudo);
                return new SaidaRotina(
                    new List<Evento>(),
                    new List<Evento>(),
                    new List<Evento>()
                );
            }
        }
    }
}

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
        private Cabecote Cabecote { get; set; }

        public ExtratorTokens()
        {
            Acumulador = new List<CaracterClassificado>();
            //AutomatoFinito = new AutomatoFinito("C://Files/a.txt");
            AutomatoFinito = InstanciaAutomato();

            //Rotinas.Add("ASCII", new Func<Evento, SaidaRotina>(LerLinha));
            //Rotinas.Add("LIMPAR_ACUMULADOR", new Func<Evento, SaidaRotina>());
            //Rotinas.Add("EOF", new Func<Evento, SaidaRotina>());
        }

        private AutomatoFinito InstanciaAutomato()
        {
            return new AutomatoFinito()
            {
                EstadoInicial = "START",
                Estados = { "START", "NUMERO", "IDENTIFICADOR", "ESPECIAL" },
                EstadosFinais = { "NUMERO", "IDENTIFICADOR", "ESPECIAL" },
                Transicoes = {
                    new Transicao() { Origem = "START", Destino = "NUMERO", Simbolo = "DIGITO" },
                    new Transicao() { Origem = "NUMERO", Destino = "NUMERO", Simbolo = "DIGITO" },
                    new Transicao() { Origem = "START", Destino = "IDENTIFICADOR", Simbolo = "LETRA" },
                    new Transicao() { Origem = "IDENTIFICADOR", Destino = "IDENTIFICADOR", Simbolo = "LETRA" },
                    new Transicao() { Origem = "IDENTIFICADOR", Destino = "IDENTIFICADOR", Simbolo = "DIGITO" },
                    new Transicao() { Origem = "START", Destino = "ESPECIAL", Simbolo = "ESPECIAL" },
                }
            };
        }

        /// <summary>
        /// Recebe um Evento contendo uma CaracterCaracterizado
        /// </summary>
        /// <param name="evento">Tipo: ASCII, Conteudo: CaracterClassificado </param>
        /// <returns></returns>
            //  Token possui Tipo (Identificador, Número, Especial) e Valor
        public SaidaRotina RecebeCaracter(Evento evento)
        {
            CaracterClassificado caracterClassificado = (CaracterClassificado)evento.Conteudo;
            if (caracterClassificado.Funcao == "DESCARTAVEL")
                return new SaidaRotina(
                    new List<Evento>(),
                    new List<Evento>(),
                    new List<Evento>()
                );

            bool automatoAceito = Cabecote.Aceito;
            Acumulador.Add(caracterClassificado);

            AutomatoFinito.Passo(Cabecote);

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

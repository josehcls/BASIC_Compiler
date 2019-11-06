using BASIC_Compiler.AnalisadorLexico.Utils;
using BASIC_Compiler.Automatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BASIC_Compiler.AnalisadorLexico.ExtratorTokens
{
    public class ExtratorTokens : SeletorRotinas
    {
        private List<CaracterClassificado> Acumulador { get; set; }
        private AutomatoFinito AutomatoFinito { get; set; }
        private Cabecote Cabecote { get; set; }

        public ExtratorTokens()
        {
            AutomatoFinito = InstanciaAutomato();
            Acumulador = new List<CaracterClassificado>();
            Cabecote = new Cabecote(AutomatoFinito.EstadoInicial);

            Rotinas.Add("ASCII", new Func<Evento, SaidaRotina>(ReceberCaracter));
            Rotinas.Add("RESET", new Func<Evento, SaidaRotina>(Reset));
            Rotinas.Add("EOF", new Func<Evento, SaidaRotina>(Eof));
        }


        /// <summary>
        /// Recebe um Evento contendo uma CaracterCaracterizado
        /// </summary>
        /// <param name="evento">Tipo: ASCII, Conteudo: CaracterClassificado </param>
        /// <returns></returns>
            //  Token possui Tipo (Identificador, Número, Especial) e Valor
        public SaidaRotina ReceberCaracter(Evento evento)
        {
            CaracterClassificado caracterClassificado = (CaracterClassificado)evento.Conteudo;

            Transicao transicao = AutomatoFinito.BuscaTransicao(Cabecote.EstadoAtual, caracterClassificado.Tipo);
            if (transicao != null)
            {
                Transicao(transicao);
                if (caracterClassificado.Funcao != "DESCARTAVEL")
                    Acumulador.Add(caracterClassificado);
                return new SaidaRotina(
                    new List<Evento>(),
                    new List<Evento>(),
                    new List<Evento>()
                );
            }
            else
            {
                if (Cabecote.Aceito)
                {
                    return new SaidaRotina(
                        new List<Evento>(),
                        new List<Evento>() { new Evento(evento.InstanteProgramado + 1, "RESET", evento.Tarefa, null), new Evento(evento.InstanteProgramado + 2, "ASCII", evento.Tarefa, evento.Conteudo) },
                        new List<Evento>() { new Evento(evento.InstanteProgramado + 1, "TOKEN", evento.Tarefa, GetToken()) }
                    );
                }
                else
                {
                    // TODO: Reportar Erro!
                    return new SaidaRotina(
                        new List<Evento>(),
                        new List<Evento>(),
                        new List<Evento>()
                    );
                }
            }
        }

        SaidaRotina Reset(Evento evento)
        {
            Acumulador = new List<CaracterClassificado>();
            Cabecote = new Cabecote(AutomatoFinito.EstadoInicial);
            return new SaidaRotina(
                new List<Evento>(),
                new List<Evento>(),
                new List<Evento>()
            );
        }

        SaidaRotina Eof(Evento evento)
        {
            return new SaidaRotina(
                new List<Evento>(),
                new List<Evento>(),
                new List<Evento> { new Evento(evento.InstanteProgramado + 1, "EOF", evento.Tarefa, null) }
            );
        }

        AutomatoFinito InstanciaAutomato()
        {
            return new AutomatoFinito()
            {
                EstadoInicial = "START",
                Estados = new List<string> { "START", "NUMERO", "IDENTIFICADOR", "ESPECIAL" },
                EstadosFinais = new List<string> { "NUMERO", "IDENTIFICADOR", "ESPECIAL" },
                Transicoes = new List<Transicao> {
                    new Transicao() { Origem = "START", Destino = "START", Simbolo = "DELIMITADOR" },
                    new Transicao() { Origem = "START", Destino = "NUMERO", Simbolo = "DIGITO" },
                    new Transicao() { Origem = "NUMERO", Destino = "NUMERO", Simbolo = "DIGITO" },
                    new Transicao() { Origem = "START", Destino = "IDENTIFICADOR", Simbolo = "LETRA" },
                    new Transicao() { Origem = "IDENTIFICADOR", Destino = "IDENTIFICADOR", Simbolo = "LETRA" },
                    new Transicao() { Origem = "IDENTIFICADOR", Destino = "IDENTIFICADOR", Simbolo = "DIGITO" },
                    new Transicao() { Origem = "START", Destino = "ESPECIAL", Simbolo = "ESPECIAL" },
                }
            };
        }

        TokenLexico GetToken()
        {
            return new TokenLexico()
            {
                Valor = string.Concat(Acumulador.Select(cc => cc.Caracter)),
                Tipo = Cabecote.EstadoAtual
            };
        }

        void Transicao(Transicao transicao)
        {
            Cabecote.EstadoAtual = transicao.Destino;
            Cabecote.Aceito = AutomatoFinito.ConfereEstadoFinal(Cabecote.EstadoAtual);
        }

    }
}

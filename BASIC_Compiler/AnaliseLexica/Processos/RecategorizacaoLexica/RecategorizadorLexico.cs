using BASIC_Compiler.AnaliseLexica.Utils.Token;
using BASIC_Compiler.Automatos;
using BASIC_Compiler.MotorDeEventos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BASIC_Compiler.AnaliseLexica.Processos.RecategorizacaoLexica
{
    public class RecategorizadorLexico : SeletorRotinas
    {
        private List<TokenLexico> Acumulador { get; set; }
        private AutomatoFinito AutomatoFinito { get; set; }
        private Cabecote Cabecote { get; set; }

        public RecategorizadorLexico()
        {
            AutomatoFinito = InstanciaAutomato();
            Acumulador = new List<TokenLexico>();
            Cabecote = new Cabecote(AutomatoFinito.EstadoInicial);
            Cabecote.Aceito = AutomatoFinito.ConfereEstadoFinal(Cabecote.EstadoAtual);

            Rotinas.Add(TipoEvento.TOKEN_LEXICO, new Func<Evento, SaidaRotina>(ReceberToken));
            Rotinas.Add(TipoEvento.RESET, new Func<Evento, SaidaRotina>(Reset));
            Rotinas.Add(TipoEvento.EOL, new Func<Evento, SaidaRotina>(Eol));
            Rotinas.Add(TipoEvento.EOF, new Func<Evento, SaidaRotina>(Eof));
        }

        /// <summary>
        /// Recebe um Evento contendo um Token Lexico
        /// </summary>
        /// <param name="evento">Tipo: TOKEN_LEXICO, Conteudo: TokenLexico </param>
        /// <returns></returns>
        public SaidaRotina ReceberToken(Evento evento)
        {
            TokenLexico tokenLexico = (TokenLexico)evento.Conteudo;

            Transicao transicao = AutomatoFinito.BuscaTransicao(Cabecote.EstadoAtual, tokenLexico.Categoria);

            if (transicao != null)
            {
                Transicao(transicao);
                Acumulador.Add(tokenLexico);
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
                        new List<Evento>() { new Evento(evento.InstanteProgramado + 1, TipoEvento.RESET, evento.Tarefa, null), new Evento(evento.InstanteProgramado + 2, TipoEvento.TOKEN_LEXICO, evento.Tarefa, evento.Conteudo) },
                        new List<Evento>() { new Evento(evento.InstanteProgramado + 1, TipoEvento.TOKEN_LEXICO, evento.Tarefa, GetTokenLexicoFromEstadoAtual()) }
                    );
                }
                else
                {
                    List<Evento> eventosSaida = new List<Evento>();
                    eventosSaida.AddRange(Acumulador.Select(t => new Evento(evento.InstanteProgramado + 1, TipoEvento.TOKEN_LEXICO, evento.Tarefa, t)));
                    eventosSaida.Add(new Evento(evento.InstanteProgramado + 2, TipoEvento.TOKEN_LEXICO, evento.Tarefa, evento.Conteudo));

                    return new SaidaRotina(
                        new List<Evento>(),
                        new List<Evento>() { new Evento(evento.InstanteProgramado + 1, TipoEvento.RESET, evento.Tarefa, null) },
                        eventosSaida
                    );
                }
            }
        }

        SaidaRotina Reset(Evento evento)
        {
            Acumulador = new List<TokenLexico>();
            Cabecote = new Cabecote(AutomatoFinito.EstadoInicial);
            Cabecote.Aceito = AutomatoFinito.ConfereEstadoFinal(Cabecote.EstadoAtual);
            return new SaidaRotina(
                new List<Evento>(),
                new List<Evento>(),
                new List<Evento>()
            );
        }

        private SaidaRotina Eol(Evento evento)
        {
            if (Acumulador.Any())
                return new SaidaRotina(
                    new List<Evento>(),
                    new List<Evento>() { new Evento(evento.InstanteProgramado + 1, TipoEvento.RESET, evento.Tarefa, null) },
                    new List<Evento>() { new Evento(evento.InstanteProgramado + 1, TipoEvento.TOKEN_LEXICO, evento.Tarefa, GetTokenLexicoFromEstadoAtual()), new Evento(evento.InstanteProgramado + 2, TipoEvento.EOL, evento.Tarefa, null) }
                );

            else
                return new SaidaRotina(
                    new List<Evento>(),
                    new List<Evento>() { new Evento(evento.InstanteProgramado + 1, TipoEvento.RESET, evento.Tarefa, null) },
                    new List<Evento>() { new Evento(evento.InstanteProgramado + 1, TipoEvento.EOL, evento.Tarefa, null) }
                );
        }

        SaidaRotina Eof(Evento evento)
        {
            return new SaidaRotina(
                new List<Evento>(),
                new List<Evento>(),
                new List<Evento> { new Evento(evento.InstanteProgramado + 1, TipoEvento.EOF, evento.Tarefa, null) }
            );
        }

        AutomatoFinito InstanciaAutomato()
        {
            return new AutomatoFinito()
            {
                EstadoInicial = "START",
                Estados = new List<string> { "START", "GOTO", "PRE_GOTO", "DEF_FN", "PRE_DEF_FN", "MAIOR_IGUAL", "PRE_MAIOR_IGUAL", "MENOR_IGUAL", "PRE_MENOR", "DIFERENTE", "PRE_DIFERENTE", "REMARK" },
                EstadosFinais = new List<string> { "GOTO", "DEF_FN", "MAIOR_IGUAL", "MENOR_IGUAL", "DIFERENTE", "REMARK" },
                Transicoes = new List<Transicao> {
                    new TransicaoRecategorizadorLexico("START", "PRE_GOTO", CategoriaTokenLexico.RESERVADA_GO),
                    new TransicaoRecategorizadorLexico("START", "PRE_DEF_FN", CategoriaTokenLexico.RESERVADA_DEF),
                    new TransicaoRecategorizadorLexico("START", "PRE_MAIOR_IGUAL", CategoriaTokenLexico.ESPECIAL_MAIOR),
                    new TransicaoRecategorizadorLexico("START", "PRE_MENOR", CategoriaTokenLexico.ESPECIAL_MENOR),
                    new TransicaoRecategorizadorLexico("START", "REMARK", CategoriaTokenLexico.RESERVADA_REM),
                    new TransicaoRecategorizadorLexico("PRE_GOTO", "GOTO",  CategoriaTokenLexico.RESERVADA_TO),
                    new TransicaoRecategorizadorLexico("PRE_DEF_FN", "DEF_FN",  CategoriaTokenLexico.RESERVADA_FN),
                    new TransicaoRecategorizadorLexico("PRE_MAIOR_IGUAL", "MAIOR_IGUAL",  CategoriaTokenLexico.ESPECIAL_IGUAL),
                    new TransicaoRecategorizadorLexico("PRE_MENOR", "MENOR_IGUAL",  CategoriaTokenLexico.ESPECIAL_IGUAL),
                    new TransicaoRecategorizadorLexico("PRE_MENOR", "DIFERENTE",  CategoriaTokenLexico.ESPECIAL_MAIOR),
                    new TransicaoRecategorizadorLexico("REMARK", "REMARK", CategoriaTokenLexico.WILDCARD),
                }
            };
        }

        TokenLexico GetTokenLexicoFromEstadoAtual()
        {
            string valor = string.Concat(Acumulador.Select(t => t.Valor));
            TipoTokenLexico tipo = TipoTokenLexico.NA;
            CategoriaTokenLexico categoria = CategoriaTokenLexico.NA;

            switch (Cabecote.EstadoAtual)
            {
                case "GOTO":
                    tipo = TipoTokenLexico.IDENTIFICADOR;
                    categoria = CategoriaTokenLexico.RESERVADA_GOTO;
                    break;
                case "DEF_FN":
                    tipo = TipoTokenLexico.IDENTIFICADOR;
                    categoria = CategoriaTokenLexico.RESERVADA_DEF_FN;
                    break;
                case "MAIOR_IGUAL":
                    tipo = TipoTokenLexico.ESPECIAL;
                    categoria = CategoriaTokenLexico.ESPECIAL_MAIOR_IGUAL;
                    break;
                case "MENOR_IGUAL":
                    tipo = TipoTokenLexico.ESPECIAL;
                    categoria = CategoriaTokenLexico.ESPECIAL_MENOR_IGUAL;
                    break;
                case "DIFERENTE":
                    tipo = TipoTokenLexico.ESPECIAL;
                    categoria = CategoriaTokenLexico.ESPECIAL_DIFERENTE;
                    break;
                case "REMARK":
                    tipo = TipoTokenLexico.TEXTO;
                    categoria = CategoriaTokenLexico.COMENTARIO;
                    break;
            }

            return new TokenLexico() { Valor = valor, Tipo = tipo, Categoria = categoria };
        }

        void Transicao(Transicao transicao)
        {
            Cabecote.EstadoAtual = transicao.Destino;
            Cabecote.Aceito = AutomatoFinito.ConfereEstadoFinal(Cabecote.EstadoAtual);
        }

    }
}

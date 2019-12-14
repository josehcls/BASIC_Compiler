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

        private static List<char> LETRAS = new List<char>().Concat(CaracterClassificado.LETRAS_MINUSCULAS).Concat(CaracterClassificado.LETRAS_MAIUSCULAS).ToList();

        private static List<char> DIGITOS = CaracterClassificado.DIGITOS;

        public static List<char> ESPECIAIS= new List<char> { '=', '<', '>', '(', ')', ',', '*', '/', ',', '^', '"' }; 
        public static List<char> MAIS_MENOS = new List<char> { '+', '-' };
        public static List<char> PONTO = new List<char> { '.' };
        public static List<char> E = new List<char> { 'E' };

        public static List<char> DELIMITADORES = new List<char>().Concat(CaracterClassificado.ESPACADORES).Concat(CaracterClassificado.QUEBRA_LINHA).ToList();

        public ExtratorTokens()
        {
            AutomatoFinito = InstanciaAutomato();
            Acumulador = new List<CaracterClassificado>();
            Cabecote = new Cabecote(AutomatoFinito.EstadoInicial);

            Rotinas.Add(TipoEvento.ASCII, new Func<Evento, SaidaRotina>(ReceberCaracter));
            Rotinas.Add(TipoEvento.RESET, new Func<Evento, SaidaRotina>(Reset));
            Rotinas.Add(TipoEvento.EOF, new Func<Evento, SaidaRotina>(Eof));
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

            Transicao transicao = AutomatoFinito.BuscaTransicao(Cabecote.EstadoAtual, caracterClassificado.Caracter);
            if (transicao != null)
            {
                Transicao(transicao);
                if (caracterClassificado.Funcao != FuncaoCaracter.DESCARTAVEL)
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
                        new List<Evento>() { new Evento(evento.InstanteProgramado + 1, TipoEvento.RESET, evento.Tarefa, null), new Evento(evento.InstanteProgramado + 2, TipoEvento.ASCII, evento.Tarefa, evento.Conteudo) },
                        new List<Evento>() { new Evento(evento.InstanteProgramado + 1, TipoEvento.TOKEN_LEXICO, evento.Tarefa, GetToken()) }
                    );
                }
                else
                {
                    // TODO: Reportar Erro! -- Casos como "3a" não estão caindo aqui.... Ele le como Num "3" e Id "a"
                    Console.WriteLine("ERRO: SEM TRANSIÇÃO");
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
                new List<Evento> { new Evento(evento.InstanteProgramado + 1, TipoEvento.EOF, evento.Tarefa, null) }
            );
        }

        AutomatoFinito InstanciaAutomato()
        {
            return new AutomatoFinito()
            {
                EstadoInicial = "START",
                Estados = new List<string> { "START", "ESPECIAL", "IDENTIFICADOR", "INTEIRO", "ESPECIAL_2", "PRE_DECIMAL", "DECIMAL", "PRE_CIENTIFICO", "PRE_CIENTIFICO_2", "CIENTIFICO" },
                EstadosFinais = new List<string> {"ESPECIAL", "IDENTIFICADOR", "INTEIRO", "ESPECIAL_2", "DECIMAL", "CIENTIFICO"},
                Transicoes = new List<Transicao> {
                    new TransicaoExtratorTokens("START", "START", DELIMITADORES),
                    new TransicaoExtratorTokens("START", "ESPECIAL", ESPECIAIS),
                    new TransicaoExtratorTokens("START", "IDENTIFICADOR", LETRAS),
                    new TransicaoExtratorTokens("START", "INTEIRO", DIGITOS),
                    new TransicaoExtratorTokens("START", "ESPECIAL_2", MAIS_MENOS),
                    new TransicaoExtratorTokens("START", "PRE_DECIMAL", PONTO),
                    new TransicaoExtratorTokens("IDENTIFICADOR", "IDENTIFICADOR", LETRAS),
                    new TransicaoExtratorTokens("IDENTIFICADOR", "IDENTIFICADOR", DIGITOS),
                    new TransicaoExtratorTokens("INTEIRO", "INTEIRO", DIGITOS),
                    new TransicaoExtratorTokens("INTEIRO", "PRE_DECIMAL", PONTO),
                    new TransicaoExtratorTokens("INTEIRO", "PRE_CIENTIFICO_1", E),
                    new TransicaoExtratorTokens("ESPECIAL_2", "INTEIRO", DIGITOS),
                    new TransicaoExtratorTokens("ESPECIAL_2", "PRE_DECIMAL", PONTO),
                    new TransicaoExtratorTokens("PRE_DECIMAL", "DECIMAL", DIGITOS),
                    new TransicaoExtratorTokens("DECIMAL", "DECIMAL", DIGITOS),
                    new TransicaoExtratorTokens("DECIMAL", "PRE_CIENTIFICO_1", E),
                    new TransicaoExtratorTokens("PRE_CIENTIFICO_1", "PRE_CIENTIFICO_2", MAIS_MENOS),
                    new TransicaoExtratorTokens("PRE_CIENTIFICO_2", "CIENTIFICO", DIGITOS),
                    new TransicaoExtratorTokens("CIENTIFICO", "CIENTIFICO", DIGITOS),
                }
            };
        }

        TokenLexico GetToken()
        {
            return new TokenLexico()
            {
                Valor = string.Concat(Acumulador.Select(cc => cc.Caracter)),
                Tipo = GetTipoTokenLexicoFromEstadoAtual()
            };
        }

        TipoTokenLexico GetTipoTokenLexicoFromEstadoAtual()
        {
            TipoTokenLexico tipo = TipoTokenLexico.NA;
            switch (Cabecote.EstadoAtual)
            {
                case "ESPECIAL":
                    tipo = TipoTokenLexico.ESPECIAL;
                    break;
                case "IDENTIFICADOR":
                    tipo = TipoTokenLexico.IDENTIFICADOR;
                    break;
                case "INTEIRO":
                    tipo = TipoTokenLexico.NUMERO_INTEIRO;
                    break;
                case "ESPECIAL_2":
                    tipo = TipoTokenLexico.ESPECIAL;
                    break;
                case "DECIMAL":
                    tipo = TipoTokenLexico.NUMERO_DECIMAL;
                    break;
                case "CIENTIFICO":
                    tipo = TipoTokenLexico.NUMERO_CIENTIFICO;
                    break;
            }
            return tipo;
        }

        void Transicao(Transicao transicao)
        {
            Cabecote.EstadoAtual = transicao.Destino;
            Cabecote.Aceito = AutomatoFinito.ConfereEstadoFinal(Cabecote.EstadoAtual);
        }

    }
}

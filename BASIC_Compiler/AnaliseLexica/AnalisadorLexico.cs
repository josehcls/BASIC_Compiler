using BASIC_Compiler.AnaliseLexica.Processos.ExtracaoTokens;
using BASIC_Compiler.AnaliseLexica.Processos.FiltragemASCII;
using BASIC_Compiler.AnaliseLexica.Processos.LeituraArquivo;
using BASIC_Compiler.AnaliseLexica.Processos.RecategorizacaoLexica;
using BASIC_Compiler.AnaliseLexica.Utils.Caracter;
using BASIC_Compiler.AnaliseLexica.Utils.Token;
using BASIC_Compiler.MotorDeEventos;
using System;
using System.Collections.Generic;

namespace BASIC_Compiler.AnaliseLexica
{
    public class AnalisadorLexico
    {
        public bool Debug { get; set; }

        public AnalisadorLexico(bool debug)
        {
            Debug = debug;
        }

        public List<Evento> ExtrairTokensLexicos(string nomePrograma, string arquivo, HashSet<Simbolo> tabelaDeSimbolos)
        {
            List<Evento> tokensLexicos = new List<Evento>();

            MotorEventos motorRecategorizadorLexico = new MotorEventos(new RecategorizadorLexico(), 0, tokensLexicos);
            MotorEventos motorExtratorTokens = new MotorEventos(new ExtratorTokens(), 0, motorRecategorizadorLexico.Eventos);
            MotorEventos motorFiltroAscii = new MotorEventos(new FiltroAscii(), 0, motorExtratorTokens.Eventos);
            MotorEventos motorLeitorArquivo = new MotorEventos(new LeitorDeArquivo(), 0, motorFiltroAscii.Eventos);

            motorLeitorArquivo.Inicializar(new List<Evento> { new Evento(0, TipoEvento.ARQUIVO, nomePrograma, arquivo) });
            while (motorLeitorArquivo.Rodar()) { }
            Console.WriteLine("------ LEITOR DE ARQUIVOS ------\n");
            motorFiltroAscii.Eventos.ForEach(ev => Console.WriteLine(ev.Tipo + " - " + (ev.Tipo == TipoEvento.EOF ? "EOF" : ev.Tipo == TipoEvento.EOL ? "EOL" : ((string)ev.Conteudo))));
            Console.WriteLine("\n--------------------------------\n\n");

            while (motorFiltroAscii.Rodar()) { }
            Console.WriteLine("------ FILTRO ASCII ------\n");
            motorExtratorTokens.Eventos.ForEach(ev => Console.WriteLine(ev.Tipo + " - " + (ev.Tipo == TipoEvento.EOF ? "EOF" : ev.Tipo == TipoEvento.EOL ? "EOL" : ((CaracterClassificado)ev.Conteudo).Caracter + " " + ((CaracterClassificado)ev.Conteudo).Funcao + " " + ((CaracterClassificado)ev.Conteudo).Tipo)));
            Console.WriteLine("\n--------------------------\n\n");

            while (motorExtratorTokens.Rodar()) { }
            Console.WriteLine("------ EXTRATOR TOKENS ------\n");
            motorRecategorizadorLexico.Eventos.ForEach(ev => Console.WriteLine(ev.Tipo + " - " + (ev.Tipo == TipoEvento.EOF ? "EOF" : ev.Tipo == TipoEvento.EOL ? "EOL" : ((TokenLexico)ev.Conteudo).Categoria + " " + ((TokenLexico)ev.Conteudo).Valor)));
            Console.WriteLine("\n-----------------------------\n\n");

            while (motorRecategorizadorLexico.Rodar()) { }
            Console.WriteLine("------ RECATEGORIZADOR LEXICO ------\n");
            tokensLexicos.ForEach(ev => Console.WriteLine(ev.Tipo + " - " + (ev.Tipo == TipoEvento.EOF ? "EOF" : ev.Tipo == TipoEvento.EOL ? "EOL" : ((TokenLexico)ev.Conteudo).Categoria + " " + ((TokenLexico)ev.Conteudo).Valor)));
            Console.WriteLine("\n-----------------------------\n\n");

            return tokensLexicos;
        }
    }
}

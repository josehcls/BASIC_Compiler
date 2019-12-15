using BASIC_Compiler.AnalisadorLexico.ExtratorTokens;
using BASIC_Compiler.AnalisadorLexico.FiltroASCII;
using BASIC_Compiler.AnalisadorLexico.LeitorDeArquivo;
using BASIC_Compiler.AnalisadorLexico.Utils;
using System;
using System.Collections.Generic;

namespace BASIC_Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Evento> filaFinal = new List<Evento>();

            MotorEventos motorRecategorizadorLexico = new MotorEventos(new RecategorizadorLexico(), 0, filaFinal);
            MotorEventos motorExtratorTokens = new MotorEventos(new ExtratorTokens(), 0, motorRecategorizadorLexico.Eventos);
            MotorEventos motorFiltroAscii = new MotorEventos(new FiltroAscii(), 0, motorExtratorTokens.Eventos);
            MotorEventos motorLeitorArquivo = new MotorEventos(new LeitorDeArquivo(), 0, motorFiltroAscii.Eventos);

            motorLeitorArquivo.Inicializar(new List<Evento> { new Evento(5, TipoEvento.ARQUIVO, "P1", "C://Files/a.txt")});
            while(motorLeitorArquivo.Rodar()) {}
            while(motorFiltroAscii.Rodar()) {}
            while(motorExtratorTokens.Rodar()) {}
            while(motorRecategorizadorLexico.Rodar()) {}

            //filaFinal.ForEach(ev => Console.WriteLine(ev.Tipo + " - " + (ev.Tipo == "EOF" ? "EOF" : ( ((CaracterClassificado)ev.Conteudo).Caracter) + " " + ((CaracterClassificado)ev.Conteudo).Funcao + " " + ((CaracterClassificado)ev.Conteudo).Tipo ) ) );
            //filaFinal.ForEach(ev => Console.WriteLine(ev.Tipo + " - " + (ev.Tipo == TipoEvento.EOF ? "EOF" : ((TokenLexico)ev.Conteudo).Categoria + " " + ((TokenLexico)ev.Conteudo).Valor)));
            filaFinal.ForEach(ev => Console.WriteLine(ev.Tipo + " - " + (ev.Tipo == TipoEvento.EOF ? "EOF" : ((TokenLexico)ev.Conteudo).Categoria + " " + ((TokenLexico)ev.Conteudo).Valor)));

            Console.ReadKey();
        }
    }
}

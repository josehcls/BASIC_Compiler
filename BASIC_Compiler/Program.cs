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

            MotorEventos motorFiltroAscii = new MotorEventos(new FiltroAscii(), 0, filaFinal);
            MotorEventos motorLeitorArquivo = new MotorEventos(new LeitorDeArquivo(), 0, motorFiltroAscii.Eventos);

            motorLeitorArquivo.Inicializar(new List<Evento> { new Evento(5, "ARQUIVO", "P1", "C://Files/a.txt")});
            while(motorLeitorArquivo.Rodar()) {}
            while(motorFiltroAscii.Rodar()) {}

            filaFinal.ForEach(ev => Console.WriteLine(ev.Tipo + " - " + (ev.Tipo == "EOF" ? "EOF" : ( ((CaracterClassificado)ev.Conteudo).Caracter) + " " + ((CaracterClassificado)ev.Conteudo).Funcao + " " + ((CaracterClassificado)ev.Conteudo).Tipo ) ) );

            Console.ReadKey();
        }
    }
}

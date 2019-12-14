using BASIC_Compiler.AnalisadorLexico.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BASIC_Compiler.AnalisadorLexico.FiltroASCII
{
    public class FiltroAscii : SeletorRotinas
    {

        public FiltroAscii()
        {
            Rotinas.Add(TipoEvento.ASCII, new Func<Evento, SaidaRotina>(LerLinha));
            Rotinas.Add(TipoEvento.EOF, new Func<Evento, SaidaRotina>(Eof));
        }

        public SaidaRotina LerLinha(Evento evento)
        {
            string linha = (string)evento.Conteudo;
            if (!String.IsNullOrEmpty(linha))
            {
                CaracterClassificado caracterClassificado = new CaracterClassificado(linha[0]);
                linha = linha.Remove(0, 1);
                return new SaidaRotina(
                    new List<Evento>(),
                    new List<Evento> { new Evento(evento.InstanteProgramado + 1, TipoEvento.ASCII, evento.Tarefa, linha) },
                    new List<Evento> { new Evento(evento.InstanteProgramado + 1, TipoEvento.ASCII, evento.Tarefa, caracterClassificado) }
                );
            }
            else
            {
                return new SaidaRotina(
                    new List<Evento>(),
                    new List<Evento>(),
                    new List<Evento>()
                );
            }
        }

        public SaidaRotina Eof(Evento evento)
        {
            return new SaidaRotina(
                new List<Evento>(),
                new List<Evento>(),
                new List<Evento> { new Evento(evento.InstanteProgramado + 1, TipoEvento.EOF, evento.Tarefa, null) }
            );
        }
    }
}

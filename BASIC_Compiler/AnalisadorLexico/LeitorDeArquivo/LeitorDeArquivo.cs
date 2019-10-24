using System;
using System.Collections.Generic;
using System.IO;

namespace BASIC_Compiler.AnalisadorLexico.NivelUm
{
    public class LeitorDeArquivo : SeletorRotinas
    {

        private StreamReader Arquivo { get; set; }

        public LeitorDeArquivo()
        {
            Rotinas.Add("ARQUIVO", new Func<Evento, SaidaRotina>(AbrirArquivo));
            Rotinas.Add("LER_ARQUIVO", new Func<Evento, SaidaRotina>(LerArquivo));
        }

        override
        public SaidaRotina ProcessarEvento(Evento evento)
        {
            return Rotinas[evento.Tipo](evento);
        }

        /// <summary>
        /// Recebe um Evento contendo uma String com o Caminho de um Arquivo e o Abre, gerando eventos prioritarios de Leitura do Arquivo
        /// </summary>
        /// <param name="evento">Tipo: ARQUIVO, Conteudo: String (caminho do arquivo)</param>
        /// <returns></returns>
        public SaidaRotina AbrirArquivo(Evento evento)
        {
            // IN arquivo
            // Seta arquivo interno
            // Evento Prioritario Ler Linha
            Arquivo = new StreamReader(evento.Conteudo.ToString());
            return new SaidaRotina(
                null, 
                new List<Evento>{new Evento(evento.InstanteProgramado+1, "LER_ARQUIVO", evento.Tarefa, Arquivo)}, 
                null
            );
        }

        /// <summary>
        /// Recebe um StreamReader de um Arquivo e tenta Ler uma Linha. Com sucesso, envia externamente a linha lida e internamente um novo comando de Leitura de Arquivo, com falha, fecha o arquivo e envia externamete um comando EOF
        /// </summary>
        /// <param name="evento">Tipo: LER_ARQUIVO, Conteudo: StreamReader (arquivo aberto)</param>
        /// <returns></returns>
        public SaidaRotina LerArquivo(Evento evento)
        {
            // Le proxima linha do arquivo
            // Evento Prioritário Ler Linha
            // A menos que EOF, então Fechar Arquivo
            // Evento Externo com a Linha Lida ou EOF
            StreamReader streamReader = (StreamReader)evento.Conteudo;
            if (!streamReader.EndOfStream)
            {
                string linha = streamReader.ReadLine();
                return new SaidaRotina(
                    null, 
                    new List<Evento>{new Evento(evento.InstanteProgramado+1, "LER_ARQUIVO", evento.Tarefa, streamReader)}, 
                    new List<Evento>{new Evento(evento.InstanteProgramado+1, "ASCII", evento.Tarefa, linha)}
                );
            } else //EOF
            {
                streamReader.Close();
                return new SaidaRotina(
                    null,
                    null,
                    new List<Evento> { new Evento(evento.InstanteProgramado + 1, "EOF", evento.Tarefa, null) }
                );
            }
        }
    }
}


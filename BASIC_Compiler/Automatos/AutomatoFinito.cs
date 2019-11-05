using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BASIC_Compiler.Automatos
{
    public class AutomatoFinito
    {
        public List<string> Estados { get; set; }
        public string EstadoInicial { get; set; }
        public List<Transicao> Transicoes { get; set; }
        public List<string> EstadosFinais { get; set; }

        public AutomatoFinito (string path)
        {
            var json = System.IO.File.ReadAllText(path);

            AutomatoFinito automato = JsonConvert.DeserializeObject<AutomatoFinito>(json);

            Estados = automato.Estados;
            EstadoInicial = automato.EstadoInicial;
            Transicoes = automato.Transicoes;
            EstadosFinais = automato.EstadosFinais;
        }

        public Cabecote Simulacao(LinkedList<string> fita)
        {
            Cabecote cabecote = new Cabecote(fita);
            Simulacao(cabecote);
            return cabecote;
        }

        public void Simulacao(Cabecote cabecote)
        {
            if (cabecote.EstadoAtual == null)
            {
                PartidaInicial(cabecote);
                Simulacao(cabecote);
            }
            else
            {
                string simbolo = cabecote.PosicaoAtual.Value;

                if (simbolo == "#" && ConfereEstadoFinal(cabecote))
                {
                    cabecote.Aceito = true;
                }
                else
                {
                    var estadoAtual = cabecote.EstadoAtual;
                    var transicoes = BuscaTransicoes(estadoAtual, simbolo);
                    // Salva posição atual para o caso de mais de uma transição
                    var posicao = cabecote.PosicaoAtual;
                    if (!transicoes.Any())
                    {
                        if (simbolo == "#")
                        {
                            if (ConfereEstadoFinal(cabecote))
                            {
                                cabecote.Aceito = true;
                            }
                        }
                        else
                        {
                            cabecote.Erro = true;
                        }
                    }
                    else
                    {
                        foreach (var transicao in transicoes)
                        {
                            // Ignora transição caso a entrada já tenha sido aceita
                            if (!cabecote.Aceito)
                            {
                                // Volta posição para o caso de mais de uma transição
                                cabecote.PosicaoAtual = posicao;
                                RealizaTransicao(cabecote, transicao);
                                // Move Cabeçote para a Direita, caso a transição não tenha sido em vazio
                                if (transicao.Simbolo != " ") cabecote.MoveParaDireita();
                                Simulacao(cabecote);
                            }
                        }
                    }
                }
            }
        }

        void PartidaInicial(Cabecote cabecote)
        {
            cabecote.PosicaoAtual = cabecote.Fita.First;
            cabecote.EstadoAtual = EstadoInicial;
        }

        List<Transicao> BuscaTransicoes(string estadoAtual, string simbolo)
        {
            return Transicoes.Where(t => t.Origem == estadoAtual && (t.Simbolo == simbolo || t.Simbolo == "")).ToList();
        }

        bool ConfereEstadoFinal(Cabecote cabecote)
        {
            return EstadosFinais.Contains(cabecote.EstadoAtual);
        }

        static void RealizaTransicao(Cabecote cabecote, Transicao transicao)
        {
            cabecote.EstadoAtual = transicao.Destino;
        }
    }
    public class Transicao
    {
        public virtual string Origem { get; set; }
        public virtual string Simbolo { get; set; }
        public virtual string Destino { get; set; }
    }
}


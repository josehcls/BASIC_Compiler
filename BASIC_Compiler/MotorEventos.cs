using System;
using System.Collections.Generic;
using System.Text;

namespace BASIC_Compiler
{
    public class MotorEventos
    {
        public Evento EventoCorrente { get; set; }
        public List<Evento> Eventos { get; set; }
        public List<Evento> EventosPrioritarios { get; set; }
        public int InstanteExecucao { get; set; }
        public SeletorRotinas SeletorRotinas { get; set; }
        public List<Evento> FilaSaida { get; set; }
        // TODO: Saida mais generica, como uma fila
        // TODO: Relatorio de execucao

        public MotorEventos(SeletorRotinas seletorRotinas, int instanteExecucao = 0, List<Evento> filaSaida = null)
        {
            EventoCorrente = null;
            Eventos = new List<Evento>();
            EventosPrioritarios = new List<Evento>();
            InstanteExecucao = instanteExecucao;
            SeletorRotinas = seletorRotinas;
            FilaSaida = filaSaida;
        }

        public void Inicializar(List<Evento> eventosIniciais)
        {

        }

        public Evento ExtrairProximoEvento()
        {

        }

        public string Finalizar()
        {

        }

        public void ProcessarEvento(Evento evento)
        {
            SaidaRotina saidaRotina = SeletorRotinas.ProcessarEvento(evento);
            Eventos.AddRange(saidaRotina.EventosInternos);
            EventosPrioritarios.AddRange(saidaRotina.EventosPrioritarios);
            if(FilaSaida != null) FilaSaida.AddRange(saidaRotina.EventosExternos);
        }

        public void ReceberEvento (Evento evento)
        {
            Eventos.Add(evento);
        }

        public void EnviarEvento (Evento evento)
        {
            FilaSaida.Add(evento);
        }

        public void Rodar()
        {

        }
    }
}

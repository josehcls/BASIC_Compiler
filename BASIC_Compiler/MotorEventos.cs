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
        public MotorEventos MotorSaida { get; set; }
        // TODO: Saida mais generica, como uma fila
        // TODO: Relatorio de execucao


        public void Inicializar()
        {

        }

        public Evento ExtrairProximoEvento()
        {

        }

        public string Finalizar()
        {

        }

        public List<Evento> ExecutarEvento(Evento evento)
        {

        }

        public void ReceberEvento (Evento evento)
        {
        
        }

        public void EnviarEvento (Evento evento)
        {
            this.MotorSaida.ReceberEvento(evento);
        }

        public void Rodar()
        {

        }
    }
}

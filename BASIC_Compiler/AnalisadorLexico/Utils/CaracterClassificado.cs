using System;
using System.Collections.Generic;
using System.Text;

namespace BASIC_Compiler.AnalisadorLexico.Utils
{
    public class CaracterClassificado
    {
        public char Caracter { get; set; }
        public string Funcao { get; set; }
        public string Tipo { get; set; }

        public CaracterClassificado(char caracter)
        {
            Caracter = caracter;
            Funcao = ClassificarFuncao(Caracter);
            Tipo = ClassificarTipo(Caracter);
        }

        private string ClassificarFuncao(char caracter)
        {
            string funcao = "NA";
            if ((new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }).Contains(caracter))
                funcao = "UTIL";
            else if ((new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }).Contains(caracter))
                funcao = "UTIL";
            else if ((new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }).Contains(caracter))
                funcao = "UTIL";
            else if ((new List<char>() { ' ', '\t' }).Contains(caracter))
                funcao = "DESCARTAVEL";
            else if ((new List<char>() { ':', ';', '=', '!', '<', '>', '+', '-', '*', '/', ',' }).Contains(caracter))
                funcao = "UTIL";
            else if ((new List<char>() { '\n', '\r' }).Contains(caracter))
                funcao = "CONTROLE";
            return funcao;
        }

        private string ClassificarTipo(char caracter)
        {
            string tipo = "NA";
            if ((new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }).Contains(caracter))
                tipo = "LETRA";
            else if ((new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }).Contains(caracter))
                tipo = "LETRA";
            else if ((new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }).Contains(caracter))
                tipo = "DIGITO";
            else if ((new List<char>() { ' ', '\t' }).Contains(caracter))
                tipo = "DELIMITADOR";
            else if ((new List<char>() { ':', ';', '=', '!', '<', '>', '+', '-', '*', '/', ',' }).Contains(caracter))
                tipo = "ESPECIAL";
            else if ((new List<char>() { '\n', '\r' }).Contains(caracter))
                tipo = "CONTROLE";
            return tipo;
        }
    }

}

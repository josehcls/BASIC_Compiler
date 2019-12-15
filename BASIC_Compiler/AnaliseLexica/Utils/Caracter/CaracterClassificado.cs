using System.Collections.Generic;

namespace BASIC_Compiler.AnaliseLexica.Utils.Caracter
{
    public class CaracterClassificado
    {
        public char Caracter { get; set; }
        public FuncaoCaracter Funcao { get; set; }
        public TipoCaracter Tipo { get; set; }

        public static List<char> LETRAS_MINUSCULAS = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static List<char> LETRAS_MAIUSCULAS = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public static List<char> DIGITOS = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static List<char> ESPACADORES = new List<char> { ' ', '\t' };
        public static List<char> QUEBRA_LINHA = new List<char> { '\n', '\r' };

        public static List<char> ESPECIAIS = new List<char> { '=', '<', '>', '(', ')', '.', ',', '+', '-', '*', '/', ',', '^', '"', };

        public CaracterClassificado(char caracter)
        {
            Caracter = caracter;
            Funcao = ClassificarFuncao(Caracter);
            Tipo = ClassificarTipo(Caracter);
        }

        private FuncaoCaracter ClassificarFuncao(char caracter)
        {
            FuncaoCaracter funcao = FuncaoCaracter.NA;
            if (LETRAS_MINUSCULAS.Contains(caracter))
                funcao = FuncaoCaracter.UTIL;
            else if (LETRAS_MAIUSCULAS.Contains(caracter))
                funcao = FuncaoCaracter.UTIL;
            else if (DIGITOS.Contains(caracter))
                funcao = FuncaoCaracter.UTIL;
            else if (ESPACADORES.Contains(caracter))
                funcao = FuncaoCaracter.DESCARTAVEL;
            else if (ESPECIAIS.Contains(caracter))
                funcao = FuncaoCaracter.UTIL;
            else if (QUEBRA_LINHA.Contains(caracter))
                funcao = FuncaoCaracter.CONTROLE;
            return funcao;
        }

        private TipoCaracter ClassificarTipo(char caracter)
        {
            TipoCaracter tipo = TipoCaracter.NA;
            if (LETRAS_MINUSCULAS.Contains(caracter))
                tipo = TipoCaracter.LETRA;
            if (LETRAS_MAIUSCULAS.Contains(caracter))
                tipo = TipoCaracter.LETRA;
            else if (DIGITOS.Contains(caracter))
                tipo = TipoCaracter.DIGITO;
            else if (ESPACADORES.Contains(caracter))
                tipo = TipoCaracter.DELIMITADOR;
            else if (ESPECIAIS.Contains(caracter))
                tipo = TipoCaracter.ESPECIAL;
            else if (QUEBRA_LINHA.Contains(caracter))
                tipo = TipoCaracter.CONTROLE;
            return tipo;
        }
    }

}

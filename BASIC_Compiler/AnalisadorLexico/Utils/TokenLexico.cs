using System.Text.RegularExpressions;

namespace BASIC_Compiler.AnalisadorLexico.Utils
{
    public class TokenLexico
    {
        public string Valor { get; set; }
        public TipoTokenLexico Tipo { get; set; }
        public CategoriaTokenLexico Categoria { get; set; }

        public static CategoriaTokenLexico CategorizarIdentificador(string valor)
        {
            CategoriaTokenLexico categoria = CategoriaTokenLexico.NA;
            switch (valor)
            {
                case "END":
                    categoria = CategoriaTokenLexico.RESERVADA_END;
                    break;
                case "LET":
                    categoria = CategoriaTokenLexico.RESERVADA_LET;
                    break;
                case "FN":
                    categoria = CategoriaTokenLexico.RESERVADA_FN;
                    break;
                case "SIN":
                    categoria = CategoriaTokenLexico.RESERVADA_SIN;
                    break;
                case "COS":
                    categoria = CategoriaTokenLexico.RESERVADA_COS;
                    break;
                case "TAN":
                    categoria = CategoriaTokenLexico.RESERVADA_TAN;
                    break;
                case "ATN":
                    categoria = CategoriaTokenLexico.RESERVADA_ATN;
                    break;
                case "EXP":
                    categoria = CategoriaTokenLexico.RESERVADA_EXP;
                    break;
                case "ABS":
                    categoria = CategoriaTokenLexico.RESERVADA_ABS;
                    break;
                case "LOG":
                    categoria = CategoriaTokenLexico.RESERVADA_LOG;
                    break;
                case "SQR":
                    categoria = CategoriaTokenLexico.RESERVADA_SQR;
                    break;
                case "INT":
                    categoria = CategoriaTokenLexico.RESERVADA_INT;
                    break;
                case "RND":
                    categoria = CategoriaTokenLexico.RESERVADA_RND;
                    break;
                case "READ":
                    categoria = CategoriaTokenLexico.RESERVADA_READ;
                    break;
                case "DATA":
                    categoria = CategoriaTokenLexico.RESERVADA_DATA;
                    break;
                case "PRINT":
                    categoria = CategoriaTokenLexico.RESERVADA_PRINT;
                    break;
                case "GOTO":
                    categoria = CategoriaTokenLexico.RESERVADA_GOTO;
                    break;
                case "GO":
                    categoria = CategoriaTokenLexico.RESERVADA_GO;
                    break;
                case "TO":
                    categoria = CategoriaTokenLexico.RESERVADA_TO;
                    break;
                case "IF":
                    categoria = CategoriaTokenLexico.RESERVADA_IF;
                    break;
                case "THEN":
                    categoria = CategoriaTokenLexico.RESERVADA_THEN;
                    break;
                case "FOR":
                    categoria = CategoriaTokenLexico.RESERVADA_FOR;
                    break;
                case "STEP":
                    categoria = CategoriaTokenLexico.RESERVADA_STEP;
                    break;
                case "NEXT":
                    categoria = CategoriaTokenLexico.RESERVADA_NEXT;
                    break;
                case "DIM":
                    categoria = CategoriaTokenLexico.RESERVADA_DIM;
                    break;
                case "DEF":
                    categoria = CategoriaTokenLexico.RESERVADA_DEF;
                    break;
                case "GOSUB":
                    categoria = CategoriaTokenLexico.RESERVADA_GOSUB;
                    break;
                case "RETURN":
                    categoria = CategoriaTokenLexico.RESERVADA_RETURN;
                    break;
                case "REM":
                    categoria = CategoriaTokenLexico.RESERVADA_REM;
                    break;
                default:
                    if (Regex.IsMatch(valor, @"[a-zA-Z]"))
                        categoria = CategoriaTokenLexico.IDENTIFICADOR_LETRA;
                    else if (Regex.IsMatch(valor, @"[a-zA-Z]\d?"))
                        categoria = CategoriaTokenLexico.IDENTIFICADOR_LETRA_NUMERO;
                    break;
            }
            return categoria;
        }

       public static CategoriaTokenLexico CategorizarEspecial(string valor)
        {
            CategoriaTokenLexico categoria = CategoriaTokenLexico.NA;
            switch (valor)
            {
                case "=":
                    categoria = CategoriaTokenLexico.ESPECIAL_IGUAL;
                    break;
                case "<":
                    categoria = CategoriaTokenLexico.ESPECIAL_MENOR;
                    break;
                case ">":
                    categoria = CategoriaTokenLexico.ESPECIAL_MAIOR;
                    break;
                case "(":
                    categoria = CategoriaTokenLexico.ESPECIAL_ABRE_PARENTESES;
                    break;
                case ")":
                    categoria = CategoriaTokenLexico.ESPECIAL_FECHA_PARENTESES;
                    break;
                case ",":
                    categoria = CategoriaTokenLexico.ESPECIAL_VIRGULA;
                    break;
                case "+":
                    categoria = CategoriaTokenLexico.ESPECIAL_MAIS;
                    break;
                case "-":
                    categoria = CategoriaTokenLexico.ESPECIAL_MENOS;
                    break;
                case "*":
                    categoria = CategoriaTokenLexico.ESPECIAL_ASTERISCO;
                    break;
                case "/":
                    categoria = CategoriaTokenLexico.ESPECIAL_BARRA;
                    break;
                case "^":
                    categoria = CategoriaTokenLexico.ESPECIAL_CIRCUNFLEXO;
                    break;
                case "\"":
                    categoria = CategoriaTokenLexico.ESPECIAL_ASPAS;
                    break;
            }
            return categoria;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BASIC_Compiler.AnalisadorLexico.Utils
{
    public enum CategoriaTokenLexico
    {
        IDENTIFICADOR,
        NA,

        #region Palavras Reservadas
        RESERVADA_END,
        RESERVADA_LET,
        RESERVADA_FN,
        RESERVADA_SIN,
        RESERVADA_COS,
        RESERVADA_TAN,
        RESERVADA_ATN,
        RESERVADA_EXP,
        RESERVADA_ABS,
        RESERVADA_LOG,
        RESERVADA_SQR,
        RESERVADA_INT,
        RESERVADA_RND,
        RESERVADA_READ,
        RESERVADA_DATA,
        RESERVADA_PRINT,
        RESERVADA_GOTO,
        RESERVADA_GO,
        RESERVADA_TO,
        RESERVADA_IF,
        RESERVADA_THEN,
        RESERVADA_FOR,
        RESERVADA_STEP,
        RESERVADA_NEXT,
        RESERVADA_DIM,
        RESERVADA_DEF,
        RESERVADA_GOSUB,
        RESERVADA_RETURN,
        RESERVADA_REM,
        #endregion

        #region Números
        NUMERO_INTEIRO,
        NUMERO_DECIMAL,
        NUMERO_CIENTIFICO,
        #endregion

        #region
        ESPECIAL_IGUAL,
        ESPECIAL_MAIOR_IGUAL,
        ESPECIAL_MAIOR,
        ESPECIAL_DIFERENTE,
        ESPECIAL_MENOR_IGUAL,
        ESPECIAL_MENOR,
        ESPECIAL_ABRE_PARENTESES,
        ESPECIAL_FECHA_PARENTESES,
        ESPECIAL_VIRGULA,
        ESPECIAL_MAIS,
        ESPECIAL_MENOS,
        ESPECIAL_ASTERISCO,
        ESPECIAL_BARRA,
        ESPECIAL_CIRCUNFLEXO,
        ESPECIAL_ASPAS,
        #endregion

    }
}

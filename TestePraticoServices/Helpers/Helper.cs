using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TestePraticoServices.Helpers
{
    public static class Helper
    {
        public static bool CnpjValido(string cnpj)
        {
            if (!Regex.IsMatch(cnpj, @"(^([0-9]{2}\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}\-[0-9]{2}))"))
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool AgenciaValida(string agencia)
        {
            return (String.IsNullOrEmpty(agencia) || Regex.IsMatch(agencia, @"(^([a-zA-Z0-9]{3}\-[a-zA-Z0-9]{1}))"));
        }

        public static bool ContaValida(string conta)
        {
            return (String.IsNullOrEmpty(conta) || Regex.IsMatch(conta, @"(^([a-zA-Z0-9]{2}\.[a-zA-Z0-9]{3}\-[a-zA-Z0-9]{1}))"));
        }

        public static bool EmailValido(string email)
        {
            return (String.IsNullOrEmpty(email) || Regex.IsMatch(email, @"(^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+))"));
        }
    }
}
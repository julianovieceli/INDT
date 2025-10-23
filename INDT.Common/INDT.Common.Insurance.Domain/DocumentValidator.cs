public static class DocumentValidator
{
    public static bool IsCpfCnpjValid(string documento)
    {
        // Limpa o documento, removendo caracteres não numéricos
        string documentoLimpo = new string(documento.Where(char.IsDigit).ToArray());

        if (string.IsNullOrEmpty(documentoLimpo))
        {
            return false;
        }

        if (documentoLimpo.Length == 11)
        {
            return IsCpfValid(documentoLimpo);
        }
        else if (documentoLimpo.Length == 14)
        {
            return IsCnpjValid(documentoLimpo);
        }

        return false;
    }

    private static bool IsCpfValid(string cpf)
    {
        // Verifica se todos os dígitos são iguais (ex: 111.111.111-11), o que é inválido
        if (new string(cpf[0], 11) == cpf)
        {
            return false;
        }

        // Lógica de validação dos dígitos verificadores do CPF
        int[] multiplicadores1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        string digito = resto.ToString();
        tempCpf += digito;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }

    private static bool IsCnpjValid(string cnpj)
    {
        // Verifica se todos os dígitos são iguais, o que é inválido
        if (new string(cnpj[0], 14) == cnpj)
        {
            return false;
        }

        // Lógica de validação dos dígitos verificadores do CNPJ
        int[] multiplicadores1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores1[i];

        int resto = (soma % 11);
        resto = resto < 2 ? 0 : 11 - resto;
        string digito = resto.ToString();
        tempCnpj += digito;

        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores2[i];

        resto = (soma % 11);
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }
}

using Bogus;
using PontoEstagio.Communication.Request;

public class RequestRegisterCompanyJsonBuilder
{
    public static RequestRegisterCompanytJson Build()
    {
        var faker = new Faker<RequestRegisterCompanytJson>()
            .RuleFor(c => c.Name, f => f.Company.CompanyName()) 
            .RuleFor(c => c.CNPJ, f => GenerateValidCNPJ())   
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("(##) #####-####")) 
            .RuleFor(c => c.Email, f => f.Internet.Email())   
            .RuleFor(c => c.IsActive, f => true);       

        return faker.Generate();
    }

    private static string GenerateValidCNPJ()
    {
        var random = new Random();

        int[] cnpjBase = new int[12];
        for (int i = 0; i < 8; i++) cnpjBase[i] = random.Next(0, 9); // raiz
        cnpjBase[8] = 0;
        cnpjBase[9] = 0;
        cnpjBase[10] = 0;
        cnpjBase[11] = 1;  

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma = 0;
        for (int i = 0; i < 12; i++) soma += cnpjBase[i] * multiplicador1[i];
        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        soma = 0;
        for (int i = 0; i < 12; i++) soma += cnpjBase[i] * multiplicador2[i];
        soma += digito1 * multiplicador2[12];
        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        var cnpj = $"{cnpjBase[0]}{cnpjBase[1]}.{cnpjBase[2]}{cnpjBase[3]}{cnpjBase[4]}." +
                   $"{cnpjBase[5]}{cnpjBase[6]}{cnpjBase[7]}/{cnpjBase[8]}{cnpjBase[9]}{cnpjBase[10]}{cnpjBase[11]}" +
                   $"-{digito1}{digito2}";

        return cnpj;
    }
}

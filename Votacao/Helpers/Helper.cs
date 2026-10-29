namespace SistemaDeVotacao.Helpers;

public class Helper
{
    public static void CriarMenu(string[] opcoes)
    {
        for (var i = 0; i < opcoes.Length; i++)
            Console.WriteLine($"[{i + 1}] {opcoes[i]}");
    }

    public static void Mensagem(string mensagem, bool limpar, bool pausar)
    {
        if (limpar) Console.Clear();
        Console.WriteLine($"{mensagem}");
        if (pausar) Console.ReadKey();
    }

    public static string ValidarSaidaString(string mensagem)
    {   
        string valida;
        while (true)
        {
            Console.Write($"{mensagem}: ");
            if (!string.IsNullOrWhiteSpace(valida = Console.ReadLine()!.Trim())) return valida;

            Console.WriteLine("[ERRO] entrada inválida! \nTente novamente.");
        }
    }

    public static int ValidarSaidaInt(string mensagem)
    {
        while (true)
        {
            Console.Write($"{mensagem}: ");
            if (int.TryParse(Console.ReadLine(), out int valido)) return valido;

            Console.WriteLine("[ERRO] entrada inválida! \nTente novamente.");
        }
    }
}

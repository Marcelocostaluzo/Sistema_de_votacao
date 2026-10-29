using SistemaDeVotacao.Exceptions;

namespace SistemaDeVotacao.Models;

public class Votante
{
    public string Nome { get; private set; }
    public string Matricula { get; private set; }

    public Votante(string? nome, string? matricula)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new NomeNaoPodeSerVazioOuNuloException("Nome não pode ser vazio ou nulo.");
        if (string.IsNullOrWhiteSpace(matricula)) throw new MatriculaNaoPodeSerVazioOuNuloException("Matrícula não pode ser vazio ou nulo.");

        Nome = nome;
        Matricula = matricula;
    }
}

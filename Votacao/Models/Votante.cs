using SistemaDeVotacao.Exceptions;

namespace SistemaDeVotacao.Models;

public class Votante
{
    public string Nome { get; private set; }
    public string Matricula { get; private set; }
    public bool JaVotou { get; private set;} = false;

    public Votante(string? nome, string? matricula)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new NomeNaoPodeSerVazioOuNuloException("Nome não pode ser vazio ou nulo.");
        if (string.IsNullOrWhiteSpace(matricula)) throw new MatriculaNaoPodeSerVazioOuNuloException("Matrícula não pode ser vazia ou nulo.");

        Nome = nome;
        Matricula = matricula;
    }

    public void Votou()
    {
        if (JaVotou) throw new VotanteJaVotouException("O voto já foi feito");
        JaVotou = true;
    }
}

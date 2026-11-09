using SistemaDeVotacao.Exceptions;
using SistemaDeVotacao.Models;

namespace SistemaDeVotacao.Repository;

public class ListaDeVotantes
{
    private List<Votante> _votantes = new List<Votante>();

    public IReadOnlyList<Votante> Registro => _votantes;

    public void Adicionar(Votante votante)
    {
        if (_votantes.Any(v => v.Matricula == votante.Matricula)) throw new MatriculaJaRegistradaException("Esta matrícula já foi cadastrada.");
        _votantes.Add(votante);
    } 

    public Votante? Buscar(string matricula) => _votantes.FirstOrDefault(v => v.Matricula == matricula);

    public void Limpar() => _votantes.Clear();
}

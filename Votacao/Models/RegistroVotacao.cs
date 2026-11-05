namespace SistemaDeVotacao.Models;

public record RegistroVotacao(
    int TotalOpcoes,
    int TotalVotantes,
    string Ganhador,
    int TotalVotosGanhador,
    DateTime DataVotacao
);

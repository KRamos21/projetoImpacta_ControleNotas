using projetoImpacta_ControleNotas.Models;

namespace projetoImpacta_ControleNotas.Middleware
{
    public interface IManipularNotas
    {
        AlunoModel EstabelecerNotaESituacao(AlunoModel alunoModel);
    }
}

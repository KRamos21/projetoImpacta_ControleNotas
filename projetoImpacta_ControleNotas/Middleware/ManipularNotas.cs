using projetoImpacta_ControleNotas.Models;

namespace projetoImpacta_ControleNotas.Middleware
{
    public class ManipularNotas : IManipularNotas
    {
        public AlunoModel EstabelecerNotaESituacao(AlunoModel alunoModel)
        {
            float NotaFinal = (alunoModel.Nota1 + alunoModel.Nota2) / 2;
            alunoModel.NotaFinal = NotaFinal;

            if (alunoModel.NotaFinal >= 6)
            {
                alunoModel.Situacao = "Aprovado";
            }
            else if (alunoModel.NotaFinal > 4 && alunoModel.NotaFinal < 6)
            {
                alunoModel.Situacao = "Recuperação";
            }
            else
            {
                alunoModel.Situacao = "Reprovado";
            }

            return alunoModel;
        }
    }
}

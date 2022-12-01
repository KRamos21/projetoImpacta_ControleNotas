namespace projetoImpacta_ControleNotas.Models
{
    public class AlunoModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public float Nota1 { get; set; }
        public float Nota2 { get; set; }
        public float NotaFinal { get; set; }
        public string Situacao { get; set; }
    }
}

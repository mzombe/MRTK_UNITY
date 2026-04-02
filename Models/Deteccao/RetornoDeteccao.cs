namespace ApiClassificacaoYolo.Models.Deteccao
{
    public class RetornoDeteccao
    {

        public Box? box {  get; set; }
        public string? classe { get; set; }
        public double? confianca { get; set; }
    }
    public class Box
    {
        public float esquerda { get; set; }
        public float direita { get; set; }
        public float topo { get; set; }
        public float embaixo { get; set; }
    }
}

using ApiClassificacaoYolo.Models.Deteccao;

namespace ApiClassificacaoYolo.Models
{
    public class RetornoApi
    {
        public int code { get; set; }
        public string msg { get; set; }
        public List<RetornoDeteccao>? deteccao { get; set; }

    }
}

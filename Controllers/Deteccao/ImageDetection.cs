using ApiClassificacaoYolo.Models;
using ApiClassificacaoYolo.Models.Deteccao;
using ApiClassificacaoYolo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using YoloDotNet.Models;

namespace ApiClassificacaoYolo.Controllers.Deteccao
{
    [Route("Deteccao/[controller]")]
    [ApiController]
    public class ImageDetection : ControllerBase
    {
        private readonly YoloService _yoloService;
        public ImageDetection(YoloService yoloService)
        {
            _yoloService = yoloService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public RetornoApi Post([FromForm] IFormFile image)
        {
            try
            {

                List<ObjectDetection> results = _yoloService.DeteccaoImage(image.OpenReadStream());

                List<RetornoDeteccao> retornoList = results.Select(x => 
                        new RetornoDeteccao()
                        {
                            box = new()
                            {
                                esquerda = x.BoundingBox.Left,
                                direita = x.BoundingBox.Right,
                                topo = x.BoundingBox.Top,
                                embaixo = x.BoundingBox.Bottom
                            },
                            classe = x.Label.Name,
                            confianca = x.Confidence
                        }
                    )
                    .ToList();

                return new()
                {
                    code  = 0,
                    msg = "OK",
                    deteccao = retornoList
                };

            }
            catch (Exception ex)
            {
                return new()
                {
                    code = 10,
                    msg = $"Ocorreu um na detecção da imagem: {ex.Message}"
                };
            }
        }

    }
}

using SkiaSharp;
using YoloDotNet;
using YoloDotNet.Enums;
using YoloDotNet.ExecutionProvider.Cpu;
using YoloDotNet.Models;

namespace ApiClassificacaoYolo.Services
{
    public class YoloService
    {
        private readonly Yolo _yolo;

        public YoloService()
        {
            _yolo = new Yolo(new YoloOptions
            {
                ExecutionProvider = new CpuExecutionProvider("Modelos/yolo26n.onnx")
            });
        }

        public List<ObjectDetection> DeteccaoImage(Stream imageByte)
        {
            using var image = SKBitmap.Decode(imageByte);
            return _yolo.RunObjectDetection(image);
        }

    }
}

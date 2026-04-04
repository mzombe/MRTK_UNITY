# API de Detecção de Objetos com YOLO (ASP.NET)

API REST em **ASP.NET Core** para detecção de objetos em imagens usando **YOLO** via biblioteca `YoloDotNet`.

## ✨ O que este projeto faz

- Recebe uma imagem via `multipart/form-data`.
- Executa detecção de objetos com um modelo YOLO ONNX (`Modelos/yolo26n.onnx`).
- Retorna uma lista com:
  - classe detectada,
  - confiança,
  - coordenadas da bounding box.

## 🧱 Stack utilizada

- .NET `net10.0`
- ASP.NET Core Web API
- `YoloDotNet`
- `YoloDotNet.ExecutionProvider.Cpu`
- `Scalar.AspNetCore` (documentação da API no ambiente de desenvolvimento)

## 📁 Estrutura principal

```txt
Controllers/
  Deteccao/
    ImageDetection.cs      # Endpoint de detecção
Services/
  YoloService.cs           # Carrega modelo e executa inferência
Models/
  RetornoApi.cs            # Envelope da resposta
  Deteccao/
    RetornoDeteccao.cs     # Estruturas de saída da detecção
Modelos/
  yolo26n.onnx             # Modelo ONNX usado pela API
Program.cs                 # Configuração da aplicação
```

## ✅ Pré-requisitos

- SDK do .NET compatível com `net10.0`.
- Arquivo de modelo presente em: `Modelos/yolo26n.onnx`.

> Observação: a inferência está configurada para CPU.

## ▶️ Como executar

```bash
dotnet restore
dotnet run
```

No perfil padrão de desenvolvimento, a API sobe em:

- `http://0.0.0.0:5245`
- `https://0.0.0.0:7250`

## 📚 Documentação/OpenAPI

Em `Development`, os endpoints de OpenAPI/Scalar são mapeados automaticamente.

Após iniciar a API, acesse a referência de API no endpoint configurado pelo Scalar.

## 🧪 Endpoint principal

### `POST /Deteccao/ImageDetection`

**Content-Type:** `multipart/form-data`  
**Campo esperado:** `image` (arquivo)

### Exemplo com cURL

```bash
curl -X POST "http://localhost:5245/Deteccao/ImageDetection" \
  -H "accept: application/json" \
  -H "Content-Type: multipart/form-data" \
  -F "image=@/caminho/para/sua-imagem.jpg"
```

### Exemplo de resposta

```json
{
  "code": 0,
  "msg": "OK",
  "deteccao": [
    {
      "box": {
        "esquerda": 120.5,
        "direita": 280.9,
        "topo": 45.3,
        "embaixo": 300.1
      },
      "classe": "person",
      "confianca": 0.93
    }
  ]
}
```

### Erro

Quando ocorre falha na inferência/processamento:

```json
{
  "code": 10,
  "msg": "Ocorreu um na detecção da imagem: <detalhes>"
}
```

## 🔧 Observações técnicas

- O `YoloService` é registrado como `Singleton`.
- O modelo é carregado na inicialização do serviço.
- A resposta segue o envelope `RetornoApi` com `code`, `msg` e `deteccao`.

## 🚀 Próximos passos (sugestões)

- Validar tipos/tamanho de imagem antes da inferência.
- Adicionar testes automatizados para o endpoint.
- Externalizar caminho do modelo para `appsettings`.
- Incluir versionamento da API e documentação de erros.

---

Se quiser, posso também criar uma versão do README em inglês e adicionar um fluxo de Docker para facilitar execução local e deploy.

using LLama;
using LLama.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace YaznGhanem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class TestLLamaController : ControllerBase
    {
        public TestLLamaController()
        {
            
        }

        [HttpGet ("GetEmbeddings")]
        public string GetEmbeddings(string embeddings_)
        {
            /* 1- Install-Package LLamaSharp
             * 2- install-package LLamaSharp.Backend.Cpu
             * documntation https://github.com/SciSharp/LLamaSharp
             */
            // var modelPath = @"D:\Projects\MVC Projects\learning core\LLama\Models\all-MiniLM-L12-v2.Q8_0.gguf";
            var modelPath = @"all-MiniLM-L12-v2.Q8_0.gguf";
            var @params = new ModelParams(modelPath) { EmbeddingMode = true };
            using var weights = LLamaWeights.LoadFromFile(@params);
            var embedder = new LLamaEmbedder(weights, @params);
            float[] embeddings_result = embedder.GetEmbeddings(embeddings_).Result;

            return string.Join(", ", embeddings_result);
        }
    }
}

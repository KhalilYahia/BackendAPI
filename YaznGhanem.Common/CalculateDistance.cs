using System.Runtime.Intrinsics;

namespace YaznGhanem.Common
{
    public static class CalculateDistance
    {
        /// <summary>
        /// Calculate Cosine Similarity
        /// </summary>
        /// <returns></returns>
        public static double CalCosSimilarity(float[] firstVector, float[] secondVector)
        {
            double dotProduct = 0.0;
            double magnitude1 = 0.0;
            double magnitude2 = 0.0;

            Parallel.For(0, firstVector.Length, i =>
            {
                dotProduct += (firstVector[i] * secondVector[i]);
                magnitude1 += (firstVector[i] * firstVector[i]);
                magnitude2 += (secondVector[i] * secondVector[i]);
            });

            return dotProduct / (Math.Sqrt(magnitude1) * Math.Sqrt(magnitude2));

        }

    }
}

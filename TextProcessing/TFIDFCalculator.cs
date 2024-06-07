using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Crawler.Models;
using Web_Crawler.Repositories;
using Web_Crawler.Services;

namespace Web_Crawler.TextProcessing
{
    public class TFIDFCalculator
    {
        private readonly FrequencyTokenInDescriptions_Repository tokens_repository;
        private readonly ProductRepository productRepository;
        private Dictionary<string, int> termFrequencies = new Dictionary<string, int>();

        public TFIDFCalculator()
        {
            tokens_repository = new FrequencyTokenInDescriptions_Repository();
            productRepository = new ProductRepository();
        }

        public void AnalyzeFrequencies(string[] tokens)
        {
            // Actualizar las frecuencias de los terminos de este texto
            foreach (string token in tokens)
            {
                // Incrementar la frecuencia de término para este texto
                termFrequencies[token] = termFrequencies.ContainsKey(token) ? termFrequencies[token] + 1 : 1;

                if(termFrequencies[token] == 1)
                {
                    // Incrementar la frecuencia del termino en los textos en general
                    if (!tokens_repository.TokenExists(token))
                    {
                        FrequencyTokenInDescriptions newToken = new FrequencyTokenInDescriptions { Token = token, Frequency = 1 };
                        tokens_repository.AddToken(newToken);
                    }
                    else
                    {
                        FrequencyTokenInDescriptions editedToken = tokens_repository.GetToken(token);
                        editedToken.Frequency += 1;
                        tokens_repository.EditToken(editedToken);
                    }
                }
            }
        }   

        public Dictionary<string, double> CalculateTFIDF(string[] tokens)
        {
            Dictionary<string, double> tfidfScores = new Dictionary<string, double>();

            foreach (string token in tokens)
            {
                // Calcular TF
                double tf = (double)termFrequencies[token] / tokens.Length;

                // Calcular IDF
                double idf = Math.Log((double)productRepository.GetProductCount() / tokens_repository.GetFrequencyOfToken(token));

                // Calcular TF-IDF
                tfidfScores[token] = tf * idf;
            }

            return tfidfScores;
        }
    }
}
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Web_Crawler.Models;
using Web_Crawler.Repositories;
using Web_Crawler.TextProcessing;

namespace Web_Crawler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepository prods_repository;
        private static readonly List<string> stopWords = new List<string> {"0", "1", "2", "3", "4", "5", "6", "7", "8",
    "9", ".", ",", "!", "?", "(", ")", "[", "{", "]", "}", "\\", "|", ";", ":", "<", ">", "/", "&", "-", "_", "+", "*", "a", 
    "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "ll", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
    "about", "above", "across", "after", "afterwards", "again", "against", "all", "almost", "alone", "along",
    "already", "also", "although", "always", "am", "among", "amongst", "amoungst", "amount", "an", "and", "another",
    "any", "anyhow", "anyone", "anything", "anyway", "anywhere", "are", "around", "as", "at", "back", "be", "became",
    "because", "become", "becomes", "becoming", "been", "before", "beforehand", "behind", "being", "below", "beside",
    "besides", "between", "beyond", "bill", "both", "bottom", "but", "by", "call", "can", "cannot", "cant", "co",
    "con", "could", "couldnt", "cry", "de", "describe", "detail", "do", "done", "down", "due", "during", "each", "eg",
    "eight", "either", "eleven", "else", "elsewhere", "empty", "enough", "etc", "even", "ever", "every", "everyone",
    "everything", "everywhere", "except", "few", "fifteen", "fify", "fill", "find", "fire", "first", "five", "for",
    "former", "formerly", "forty", "found", "four", "from", "front", "full", "further", "get", "give", "go", "had",
    "has", "hasnt", "have", "he", "hence", "her", "here", "hereafter", "hereby", "herein", "hereupon", "hers",
    "herself", "him", "himself", "his", "how", "however", "hundred", "ie", "if", "in", "inc", "indeed", "interest",
    "into", "is", "it", "its", "itself", "keep", "last", "latter", "latterly", "least", "less", "ltd", "made",
    "many", "may", "me", "meanwhile", "might", "mill", "mine", "more", "moreover", "most", "mostly", "move", "much",
    "must", "my", "myself", "name", "namely", "neither", "never", "nevertheless", "next", "nine", "no", "nobody",
    "none", "noone", "nor", "not", "nothing", "now", "nowhere", "of", "off", "often", "on", "once", "one", "only",
    "onto", "or", "other", "others", "otherwise", "our", "ours", "ourselves", "out", "over", "own", "part", "per",
    "perhaps", "please", "put", "rather", "re", "same", "see", "seem", "seemed", "seeming", "seems", "serious",
    "several", "she", "should", "show", "side", "since", "sincere", "six", "sixty", "so", "some", "somehow", "someone",
    "something", "sometime", "sometimes", "somewhere", "still", "such", "system", "take", "ten", "than", "that", "the",
    "their", "them", "themselves", "then", "thence", "there", "thereafter", "thereby", "therefore", "therein",
    "thereupon", "these", "they", "thick", "thin", "third", "this", "those", "though", "three", "through", "throughout",
    "thru", "thus", "to", "together", "too", "top", "toward", "towards", "twelve", "twenty", "two", "un", "under",
    "until", "up", "upon", "us", "very", "via", "was", "we", "well", "were", "what", "whatever", "when", "whence",
    "whenever", "where", "whereafter", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "which",
    "while", "whither", "who", "whoever", "whole", "whom", "whose", "why", "will", "with", "within", "without",
    "would", "yet", "you", "your", "yours", "yourself", "yourselves", "could", "he", "her", "him", "his", "how",
    "however", "i", "if", "into", "is", "it", "its", "me", "my", "myself", "of", "our", "ours", "ourselves", "she",
    "that", "their", "theirs", "them", "themselves", "they", "us", "we", "what", "whatever", "when", "where", "which",
    "while", "who", "whoever", "whom", "whomever", "whose", "why", "why's", "would", "a", "about", "above", "above",
    "across", "after", "afterwards", "again", "against", "all", "almost", "alone", "along", "already", "also", "although",
    "always", "am", "among", "amongst", "amoungst", "amount", "an", "and", "another", "any", "anyhow", "anyone",
    "anything", "anyway", "anywhere", "are", "around", "as", "at", "back", "be", "became", "because", "become",
    "becomes", "becoming", "been", "before", "beforehand", "behind", "being", "below", "beside", "besides", "between",
    "beyond", "bill", "both", "bottom", "but", "by", "call", "can", "cannot", "cant", "co", "con", "could", "couldnt",
    "cry", "de", "describe", "detail", "do", "done", "down", "due", "during", "each", "eg", "eight", "either",
    "eleven", "else", "elsewhere", "empty", "enough", "etc", "even", "ever", "every", "everyone", "everything",
    "everywhere", "except", "few", "fifteen", "fify", "fill", "find", "fire", "first", "five", "for", "former",
    "formerly", "forty", "found", "four", "from", "front", "full", "further", "get", "give", "go", "had", "has",
    "hasnt", "have", "he", "hence", "her", "here", "hereafter", "hereby", "herein", "hereupon", "hers", "herself",
    "him", "himself", "his", "how", "however", "hundred", "ie", "if", "in", "inc", "indeed", "interest", "into",
    "is", "it", "its", "itself", "keep", "last", "latter", "latterly", "least", "less", "ltd", "made", "many", "may",
    "me", "meanwhile", "might", "mill", "mine", "more", "moreover", "most", "mostly", "move", "much", "must", "my",
    "myself", "name", "namely", "neither", "never", "nevertheless", "next", "nine", "no", "nobody", "none", "noone",
    "nor", "not", "nothing", "now", "nowhere", "of", "off", "often", "on", "once", "one", "only", "onto", "or", "other",
    "others", "otherwise", "our", "ours", "ourselves", "out", "over", "own", "part", "per", "perhaps", "please", "put",
    "rather", "re", "same", "see", "seem", "seemed", "seeming", "seems", "serious", "several", "she", "should", "show",
    "side", "since", "sincere", "six", "sixty", "so", "some", "somehow", "someone", "something", "sometime", "sometimes",
    "somewhere", "still", "such", "system", "take", "ten", "than", "that", "the", "their", "them", "themselves", "then",
    "thence", "there", "thereafter", "thereby", "therefore", "therein", "thereupon", "these", "they", "thick", "thin",
    "third", "this", "those", "though", "three", "through", "throughout", "thru", "thus", "to", "together", "too",
    "top", "toward", "towards", "twelve", "twenty", "two", "un", "under", "until", "up", "upon", "us", "very", "via",
    "was", "we", "well", "were", "what", "whatever", "when", "whence", "whenever", "where", "whereafter", "whereas",
    "whereby", "wherein", "whereupon", "wherever", "whether", "which", "while", "whither", "who", "whoever", "whole",
    "whom", "whose", "why", "will", "with", "within", "without", "would", "yet", "you", "your", "yours", "yourself",
    "yourselves", "could", "he", "her", "him", "his", "how", "however", "i", "if", "into", "is", "it", "its", "me",
    "my", "myself", "of", "our", "ours", "ourselves", "she", "that", "their", "theirs", "them", "themselves", "they",
    "us", "we", "what", "whatever", "when", "where", "which", "while", "who", "whoever", "whom", "whomever", "whose",
    "why", "why's", "would", "product", "description", "producto", "descripcion"};
        private static readonly List<string> noDescriptionMessages = new List<string> { "Producto sin descripcion", "Descripción no encontrada", "Ocurrió un error al obtener la descripción del producto" };

        public HomeController()
        {
            prods_repository = new ProductRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ingresoPost = false;
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult PostIndex()
        {
            string productUrl = HttpUtility.UrlDecode(Request.QueryString["productUrl"]);

            var storedProduct = prods_repository.GetProductByUrl(productUrl);
            if (storedProduct != null)
            {
                ViewBag.KeyWords = storedProduct.Keywords;
            }
            else
            {
                List<string> keyWords;
                string productDescription = ExtractDescriptionFromUrl(productUrl);

                var newProduct = new Product { Url = productUrl, Description = productDescription, Keywords = null };
                prods_repository.AddProduct(newProduct);

                if (!noDescriptionMessages.Contains(productDescription))
                {
                    keyWords = ExtractKeywords(productDescription);

                    StringBuilder sb = new StringBuilder();

                    foreach (var keyword in keyWords)
                    {
                        sb.Append(keyword);
                        sb.Append(", ");
                    }

                    string keyWordsString = sb.ToString();

                    newProduct.Keywords = keyWordsString;
                    prods_repository.EditProduct(newProduct);

                    ViewBag.KeyWords = keyWordsString;
                }
                else
                {

                    ViewBag.KeyWords = null;
                }
            }

            ViewBag.ingresoPost = true;

            return View();
        }

        private string ExtractDescriptionFromUrl(string productUrl)
        {
            // Configurar la ubicación del ChromeDriver
            string driverPath = @"C:\Users\Nahuel-PC\Desktop\CHALLENGE-SIRIUS\Web-Crawler\Web-Crawler\bin\chromedriver.exe";

            // Configurar opciones del navegador Chrome
            ChromeOptions options = new ChromeOptions();

            // Deshabilitar la visualización de la ventana del navegador (headless)
            options.AddArgument("--headless");

            // Configurar un agente de usuario (user agent) para que coincida con un navegador real
            options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");

            // Deshabilitar la carga de imágenes para acelerar la velocidad de carga y reducir el uso de datos
            options.AddUserProfilePreference("profile.managed_default_content_settings.images", 2);

            // Deshabilitar las notificaciones del navegador
            options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);

            // Deshabilitar la detección de Selenium
            options.AddUserProfilePreference("excludeSwitches", new List<string>() { "enable-automation" });

            // Crear una instancia del navegador Chrome con Selenium
            using (IWebDriver driver = new ChromeDriver(driverPath, options))
            {
                try
                {
                    // Navegar a la URL del producto
                    driver.Navigate().GoToUrl(productUrl);

                    // Encontrar el elemento que contiene la descripción del producto
                    IWebElement descriptionElement = driver.FindElement(By.XPath("//div[@id='productDescription']"));
                    if (descriptionElement != null)
                    {
                        // Extraer el texto de la descripción del producto
                        return descriptionElement.Text.Trim();
                    }
                    else
                    {
                        return "Producto sin descripcion";
                    }
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return "Descripción no encontrada";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return "Ocurrió un error al obtener la descripción del producto";
                }
            }
        }

        private List<string> ExtractKeywords(string description)
        {
            // Tokenización
            string[] tokens = Tokenize(description);

            // Filtrado de stop words
            string[] filteredTokens = FilterStopWords(tokens);

            // Calcular TF-IDF
            var tfidfCalculator = new TFIDFCalculator();
            tfidfCalculator.AnalyzeFrequencies(filteredTokens);
            Dictionary<string, double> tfidfScores = tfidfCalculator.CalculateTFIDF(filteredTokens);

            // Ordenar palabras clave por puntajes TF-IDF
            List<string> keywords = tfidfScores.OrderByDescending(kv => kv.Value).Select(kv => kv.Key).Take(15).ToList();

            return keywords;
        }

        private string[] Tokenize(string text)
        {
            return text.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', '!', '?', '(', ')', '[', '{', ']', '}', '\\', '|', ';', ':', '\'', '"', '<', '>', '/' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] FilterStopWords(string[] tokens)
        {
            // Implementación de la lógica de filtrado de stop words
            string[] tokens_without_stopwords = tokens.Where(token => !stopWords.Contains(token.ToLower())).ToArray();
            string[] final_tokens = tokens_without_stopwords.Where(s => !Regex.IsMatch(s, @"^\d+$")).ToArray();

            return final_tokens;
        }
    }
}
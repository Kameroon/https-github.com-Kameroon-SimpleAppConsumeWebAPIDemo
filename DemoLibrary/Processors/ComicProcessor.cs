using DemoLibrary.Helpers;
using DemoLibrary.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLibrary.Processors
{
    public class ComicProcessor
    {
        /// <summary>
        /// -- Async pour prévoir si le service est surchargé --
        /// </summary>
        /// <param name="comicNumber"></param>
        /// <returns></returns>
        public static async Task<ComicModel> LoadComic(int comicNumber = 0)
        {

            string url = "";

            if (comicNumber > 0)
            {
                url = "https://xkcd.com/{comicNumber}/info.0.json";
            }
            else
            {
                url = "https://xkcd.com/info.0.json";
            }

            using (HttpResponseMessage httpResponseMessage = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    ComicModel comicModel = await httpResponseMessage.Content.ReadAsAsync<ComicModel>();

                    return comicModel;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
        }
    }
}

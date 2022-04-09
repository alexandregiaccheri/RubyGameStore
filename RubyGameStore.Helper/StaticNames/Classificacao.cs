using Microsoft.AspNetCore.Hosting;

namespace RubyGameStore.Helper.StaticNames
{
    public class Classificacao
    {
        public string ImagensPath { get; private set; }
        public string _00 { get; private set; }
        public string _10 { get; private set; }
        public string _12 { get; private set; }
        public string _14 { get; private set; }
        public string _16 { get; private set; }
        public string _18 { get; private set; }
        public Classificacao(IWebHostEnvironment webHostEnvironment)
        {
            ImagensPath = webHostEnvironment.ContentRootPath + @"\images\assets\classificacao\";
            _00 = ImagensPath + "00.png";
            _10 = ImagensPath + "10.png";
            _12 = ImagensPath + "12.png";
            _14 = ImagensPath + "14.png";
            _16 = ImagensPath + "16.png";
            _18 = ImagensPath + "18.png";
        }
    }
}

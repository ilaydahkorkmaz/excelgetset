
namespace service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class labres : ControllerBase
    {
        private readonly serviceServiceClient _client;

        public labres(serviceServiceClient client)
        {
            _client = client;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                Tkimlik kimlik = new Tkimlik
                {
                    tckimlik_no = "99999999999",
                    adi = "deneme",
                    soyadi = "deneme",
                    Email = "ilaydahkorkmaz@gmail.com",
                    anne_adi = "anne",
                    baba_adi = "baba",
                    dogumtarihi = "04.08.1999",
                    cins = "E"
                };

                List<Tistekler> istekler = new List<Tistekler>
            {
                new Tistekler
                {
                    sira_no = 1,
                    tarih = "27.08.2024",
                    kodu = 1010,
                    exkodu = "0",
                    aciklama = "",
                    girisyapan = "service",
                    acilmi = "F"
                },
                new Tistekler
                {
                    sira_no = 2,
                    tarih = "27.08.2024",
                    kodu = 1010,
                    exkodu = "0",
                    aciklama = "",
                    girisyapan = "service",
                    acilmi = "F"
                }
            };

                Tistekgiris istekGiris = new Tistekgiris
                {
                    isteyenbarkod = 0,
                    aciklama = "",
                    giris_tipi = "TUMU",
                    kimlik = kimlik,
                    tckimlik_no = long.Parse("99999999999"),
                    dosya_no = 0,
                    istekler = istekler.ToArray(),
                    istek_tarihi = DateTime.Now.ToString("dd.MM.yyyy")
                };

                var kullanici = Base64Encode("ilayda");
                var sifre = Base64Encode("ilayda");
                int hastaneKodu = 212;
                string exBolumKodu = "";

                var response = await _client.istekyapAsync(kullanici, sifre, hastaneKodu, exBolumKodu, istekGiris);
                TIstekSonuc result = response.@return;

                if (result.serviceteslimno != null)
                {
                    return Ok($"Başarılı! Teslim No: {result.serviceteslimno}");
                }
                else
                {
                    return BadRequest("Hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Hata: {ex.Message}");
            }
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
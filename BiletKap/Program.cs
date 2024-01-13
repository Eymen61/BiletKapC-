using System;
using System.Collections.Generic;
using System.Threading;
using System.Net.Mail;
using System.Security.Claims;




public enum BiletTuru                                               //Sabit değerler için enum kullandım.
{
    Standart = 1,
    VIP = 2,
    Ogrenci = 3
}

public abstract class Mac                                           // Maç adında bir soyut sınıf oluşturdum.
{
    public string EvSahibi { get; set; }
    public string Misafir { get; set; }

    public abstract void MacBilgisiGoster();
}

public class FutbolMaci : Mac                                            //Futbol maçı maçtan miras alıyor.
{
    public int EvSahibiSkor { get; set; }
    public int MisafirSkor { get; set; }

    public override void MacBilgisiGoster()                                 // Override ile aşırı bilgi yüklemesi yaptım.
    {
        Console.WriteLine($"Futbol Maçı: {EvSahibi} vs {Misafir} | Skor: {EvSahibiSkor}-{MisafirSkor}");
    }
}

public class BasketbolMaci : Mac                                         //Basketbol maçı maçtan miras alıyor.  
{
    public int EvSahibiSkor { get; set; }
    public int MisafirSkor { get; set; }

    public override void MacBilgisiGoster()                                     // Override ile aşırı bilgi yüklemesi yaptım.
    {
        Console.WriteLine($"Basketbol Maçı: {EvSahibi} vs {Misafir} | Skor: {EvSahibiSkor}-{MisafirSkor}");
        Console.WriteLine("");
    }
}

public class Bilet
{
    public BiletTuru Tur { get; set; }
    public int Fiyat { get; set; }

    public Bilet(BiletTuru tur, int fiyat)
    {
        Tur = tur;
        Fiyat = fiyat;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("*");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*          BiletKap'a Hoş Geldiniz.           *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*                                             *");
        Console.WriteLine("*");
        Thread.Sleep(2000);                                                             //İki saniye bekleyip ekranı kendisi geçecektir.
        Console.Clear();

        string dogruKullaniciAdi = "admin";
        string dogruSifre = "admin";
        string üyeGiris = "üye";
        string üyeSifre = "üye";
        string kullaniciAdi, sifre;

        int girisHakki = 3;
        bool girisBasarili = false;
        bool üyeGirisSorgu = false;

        while (girisHakki > 0)
        {
            Console.WriteLine("Kullanıcı Adınız:");
            kullaniciAdi = Console.ReadLine();

            Console.WriteLine("Şifreniz:");
            sifre = SifreAl();

            if (kullaniciAdi == dogruKullaniciAdi && sifre == dogruSifre)
            {
                girisBasarili = true;                                           //Şİfre doğru ise true döndür.
                break;                                                          //Kır
            }
            else if (kullaniciAdi == üyeGiris && sifre == üyeSifre)
            {
                üyeGirisSorgu = true;                                           //Şİfre doğru ise true döndür.
                break;                                                          //Kır
            }
            else
            {
                Console.WriteLine($"\n\nHatalı kullanıcı adı veya şifre. Kalan giriş hakkı: {--girisHakki}");
                Thread.Sleep(2000);                                                        //İki saniye bekleyip ekranı kendisi geçecektir.
                Console.Clear();
            }
        }
        if (girisBasarili)
        {
            Console.WriteLine($"\nHoş geldiniz, {dogruKullaniciAdi}!");
            Dictionary<BiletTuru, int> futbolBiletStok = new Dictionary<BiletTuru, int>
        {
            { BiletTuru.Standart, 50 },
            { BiletTuru.VIP, 50 },
            { BiletTuru.Ogrenci, 50 }
        };

            Dictionary<BiletTuru, int> basketbolBiletStok = new Dictionary<BiletTuru, int>
        {
            { BiletTuru.Standart, 50 },
            { BiletTuru.VIP, 50 },
            { BiletTuru.Ogrenci, 50 }
        };

            List<Mac> futbolMaclar = new List<Mac>                                  //Maçlar Futbol olarak listeleniyor.
        {
            new FutbolMaci { EvSahibi = "Fenerbahçe", Misafir = "Galatasaray", EvSahibiSkor = 0, MisafirSkor = 0 },

        };

            List<Mac> basketbolMaclar = new List<Mac>                                //Maçlar Basketbol olarak listeleniyor.
        {
            new BasketbolMaci { EvSahibi = "Lakers", Misafir = "Warriors", EvSahibiSkor = 0, MisafirSkor = 0 },

        };

            int yanlisSecimHakki = 3;                                       // 3 kere yanlış işlem yaptığında giriş ekranına yönlendiriliyor.

            while (true)
            {
                Console.WriteLine("1. Futbol Maçı Bileti ");
                Console.WriteLine("2. Basketbol Maçı Bileti ");
                Console.WriteLine("3. Bilet İade Et");
                Console.WriteLine("4. Stok Durumu Göster");
                Console.WriteLine("5. Maçları Göster");
                Console.WriteLine("6. Bilet Stok Güncelle");
                Console.WriteLine("7. Stok Uygulaması Hakkında Bilgi");
                Console.WriteLine("8. Çıkış");
                Console.Write("Seçiminizi yapınız (1-8): ");

                string secim = Console.ReadLine();                              //Seçim yapana kadar ekranda bekliyor.

                switch (secim)
                {
                    case "1":
                        BiletSat(futbolBiletStok, futbolMaclar);
                        break;
                    case "2":
                        BiletSat(basketbolBiletStok, basketbolMaclar);
                        break;
                    case "3":
                        BiletIadeEt(futbolBiletStok, basketbolBiletStok);
                        break;
                    case "4":
                        StokDurumuGoster(futbolBiletStok, basketbolBiletStok);
                        break;
                    case "5":
                        MacGoster(futbolMaclar, basketbolMaclar);
                        break;
                    case "6":
                        StokGuncelle(futbolBiletStok, basketbolBiletStok);
                        break;
                    case "7":
                        StokUygulamasiHakkindaBilgi();
                        break;
                    case "8":
                        Console.WriteLine("*");
                        Console.WriteLine("*                                             *");
                        Console.WriteLine("*         BiletKap'tan Çıkış Yapılıyor.       *");
                        Console.WriteLine("*                 İyi günler...               *");
                        Console.WriteLine("*                                             *");
                        Console.WriteLine("*");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim yaptınız. Lütfen tekrar deneyiniz.");
                        Thread.Sleep(1000);                                                      //1 saniye bekler ve giriş ekranına tekrar döner.
                        Console.Clear();
                        yanlisSecimHakki--;                                                      //Deneme hakkı birer birer azalır.

                        if (yanlisSecimHakki == 0)
                        {
                            Console.WriteLine("Üzgünüz, üç kez yanlış seçim yaptınız. Giriş ekranına yönlendiriliyorsunuz.");       // 3 kere yanlış işlem yaptığında giriş ekranına yönlendiriliyor.
                            Thread.Sleep(2000);                                                                                     //2 saniye bekler ve giriş ekranına tekrar döner.    
                            Console.Clear();
                            yanlisSecimHakki = 3;
                            continue;
                        }
                        break;
                }
            }
        }
        else if (üyeGirisSorgu)                                      //Üyelerin giriş yapabileceği bölüm
        {
            Console.WriteLine($"\nHoş geldiniz, {üyeGiris}!");

            Dictionary<BiletTuru, int> futbolBiletStok = new Dictionary<BiletTuru, int>                  //dictionary eklenen elemanları key ve value olarak kayıt eder.
        {
            { BiletTuru.Standart, 50 },
            { BiletTuru.VIP, 50 },
            { BiletTuru.Ogrenci, 50 }
        };

            Dictionary<BiletTuru, int> basketbolBiletStok = new Dictionary<BiletTuru, int>               //dictionary eklenen elemanları key ve value olarak kayıt eder.
        {
            { BiletTuru.Standart, 50 },
            { BiletTuru.VIP, 50 },
            { BiletTuru.Ogrenci, 50 }
        };

            List<Mac> futbolMaclar = new List<Mac>
        {
            new FutbolMaci { EvSahibi = "Fenerbahçe", Misafir = "Galatasaray", EvSahibiSkor = 0, MisafirSkor = 0 },

        };

            List<Mac> basketbolMaclar = new List<Mac>
        {
            new BasketbolMaci { EvSahibi = "Lakers", Misafir = "Warriors", EvSahibiSkor = 0, MisafirSkor = 0 },

        };

            int yanlisSecimHakki = 3;

            while (true)                        //Üye girişinde stokları güncelleme seçimi bulunmuyor,bu seçim hakkı sadece admine verilir.
            {
                Console.WriteLine("1. Futbol Maçı Bileti ");
                Console.WriteLine("2. Basketbol Maçı Bileti ");
                Console.WriteLine("3. Bilet İade Et");
                Console.WriteLine("4. Stok Durumu Göster");
                Console.WriteLine("5. Maçları Göster");

                Console.WriteLine("6. Stok Uygulaması Hakkında Bilgi");
                Console.WriteLine("7. Çıkış");
                Console.Write("Seçiminizi yapınız (1-7): ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        BiletSat(futbolBiletStok, futbolMaclar);
                        break;
                    case "2":
                        BiletSat(basketbolBiletStok, basketbolMaclar);
                        break;
                    case "3":
                        BiletIadeEt(futbolBiletStok, basketbolBiletStok);
                        break;
                    case "4":
                        StokDurumuGoster(futbolBiletStok, basketbolBiletStok);
                        break;
                    case "5":
                        MacGoster(futbolMaclar, basketbolMaclar);
                        break;
                    case "6":
                        StokUygulamasiHakkindaBilgi();
                        break;
                    case "7":
                        Console.WriteLine("*");
                        Console.WriteLine("*                                             *");
                        Console.WriteLine("*         BiletKap'tan Çıkış Yapılıyor.       *");
                        Console.WriteLine("*                 İyi günler...               *");
                        Console.WriteLine("*                                             *");
                        Console.WriteLine("*");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim yaptınız. Lütfen tekrar deneyiniz.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        yanlisSecimHakki--;

                        if (yanlisSecimHakki == 0)
                        {
                            Console.WriteLine("Üzgünüz, üç kez yanlış seçim yaptınız. Giriş ekranına yönlendiriliyorsunuz.");
                            Thread.Sleep(2000);
                            Console.Clear();
                            yanlisSecimHakki = 3;
                            continue;
                        }
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Giriş hakkınız tükendi. Uygulama kapatılıyor.");
            Thread.Sleep(1000);
            Console.Clear();
            return;
        }


    }

    static void BiletSat(Dictionary<BiletTuru, int> biletStok, List<Mac> maclar)                //dictionary eklenen elemanları key ve value olarak kayıt eder.
    {
        Console.Clear();
        Console.WriteLine("Mevcut Maçlar:");
        for (int i = 0; i < maclar.Count; i++)                                                  //Count verinin sayısını ölçer.
        {
            Console.WriteLine($"{i + 1}. ");
            maclar[i].MacBilgisiGoster();
        }

        Console.Write("Bilet almak istediğiniz maçı seçiniz. (1): ");
        string secilenMacNo = Console.ReadLine();

        if (int.TryParse(secilenMacNo, out int macIndex) && (macIndex == 1))                    //32 bit tam sayıya dönüştürmeye yarar tryparse.
        {
            Mac seciliMac = maclar[macIndex - 1];                                       // Liste sıfır tabanlı olduğu için indeksi bir eksiltiyoruz.

            Console.Write($"Bilet türünü seçiniz (1: Standart (60TL), 2: VIP (100TL), 3: Öğrenci (40TL),): ");
            string secilenBiletTuru = Console.ReadLine();

            if (Enum.TryParse(secilenBiletTuru, out BiletTuru biletTuru) && (secilenBiletTuru == "1" || secilenBiletTuru == "2" || secilenBiletTuru == "3"))
            {
                Console.Write("Satış adedini giriniz: ");
                int satisAdedi;

                while (!int.TryParse(Console.ReadLine(), out satisAdedi) || satisAdedi < 0)             // Girilen sayı negatif değer ise çalıştır.
                {
                    Console.WriteLine("Geçersiz satış adedi girdiniz. Pozitif bir tam sayı giriniz.");
                    Console.Write("Satış adedini giriniz: ");
                }

                if (biletStok[biletTuru] >= satisAdedi)
                {
                    biletStok[biletTuru] -= satisAdedi;
                    int toplamFiyat = satisAdedi * BiletFiyati(biletTuru);                              //Bilet satıldıkça bilet adedi eksilir.
                    Console.WriteLine($"{satisAdedi} adet {GetBiletTuruString(biletTuru)} bileti satıldı. Toplam Tutar: {toplamFiyat} TL. Kalan stok: {biletStok[biletTuru]}");
                    /*MailMessage ePosta = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    smtp.Credentials = new System.Net.NetworkCredential("eymenaydin386@gmail.com", "eymen6161");
                    smtp.Host = "smtp.live.com";
                    smtp.Port = 587;                                                    //Güvenlik hatası verdiği için maili yollamıyor hocam bu yüzden
                    smtp.EnableSsl = true;                                                        //Kodu yorum satırına aldık.

                    ePosta.From = new MailAddress("eymenaydin386@gmail.com");
                    ePosta.Subject = "BiletKap";
                    ePosta.Body = $"{satisAdedi} adet bilet hesabınıza geçmiştir";
                    ePosta.To.Add("eymena071@gmail.com");
                    smtp.Send(ePosta);*/
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Yetersiz stok. İşlem gerçekleştirilemedi.");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Geçersiz bilet türü seçimi yaptınız. İşlem gerçekleştirilemedi.");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }
        else
        {
            Console.WriteLine("Geçersiz maç seçimi yaptınız. İşlem gerçekleştirilemedi.");
            Thread.Sleep(2000);
            Console.Clear();
        }
    }

    static void BiletIadeEt(Dictionary<BiletTuru, int> futbolBiletStok, Dictionary<BiletTuru, int> basketbolBiletStok)      //Bİlet iadesi ile ilgili kodlar.
    {
        Console.Clear();
        Console.WriteLine("Hangi maçın biletini iade etmek istiyorsunuz?");
        Console.WriteLine("1. Futbol Maçı");
        Console.WriteLine("2. Basketbol Maçı");

        Console.Write("Maç seçimi (1 veya 2): ");
        string secilenMacNo = Console.ReadLine();

        if (int.TryParse(secilenMacNo, out int macIndex) && (macIndex == 1 || macIndex == 2))
        {
            Dictionary<BiletTuru, int> secilenMacBiletStok = (macIndex == 1) ? futbolBiletStok : basketbolBiletStok;        // Bu kod parçasındaki soru işareti, C# programlama dilinde üç operatörlü koşul operatörünün bir parçasıdır. Bu operatör, bir koşulu değerlendirir ve koşul doğruysa bir değeri, yanlışsa başka bir değeri döndürür.      

            Console.WriteLine("Bilet Türleri:");
            Console.WriteLine("1. Standart");
            Console.WriteLine("2. VIP");
            Console.WriteLine("3. Öğrenci");

            Console.Write("İade edilecek bilet türünü seçiniz. (1, 2 veya 3): ");
            string secilenBiletNo = Console.ReadLine();

            if (int.TryParse(secilenBiletNo, out int biletIndex) && (biletIndex >= 1 && biletIndex <= 3))
            {
                BiletTuru biletTuru = (BiletTuru)biletIndex;

                Console.Write("İade adedini giriniz: ");
                int iadeAdedi;

                while (!int.TryParse(Console.ReadLine(), out iadeAdedi) || iadeAdedi < 0)           // Girilen sayı negatif ise döngü çalışır.
                {
                    Console.WriteLine("Geçersiz adet. Pozitif bir tam sayı giriniz.");              // İade işlemleriyle ilgili kod bloklarının bulunduğu bölüm.
                    Console.Write("İade adedini giriniz: ");
                }

                if (iadeAdedi > 0)
                {
                    int yeniStok = secilenMacBiletStok.ContainsKey(biletTuru) ? secilenMacBiletStok[biletTuru] + iadeAdedi : iadeAdedi;

                    if (yeniStok > 50)
                    {
                        Console.WriteLine("Hata: İade sonrası stok 50'den fazla olamaz.");
                        return;
                    }

                    secilenMacBiletStok[biletTuru] = yeniStok;

                    int iadeTutar = iadeAdedi * BiletFiyati(biletTuru);
                    Console.WriteLine($"{iadeAdedi} adet {GetBiletTuruString(biletTuru)} bileti iade edildi. İade Tutarı: {iadeTutar} TL. Yeni stok: {GetBiletStok(biletTuru, futbolBiletStok, basketbolBiletStok)}");
                    Console.Clear();                                                    //Ekrandaki işlem kalabalığını engellemek için Console.Clear() çalıştırıyoruz.
                }
                else
                {
                    Console.WriteLine("Hata: İade adedi sıfır veya negatif olamaz.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz bilet seçimi yaptınız. İşlem gerçekleştirilemedi.");
                Thread.Sleep(2000);
                Console.Clear();                                                        //Ekrandaki işlem kalabalığını engellemek için Console.Clear() çalıştırıyoruz.
            }
        }
        else
        {
            Console.WriteLine("Geçersiz maç seçimi yaptınız. İşlem gerçekleştirilemedi.");
            Thread.Sleep(2000);
            Console.Clear();                                                            //Ekrandaki işlem kalabalığını engellemek için Console.Clear() çalıştırıyoruz.
        }
    }





    static void StokDurumuGoster(Dictionary<BiletTuru, int> futbolBiletStok, Dictionary<BiletTuru, int> basketbolBiletStok)
    {
        Console.Clear();
        Console.WriteLine("Futbol Maçı Bilet Stok Durumu:");
        foreach (var kvp in futbolBiletStok)                                    //her döngü iterasyonunda koleksiyonun bir sonraki elemanına otomatik olarak geçer.
        {
            Console.WriteLine($"{GetBiletTuruString(kvp.Key)}: {kvp.Value}");
            Console.WriteLine("*");
        }

        Console.WriteLine("\nBasketbol Maçı Bilet Stok Durumu:");
        foreach (var kvp in basketbolBiletStok)                                 //her döngü iterasyonunda koleksiyonun bir sonraki elemanına otomatik olarak geçer.
        {
            Console.WriteLine($"{GetBiletTuruString(kvp.Key)}: {kvp.Value}");
            Console.WriteLine("*");
        }
    }

    static void StokGuncelle(Dictionary<BiletTuru, int> futbolBiletStok, Dictionary<BiletTuru, int> basketbolBiletStok)
    {
        Console.Clear();                                            //Ekrandaki işlem kalabalığını engellemek için Console.Clear() çalıştırıyoruz.
        Console.WriteLine("Stok Güncelleme");
        Console.WriteLine("1. Futbol Maçı Bileti");
        Console.WriteLine("2. Basketbol Maçı Bileti");

        Console.Write("Stokunu güncellemek istediğiniz maçı seçiniz. (1 veya 2): ");
        string secilenMacNo = Console.ReadLine();

        if (int.TryParse(secilenMacNo, out int macIndex) && (macIndex == 1 || macIndex == 2))
        {
            Console.Write("Stokunu güncellemek istediğiniz bilet türünü seçiniz (1: Standart, 2: VIP, 3: Öğrenci): ");
            string secilenBiletTuru = Console.ReadLine();

            if (Enum.TryParse(secilenBiletTuru, out BiletTuru biletTuru) && (secilenBiletTuru == "1" || secilenBiletTuru == "2" || secilenBiletTuru == "3"))
            {
                Console.Write("Yeni stok miktarını giriniz: ");
                int yeniStok;

                while (!int.TryParse(Console.ReadLine(), out yeniStok) || yeniStok < 0)
                {
                    Console.WriteLine("Geçersiz stok miktarı. Pozitif bir tam sayı giriniz.");
                    Console.Write("Yeni stok miktarını giriniz: ");
                }

                if (macIndex == 1 && futbolBiletStok.ContainsKey(biletTuru))
                {
                    futbolBiletStok[biletTuru] = yeniStok;
                }
                else if (macIndex == 2 && basketbolBiletStok.ContainsKey(biletTuru))          //Dictionary nesnesinin belirli bir anahtarın koleksiyon içinde olup olmadığını kontrol etmek için kullanılır.
                {
                    basketbolBiletStok[biletTuru] = yeniStok;
                }

                Console.WriteLine($"{GetBiletTuruString(biletTuru)} bilet stok miktarı güncellendi. Yeni stok: {GetBiletStok(biletTuru, futbolBiletStok, basketbolBiletStok)}");
                Thread.Sleep(2000);
                Console.Clear();                                                //Ekrandaki işlem kalabalığını engellemek için Console.Clear() çalıştırıyoruz.
            }
            else
            {
                Console.WriteLine("Geçersiz bilet türü seçimi yaptınız. İşlem gerçekleştirilemedi.");
                Thread.Sleep(2000);
                Console.Clear();                                                //Ekrandaki işlem kalabalığını engellemek için Console.Clear() çalıştırıyoruz.
            }
        }
        else
        {
            Console.WriteLine("Geçersiz maç seçimi yaptınız. İşlem gerçekleştirilemedi.");
            Thread.Sleep(2000);
            Console.Clear();                                                    //Ekrandaki işlem kalabalığını engellemek için Console.Clear() çalıştırıyoruz.
        }
    }

    static int GetBiletStok(BiletTuru biletTuru, Dictionary<BiletTuru, int> futbolBiletStok, Dictionary<BiletTuru, int> basketbolBiletStok)
    {
        if (futbolBiletStok.ContainsKey(biletTuru))
        {
            return futbolBiletStok[biletTuru];
        }
        else if (basketbolBiletStok.ContainsKey(biletTuru))
        {
            return basketbolBiletStok[biletTuru];
        }

        return 0;
    }

    static string SifreAl()
    {
        string sifre = "";
        do
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                sifre += key.KeyChar;
                Console.Write("*");                                     //Şİfrenin yıldız şeklinde görünmesini sağlar
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && sifre.Length > 0)
                {
                    sifre = sifre.Substring(0, (sifre.Length - 1));
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        } while (true);

        return sifre;
    }

    static int BiletFiyati(BiletTuru biletTuru)
    {
        switch (biletTuru)
        {
            case BiletTuru.Standart:
                return 60;
            case BiletTuru.VIP:
                return 100;
            case BiletTuru.Ogrenci:
                return 40;
            default:
                return 0;                                           //Bulunmuyorsa varsayılan olarak 0 değerini döndürür.
        }
    }

    static void MacGoster(List<Mac> maclar, List<Mac> basketbolMaclar)       //Lİstelenmiş olan maçları gösterir.
    {
        Console.Clear();                                            //Ekrandaki işlem kalabalığını engellemek için Console.Clear() çalıştırıyoruz.
        Console.WriteLine("Mevcut Maçlar:");
        for (int i = 0; i < maclar.Count; i++)
        {
            Console.WriteLine($"{i + 1}. ");
            maclar[i].MacBilgisiGoster();
        }
        for (int i = 0; i < basketbolMaclar.Count; i++)
        {
            Console.WriteLine($"{i + 2}. ");
            basketbolMaclar[i].MacBilgisiGoster();
        }
    }

    static string GetBiletTuruString(BiletTuru biletTuru)
    {
        switch (biletTuru)
        {
            case BiletTuru.Standart:
                return "Standart";
            case BiletTuru.VIP:
                return "VIP";
            case BiletTuru.Ogrenci:
                return "Öğrenci";
            default:
                return string.Empty;
        }
    }

    static void StokUygulamasiHakkindaBilgi()                       //Uygulama hakkında bilgi ekranı bölümü.
    {
        Console.Clear();
        Console.WriteLine("Stok Uygulaması Hakkında Bilgi");
        Console.WriteLine("*");
        Console.WriteLine("Bu uygulama, BiletKap adlı bir bilet satış sistemini simüle etmektedir.");
        Console.WriteLine("Kullanıcılar bilet alabilir, iade edebilir ve stok durumunu güncelleyebilir.");
        Console.WriteLine("Her bilet türü için ayrı stok miktarı bulunmaktadır.");
        Console.WriteLine("Bilet fiyatları standart, VIP ve öğrenci kategorileri için farklıdır.");
        Console.WriteLine("*");
        Console.WriteLine("Ana menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
        Console.Clear();
    }
}
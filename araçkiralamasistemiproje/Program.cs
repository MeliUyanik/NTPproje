using araçkiralamasistemiproje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace araçkiralamasistemiproje // Uygulamanın ana namespace'i
{
    internal class Program
    {
        // Kiralanan ve satılan araçlardan elde edilen toplam gelirler
        static int KiralananToplamGelir = 0;
        static int SatilanToplamGelir = 0;
        // Araçlar ve servis araçlarını tutacak listeler
        static List<KiralikArac> kiralikAraclar = new List<KiralikArac>();
        static List<SatilikArac> satilikAraclar = new List<SatilikArac>();
        static List<ServisAraci> servisAraclari = new List<ServisAraci>();
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            kiralikAraclar.AddRange(new List<KiralikArac>
            {
                new KiralikArac { Plaka = "04AGR2004", Marka = "Yamaha", Model = "YZF R1", KiraUcreti = 100 , Turu = "Motosiklet"},
                new KiralikArac { Plaka = "77YLV456", Marka = "Kawasaki", Model = "Ninja H2", KiraUcreti = 110 , Turu = "Motosiklet"},
                new KiralikArac { Plaka = "34FB1907", Marka = "Toyota", Model = "Corolla", KiraUcreti = 70, Turu = "Otomobil" },
                new KiralikArac { Plaka = "35GS1905", Marka = "Renault", Model = "Clio", KiraUcreti = 50, Turu = "Otomobil" },
                new KiralikArac { Plaka = "34BJK1903", Marka = "Volvo", Model = "FH5", KiraUcreti = 300, Turu = "Kamyon" },
                new KiralikArac { Plaka = "41KCL039", Marka = "Man", Model = "TGX", KiraUcreti = 280 , Turu = "Kamyon" },
                new KiralikArac { Plaka = "81DZC008", Marka = "Temsa", Model = "Maraton", KiraUcreti = 170 , Turu = "Otobüs"},
                new KiralikArac { Plaka = "54SKR1211", Marka = "Mercedes Benz", Model = "Travego", KiraUcreti = 180 , Turu = "Otobüs"},
            });

            satilikAraclar.AddRange(new List<SatilikArac>
            {
                new SatilikArac { Plaka = "37KST1289", Marka = "Honda", Model = "Civic", Turu = "Otomobil" , SatisFiyati = 900000 },
                new SatilikArac { Plaka = "35İZM4834", Marka = "Ford", Model = "Focus", Turu = "Otomobil" , SatisFiyati = 600000 },
                new SatilikArac { Plaka = "16BRS456", Marka = "BMW", Model = "GS R", Turu = "Motosiklet" , SatisFiyati = 1200000 },
                new SatilikArac { Plaka = "07ANT1212", Marka = "Mercedes Benz", Model = "Actros MP5", Turu = "Kamyon" , SatisFiyati = 2000000 },
                new SatilikArac { Plaka = "48MGL335", Marka = "Mercedes Benz", Model = "Tourismo", Turu = "Otobüs" , SatisFiyati = 1900000 }
            });

            servisAraclari.AddRange(new List<ServisAraci>
            {
                new ServisAraci { Plaka = "61TS1453", Kapasite = 20, GunlukUcret = 9000 },
                new ServisAraci { Plaka = "36KRS5166", Kapasite = 15, GunlukUcret = 7000 },
                new ServisAraci { Plaka = "51NGD4151", Kapasite = 10, GunlukUcret = 5000 }
            });
            string kullaniciIsmi = "";

            while (true)
            {


                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                string message1 = "Designed by";
                string message2 = "Melih UYANIK";
                // Konsol genişliğini al
                int consoleWidth = Console.WindowWidth;

                // Mesaj uzunluğunu al
                int messageLength = message1.Length;

                // Yazıyı ortalamak için gerekli boşluk miktarını hesapla
                int padding = (consoleWidth - messageLength) / 2;

                // Mesajı boşluklarla beraber yazdır
                Console.WriteLine(new string(' ', padding) + message1);
                Console.WriteLine(new string(' ', padding) + message2);
                Console.ForegroundColor = ConsoleColor.Red;
                // Ana menüye giriş
                Console.WriteLine("Araç Kiralama ve Satış Sistemine Hoş Geldiniz!");
                Console.WriteLine("1. Admin Girişi");
                Console.WriteLine("2. Kullanıcı Girişi");
                Console.WriteLine("3. Çıkış");
                Console.Write("Seçiminizi yapın: ");

                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1":
                        if (AdminGirisi())
                        {
                            AdminIslemleri(); // Admin menüsüne yönlendir
                        }
                        break;
                    // Kullanıcı girişini kontrol et
                    case "2":
                        kullaniciIsmi = KullaniciGirisi(); // Kullanıcı bilgilerini al
                        if (!string.IsNullOrEmpty(kullaniciIsmi)) // Kullanıcı menüsüne yönlendir
                        {
                            KullaniciMenusu(kullaniciIsmi);
                        }
                        break;
                    case "3":
                        Console.WriteLine("Sistemden çıkmak için herhangi bir tuşa basın.");
                        return;
                    default:
                        Console.WriteLine("Hatalı seçim yaptınız. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }
        // Admin giriş işlemi
        static bool AdminGirisi()
        {
            Console.Clear();
            Console.Write("Admin Kullanıcı Adı: ");
            string adminKullaniciAdi = Console.ReadLine();
            Console.Write("Şifre: ");
            string sifre = "";
            ConsoleKeyInfo key;
            
            do
            {
                key = Console.ReadKey(true); // Tuşu okur, ekrana yazdırmaz

                if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
                {
                    sifre += key.KeyChar;
                    Console.Write("*"); // Ekrana * yazdırır
                }
                else if (key.Key == ConsoleKey.Backspace && sifre.Length > 0)
                {
                    sifre = sifre.Substring(0, sifre.Length - 1);
                    Console.Write("\b \b"); // Son *'ı siler
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine(); // Yeni satıra geçer            

            if (adminKullaniciAdi == "admin" && sifre == "1234")
            {
                Console.WriteLine("Admin girişi başarılı. Admin menüsü için herhangi bir tuşa basın.");
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("Hatalı kullanıcı adı veya şifre. Ana menüye dönmek için bir herhangi bir tuşa basın");
                Console.ReadKey();
                return false;
            }
        }
        // Admin menüsünde yapılabilecek işlemler
        static void AdminIslemleri()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin İşlemleri:");
                Console.WriteLine("1. Araç Ekle");
                Console.WriteLine("2. Araç Kaldır");
                Console.WriteLine("3. Kiralanan ve Satılan Araçların Toplam Geliri");
                Console.WriteLine("4. Ana Menüye Dön");
                Console.Write("Seçiminizi yapın: ");
                string adminSecim = Console.ReadLine();

                switch (adminSecim)
                {
                    case "1":
                        AracEkle();
                        break;
                    case "2":
                        AracKaldir();
                        break;
                    case "3":
                        GelirHesapla();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim yaptınız.");
                        break;
                }
                Console.ReadKey();
            }
        }
        // Kiralanan ve satılan araçların toplam gelirini hesaplar
        static void GelirHesapla()
        {
            Console.WriteLine($"Kiralanan Araçların Geliri: {KiralananToplamGelir} TL");
            Console.WriteLine($"Satılan Araçların Geliri: {SatilanToplamGelir} TL");
            Console.WriteLine($"Toplam Gelir: {KiralananToplamGelir + SatilanToplamGelir} TL");
            Console.WriteLine("Ana menüye dönmek için bir tuşa basın.");
            Console.ReadKey();
        }

        static void AracEkle()
        {
            Console.Clear();
            Console.WriteLine("Araç Türünü Seçin (1: Kiralık, 2: Satılık): ");
            string turSecim = Console.ReadLine();

            Console.Write("Plaka: ");
            string plaka = Console.ReadLine();
            Console.Write("Marka: ");
            string marka = Console.ReadLine();
            Console.Write("Model: ");
            string model = Console.ReadLine();
            Console.Write("Tür: ");
            string tur = Console.ReadLine();

            if (turSecim == "1")
            {
                Console.Clear();
                Console.Write("Kira Ücreti: ");
                if (int.TryParse(Console.ReadLine(), out int kiraUcreti))
                {

                    kiralikAraclar.Add(new KiralikArac { Plaka = plaka, Marka = marka, Model = model, Turu = tur, KiraUcreti = kiraUcreti });
                    Console.WriteLine("Araç başarıyla eklendi. Herhangi bir tuşa basın.");
                }
                else
                {
                    Console.WriteLine("Geçersiz kira ücreti. Herhangi bir tuşa basın.");
                }
            }
            else if (turSecim == "2")
            {
                Console.Clear();
                Console.Write("Satış Fiyatı: ");
                if (int.TryParse(Console.ReadLine(), out int satisFiyati))
                {

                    satilikAraclar.Add(new SatilikArac { Plaka = plaka, Marka = marka, Model = model, Turu = tur, SatisFiyati = satisFiyati });
                    Console.WriteLine("Araç başarıyla eklendi. Herhangi bir tuşa basın.");
                }
                else
                {
                    Console.WriteLine("Geçersiz satış fiyatı. Herhangi bir tuşa basın.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim. Herhangi bir tuşa basın.");
            }
            Console.ReadKey();
        }

        static void AracKaldir()
        {
            Console.Clear();
            Console.WriteLine("Araç Türünü Seçin (1: Kiralık, 2: Satılık): ");
            string turSecim = Console.ReadLine();

            Console.Write("Kaldırmak istediğiniz aracın plakasını girin: ");
            string plaka = Console.ReadLine();

            if (turSecim == "1")
            {
                KiralikArac arac = kiralikAraclar.Find(a => a.Plaka == plaka);
                if (arac != null)
                {
                    kiralikAraclar.Remove(arac);
                    Console.WriteLine("Kiralık araç başarıyla kaldırıldı.");
                }
                else
                {
                    Console.WriteLine("Kiralık araç bulunamadı.");
                }
            }
            else if (turSecim == "2")
            {
                SatilikArac arac = satilikAraclar.Find(a => a.Plaka == plaka);
                if (arac != null)
                {
                    satilikAraclar.Remove(arac);
                    Console.WriteLine("Satılık araç başarıyla kaldırıldı.");
                }
                else
                {
                    Console.WriteLine("Satılık araç bulunamadı.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim.");
            }
            Console.ReadKey();
        }

        // Kullanıcı girişi
        static string KullaniciGirisi()
        {
            Console.Clear();
            Console.Write("Adınızı girin: ");
            string isim = Console.ReadLine();

            string telefon;
            while (true)
            {
                Console.Write("Telefon numaranızı girin (11 haneli): ");
                telefon = Console.ReadLine();

                if (telefon.Length == 11 && long.TryParse(telefon, out _))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Geçersiz telefon numarası. Lütfen tekrar deneyin.");
                }
            }

            if (!string.IsNullOrWhiteSpace(isim) && !string.IsNullOrWhiteSpace(telefon))
            {
                Console.WriteLine($"\nHoş geldiniz, {isim}. Herhangi bir tuşa basın.");
                Console.ReadKey();
                return isim;
            }
            else
            {
                Console.WriteLine("Geçersiz giriş. Ana menüye dönmek için herhangi bir tuşa basın.");
                Console.ReadKey();
                return "";
            }
        }

        // Kullanıcı menüsü
        static void KullaniciMenusu(string kullaniciIsmi)
        {
            Console.WriteLine($"Hoş geldiniz, {kullaniciIsmi}");


            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nAraç Kiralama ve Satış Sistemine Hoş Geldiniz!");
                Console.WriteLine("1. Araç Kiralama");
                Console.WriteLine("2. Araç Satışı");
                Console.WriteLine("3. Şoförlü Servis Kiralama");
                Console.WriteLine("4. Çıkış");
                Console.Write("Seçiminizi yapın: ");

                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1":
                        AracKiralama();
                        break;
                    case "2":
                        AracSatisi();
                        break;
                    case "3":
                        ServisKirala();
                        break;
                    case "4":
                        Console.WriteLine("Sistemden çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Hatalı seçim yaptınız. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        // Kiralama işlemi için kullanılan metod
        static void AracKiralama()
        {
            Console.Clear();
            Console.Write("Ehliyet türünüzü girin (A/B/C/D veya Geri dönmek için G): "); // Kullanıcıdan ehliyet türünü alıyoruz (A/B/C/D veya G geri dönmek için)
            string ehliyetTuru = Console.ReadLine().ToUpper(); // Ehliyet türünü alır, büyük harfe dönüştürür
            if (ehliyetTuru == "G")
            {
                return; // Kullanıcı 'G' tuşuna basarsa, metodu sonlandırır (geri döner)
            }

            List<string> uygunAracTurleri = EhliyetIleUygunAraclar(ehliyetTuru); // Ehliyet türüne uygun araç türlerini alır

            if (uygunAracTurleri.Count == 0)
            {
                Console.WriteLine("Geçerli bir ehliyet türü girmediniz veya uygun araç bulunmamaktadır. Herhangi bir tuşa basın.");
                Console.ReadKey();
                return;
            }

            List<KiralikArac> uygunKiralikAraclar = kiralikAraclar.FindAll(arac => uygunAracTurleri.Contains(arac.Turu)); // Kiralık araçlar arasında, uygun araç türlerini filtreler

            if (uygunKiralikAraclar.Count == 0) // Uygun araç yoksa, kullanıcıya bilgi verir ve geri döner
            {
                Console.WriteLine("Uygun araç bulunmamaktadır. Herhangi bir tuşa basın.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nUygun Araçlar:");
            foreach (var arac in uygunKiralikAraclar) // Burada KiralikArac tipinde döngü yapıyoruz
            {
                Console.WriteLine($"Plaka: {arac.Plaka}, Marka: {arac.Marka}, Model: {arac.Model}, Tür: {arac.Turu}, Kilometre Başına Ücret: {arac.KiraUcreti} TL");
            }

            Console.Write("Kiralamak istediğiniz aracın plakasını girin(Geri dönmek için 'G' tuşuna basın): "); // Kullanıcıdan kiralamak istediği aracın plakasını alıyoruz
            string plaka = Console.ReadLine();

            if (plaka.ToUpper() == "G")
                return;

            KiralikArac secilenArac = uygunKiralikAraclar.Find(a => a.Plaka == plaka); // Seçilen aracın, uygun araçlar arasında olup olmadığını kontrol ediyoruz

            if (secilenArac != null)
            {
                Console.Write("Kaç kilometre kullanacağınızı girin: "); // Kullanıcıdan kilometre limitini ve kullanılan kilometreyi alıyoruz
                if (!int.TryParse(Console.ReadLine(), out int kmLimiti))
                {
                    Console.WriteLine("Geçersiz kilometre limiti.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Kaç kilometre kullandığınızı girin: ");
                if (!int.TryParse(Console.ReadLine(), out int kmKullanilan))
                {
                    Console.WriteLine("Geçersiz kullanılan kilometre.");
                    Console.ReadKey();
                    return;
                }

                int cezaUcreti = 0; // Kilometre aşımı için ceza ücreti hesaplıyoruz
                if (kmKullanilan > kmLimiti)
                {
                    int asimKm = kmKullanilan - kmLimiti;
                    cezaUcreti = asimKm * 10;
                }

                int toplamUcret = (kmKullanilan * secilenArac.KiraUcreti) + cezaUcreti;

                Console.WriteLine($"\nHesap Özeti:");
                Console.WriteLine($"Araç: {secilenArac.Marka} {secilenArac.Model}");
                Console.WriteLine($"Kullanılan Kilometre: {kmKullanilan} km");
                Console.WriteLine($"Anlaşılan Kilometre Limiti: {kmLimiti} km");

                if (kmKullanilan > kmLimiti)
                {
                    Console.WriteLine($"Kilometre Aşımı: {kmKullanilan - kmLimiti} km");
                    Console.WriteLine($"Ceza Ücreti: {cezaUcreti} TL");
                }
                else
                {
                    Console.WriteLine("Kilometre limiti aşılmadı.");
                }
                Console.WriteLine($"Toplam Ücret: {toplamUcret} TL");
                KiralananToplamGelir += toplamUcret;
                kiralikAraclar.Remove(secilenArac); // Kiralık araçlar listesinden, seçilen araç çıkarılıyor (araç kiralandı)
                Console.WriteLine("\nAraç başarıyla kiralandı.");
            }
            else
            {
                Console.WriteLine("Girilen plakaya uygun araç bulunamadı.");
            }
            Console.WriteLine("Geri dönmek için 'G' tuşuna basın.");
            if (Console.ReadLine().ToUpper() == "G")
                return;
            Console.ReadKey();
        }

        static List<string> EhliyetIleUygunAraclar(string ehliyetTuru)
        {
            switch (ehliyetTuru)
            {
                case "A":
                    return new List<string> { "Motosiklet" }; // "A" ehliyeti motosiklet kiralayabilir
                case "B":
                    return new List<string> { "Otomobil" }; // "B" ehliyeti otomobil kiralayabilir
                case "C":
                    return new List<string> { "Kamyon" }; // "C" ehliyeti kamyon kiralayabilir
                case "D":
                    return new List<string> { "Otobüs" }; // "D" ehliyeti otobüs kiralayabilir
                default:
                    return new List<string>(); // Geçersiz ehliyet türü için boş liste döner
            }
        }

        static void AracSatisi() // Satış işlemi için kullanılan metod
        {
            Console.Clear();
            Console.WriteLine("\nSatılık Araçlar:");
            foreach (var arac in satilikAraclar) // Burada SatilikArac tipinde döngü yapıyoruz
            {
                Console.WriteLine($"Plaka: {arac.Plaka}, Marka: {arac.Marka}, Model: {arac.Model}, Satış Fiyatı: {arac.SatisFiyati} TL");
            }
            // Kullanıcıdan satın almak istediği aracın plakasını alıyoruz
            Console.Write("Satın almak istediğiniz aracın plakasını girin: ");
            string plaka = Console.ReadLine();
            if (plaka.ToUpper() == "G")
            {
                return;
            }

            SatilikArac secilenArac = satilikAraclar.Find(a => a.Plaka == plaka); // Seçilen aracın satılık araçlar arasında olup olmadığını kontrol ediyoruz

            if (secilenArac != null)
            {
                Console.WriteLine($"\nSatın alacağınız araç: {secilenArac.Marka} {secilenArac.Model}, Fiyat: {secilenArac.SatisFiyati} TL"); // Seçilen aracın fiyatını kullanıcıya gösteriyoruz
                Console.WriteLine("\nBu aracı satın almak istiyor musunuz? (Evet için 'E', Geri dönmek için 'G'):");// Kullanıcıdan satın almayı onaylamasını istiyoruz
                string secim = Console.ReadLine().ToUpper();
                if (secim == "E")
                {
                    SatilanToplamGelir += secilenArac.SatisFiyati;
                    satilikAraclar.Remove(secilenArac);
                    Console.WriteLine("\nSatın alma işlemi başarıyla tamamlandı. Ana menüye dönmek için herhangi bir tuşa basın.");
                    Console.ReadKey();
                    return;
                }
                else if (secim == "G")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Geçersiz giriş yaptınız. Lütfen tekrar deneyin.");
                }
            }
            else
            {
                Console.WriteLine("Girilen plakaya uygun araç bulunamadı. Geri dönmek için 'G' tuşuna basabilirsiniz.");
                string geriSecim = Console.ReadLine().ToUpper();
                if (geriSecim == "G")
                {
                    return;
                }
            }
            Console.ReadKey();
        }
        static void ServisKirala()
        {
            Console.Clear();
            Console.WriteLine("\nŞoförlü Servis Araçları:");
            foreach (var servis in servisAraclari)
            {
                Console.WriteLine($"Plaka: {servis.Plaka}, Kapasite: {servis.Kapasite}, Günlük Ücret: {servis.GunlukUcret} TL");
            }

            Console.Write("Kiralamak istediğiniz servis aracının plakasını girin: ");
            string plaka = Console.ReadLine();

            ServisAraci secilenServis = servisAraclari.Find(s => s.Plaka == plaka);

            if (secilenServis != null)
            {
                Console.Write("Kaç gün kiralamak istediğinizi girin: ");
                if (int.TryParse(Console.ReadLine(), out int gunSayisi))
                {
                    int toplamUcret = secilenServis.GunlukUcret * gunSayisi;
                    KiralananToplamGelir += toplamUcret;

                    Console.WriteLine($"\nServis Aracı: {secilenServis.Plaka}");
                    Console.WriteLine($"Toplam Ücret: {toplamUcret} TL");
                    servisAraclari.Remove(secilenServis);
                    Console.WriteLine("\nServis aracı başarıyla kiralandı. Ana menüye dönmek için herhangi bir tuşa basın.");
                }
                else
                {
                    Console.WriteLine("Geçersiz gün sayısı girdiniz.");
                }
            }
            else
            {
                Console.WriteLine("Girilen plakaya uygun servis aracı bulunamadı. Ana menüye dönmek için herhangi bir tuşa basın.");
            }
            Console.ReadKey();
        }

        class Arac // Arac sınıfı, kiralanabilir veya satılabilir araçların özelliklerini tanımlar
        {
            public string Plaka { get; set; } // Aracın plakası
            public string Marka { get; set; } // Aracın markası
            public string Model { get; set; } // Aracın modeli           
            public string Turu { get; set; } // Aracın türü (örneğin; otomobil, motosiklet, kamyon, vb.)            
        }

        class KiralikArac : Arac
        {
            public int KiraUcreti { get; set; }
        }

        class SatilikArac : Arac
        {
            public int SatisFiyati { get; set; }
        }

        class ServisAraci : Arac // ServisAraci sınıfı, şoförlü servis araçlarının özelliklerini tanımlar
        {
            public int Kapasite { get; set; } // Servis aracının taşıma kapasitesi (örneğin; yolcu sayısı)
            public int GunlukUcret { get; set; } // Servis aracının günlük kiralama ücreti
        }

        static class GelirTakip // GelirTakip sınıfı, kiralama ve satış gelirlerini takip eder
        {
            public static int KiralananGelir { get; set; } // Kiralanan araçlardan elde edilen toplam gelir
            public static int SatilanGelir { get; set; } // Satılan araçlardan elde edilen toplam gelir
        }
    }
}
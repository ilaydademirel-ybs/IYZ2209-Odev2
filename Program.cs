using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Ekipman> ekipmanlar = new List<Ekipman>
        {
            new Ekipman("LAB-001", "Kamera"),
            new Ekipman("LAB-002", "Mikrofon"),
            new Ekipman("LAB-003", "Projeksiyon")
        };

        List<Ogrenci> ogrenciler = new List<Ogrenci>
        {
            new Ogrenci("1001", "İlayda Demirel"),
            new Ogrenci("1002", "Ahmet Yılmaz")
        };

        string? secim = "";
        while (secim != "0")
        {
            Console.WriteLine("\n--- EKİPMAN TAKİP SİSTEMİ ---");
            Console.WriteLine("1 - Ekipmanları Listele");
            Console.WriteLine("2 - Ödünç Ver");
            Console.WriteLine("3 - İade Al");
            Console.WriteLine("0 - Çıkış");
            Console.Write("Seçiminiz: ");
            secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    Console.WriteLine("\n=== Ekipman Listesi ===");
                    foreach (var e in ekipmanlar)
                    {
                        string durum = e.Oduncte ? $"Ödünç Verildi (Öğrenci No: {e.OduncAlanNo})" : "Müsait";
                        Console.WriteLine($"[{e.DemirbasNo}] {e.Ad} - Durum: {durum}");
                    }
                    break;

                case "2":
                    Console.Write("Ödünç verilecek Ekipman No (Örn: LAB-001): ");
                    string? eNo = Console.ReadLine()?.ToUpperInvariant();
                    Ekipman? bulunanEkipman = EkipmanBul(ekipmanlar, eNo ?? "");

                    if (bulunanEkipman == null)
                    {
                        Console.WriteLine("Hata: Ekipman bulunamadı.");
                        break;
                    }

                    Console.Write("Öğrenci Numaranız: ");
                    string? oNo = Console.ReadLine();
                    
                    bool ogrenciKayitli = false;
                    foreach (var o in ogrenciler)
                    {
                        if (o.Numara == oNo) ogrenciKayitli = true;
                    }

                    if (!ogrenciKayitli)
                    {
                        Console.WriteLine("Hata: Kayıtlı öğrenci bulunamadı.");
                        break;
                    }

                    Console.Write("Kaç günlüğüne ödünç alınacak (1-7 gün)? ");
                    string? gunGiris = Console.ReadLine();
                    if (!int.TryParse(gunGiris, out int gun) || gun < 1 || gun > 7)
                    {
                        Console.WriteLine("Hata: Geçersiz gün süresi! İşlem iptal edildi.");
                        break;
                    }

                    bool oduncSonuc = bulunanEkipman.OduncVer(oNo ?? "");
                    Console.WriteLine(oduncSonuc ? "Başarılı: Ödünç verildi." : "Hata: İşlem reddedildi! (Ekipman zaten kullanımda olabilir).");
                    break;

                case "3":
                    Console.Write("İade edilecek Ekipman No: ");
                    string? iadeNo = Console.ReadLine()?.ToUpperInvariant();
                    Ekipman? iadeEkipman = EkipmanBul(ekipmanlar, iadeNo ?? "");

                    if (iadeEkipman == null)
                    {
                        Console.WriteLine("Hata: Ekipman bulunamadı.");
                        break;
                    }

                    bool iadeSonuc = iadeEkipman.IadeAl();
                    Console.WriteLine(iadeSonuc ? "Başarılı: İade alındı." : "Hata: İşlem reddedildi! (Ekipman zaten müsaitti).");
                    break;

                case "0":
                    Console.WriteLine("Programdan çıkılıyor...");
                    break;

                default:
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                    break;
            }
        }
    }

    static Ekipman? EkipmanBul(List<Ekipman> liste, string no)
    {
        foreach (Ekipman e in liste)
        {
            if (e.DemirbasNo == no)
                return e;
        }
        return null;
    }
}


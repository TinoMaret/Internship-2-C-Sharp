Dictionary<string, (string position, int rating)> igraci = new Dictionary<string, (string position, int rating)>() {
    {"Luka Modrić", ("MF",88)},
    {"Mislav Oršić", ("FW", 77) },
    {"Mario Pašalić", ("MF", 81) },
    {"Ivan Perišić", ("LM", 84) },
    {"Marko Livaja", ("FW", 77) },
    {"Ivan Rakitić", ("MF", 82) },
    {"Marko Pjaca", ("MF", 75) },
    {"Joško Gvardiol", ("DF", 81) },
    {"Andrej Kramarić", ("FW", 82) },
    {"Lovro Majer", ("MF", 80) },
    {"Ivor Pandur", ("GK", 72) },
    {"Domagoj Vida", ("DF", 76) },
    {"Marcelo Brozović", ("MF",86) },
    {"Josip Brekalo", ("MF", 79) },
    {"Borna Sosa", ("DF", 78) },
    {"Mateo Kovačić", ("MF", 84) },
    {"Duje Ćaleta-Car", ("DF", 78) },
    {"Kristijan Jakić", ("MF", 76) },
    {"Dejan Lovren", ("DF", 78) },
    {"Ante Rebić", ("FW", 80) },
    {"NIkola Vlašič", ("MF", 78) },
    {"Dominik Livaković", ("GK", 80) },
    {"Ante Budimir", ("FW", 76) },
};

Dictionary<string, int> Strijelci = new Dictionary<string, int>();

Dictionary<string, int>[] OdigraneUtakmice = new Dictionary<string, int>[6];
OdigraneUtakmice[0] = new Dictionary<string, int>();
OdigraneUtakmice[1] = new Dictionary<string, int>();
OdigraneUtakmice[2] = new Dictionary<string, int>();
OdigraneUtakmice[3] = new Dictionary<string, int>();
OdigraneUtakmice[4] = new Dictionary<string, int>();
OdigraneUtakmice[5] = new Dictionary<string, int>();

Dictionary<string, (int, int)> Tablica = new Dictionary<string, (int, int)>()
{
    {"Belgija", (0,0) },
    {"Kanada", (0,0) },
    {"Maroko", (0,0) },
    {"Hrvatska", (0,0) }
};



int unos;
int utakmice = 0;
int kolo = 1;
bool parsiranje;


do {
    Console.WriteLine("1 - Odradi trening");
    Console.WriteLine("2 - Odigraj utakmicu");
    Console.WriteLine("3 - Statistika");
    Console.WriteLine("4 - Kontrola igrača");
    Console.WriteLine("0 - Izlaz iz aplikacije");

    do {
        parsiranje = int.TryParse(Console.ReadLine(), out unos);
        if (!parsiranje)
        {
            Console.WriteLine("Neispravan unos");
            unos = -1;
        }
        else if (unos < 0 || unos > 4)
            Console.WriteLine("Uneseni broj ne označava ni jednu moguću radnju");
    } while (unos < 0 || unos > 4);
    Console.Clear();
    switch (unos)
    {
        case 1:
            Console.WriteLine("Rejtinzi prije treninga \n\n");
            foreach (var igrac in igraci)
            {
                Console.WriteLine($"{igrac.Key} {igrac.Value}");
            }

            igraci = OdradiTrening(igraci);

            Console.WriteLine("Rejtinzi poslije treninga \n\n");
            foreach (var igrac in igraci)
            {
                Console.WriteLine($"{igrac.Key} {igrac.Value}");
            }

            unos = PocetakIliKraj();

            continue;
        case 2:

            if (Prvih11(igraci).Count < 11)
            {
                Console.WriteLine("Nije moguće pokrenuti utakmicu, nedovoljan broj igrača za rasporediti po pozicijama");

                unos = PocetakIliKraj();
            }
            else if (utakmice >= 5) {
                Console.WriteLine("Sve su utakmice odigrane za ovo kolo");

                unos = PocetakIliKraj();
            }
            else
            {
                OdigraneUtakmice[utakmice] = OdigrajUtakmicuHrv(kolo);

                Strijelci = (Shooters(Prvih11(igraci), Strijelci, OdigraneUtakmice[utakmice]["Hrvatska"]));

                Console.WriteLine("Strijelci za Hrvatsku:");
                foreach (var strijelac in Strijelci)
                {
                    Console.WriteLine($"{strijelac.Key} sa {strijelac.Value} golova");
                }

                igraci = DizanjeRatingaStrijelcima(igraci, Strijelci);

                int temp1 = 0, temp2 = 0;

                foreach (var rez in OdigraneUtakmice[utakmice]) {
                    if (rez.Key.Equals("Hrvatska")) {
                        temp1 = rez.Value;
                    }
                    else{
                        temp2 = rez.Value;
                    }
                }

                if (temp1 > temp2) {
                    igraci = SpajanjeNakonUtakmice(igraci, Pobjeda(Prvih11(igraci)));
                }
                else if(temp1 < temp2){
                    igraci = SpajanjeNakonUtakmice(igraci, Gubitak(Prvih11(igraci)));
                }

                utakmice++;

                OdigraneUtakmice[utakmice] = OdigrajUtakmicuOstali(kolo);
                
                utakmice++;

                kolo++;

                unos = PocetakIliKraj();
            }


            continue;

        case 3:
            Console.WriteLine("1 - Ispis onako kako su spremljeni");
            Console.WriteLine("2 - Ispis po ratingu uzlazno");
            Console.WriteLine("3 - Ispis po ratingu silazno");
            Console.WriteLine("4 - Ispis igrača po imenu i prezimenu");
            Console.WriteLine("5 - Ispis igrača po ratingu");
            Console.WriteLine("6 - Ispis igrača po poziciji");
            Console.WriteLine("7 - Ispis trenutnih prvih 11 igražča");
            Console.WriteLine("8 - Ispis strijelaca i koliko golova imaju");
            Console.WriteLine("9 - Ispis svih rezultata ekipe");
            Console.WriteLine("10 - Ispis rezultat svih ekipa");
            Console.WriteLine("11 - Ispis tablice grupe");

            do
            {
                parsiranje = int.TryParse(Console.ReadLine(), out unos);
                if (!parsiranje)
                {
                    Console.WriteLine("Neisprasvan unos");
                    unos = 0;
                }
                if (unos < 1 || unos > 4)
                {
                    Console.WriteLine("Uneseni broj ne oznacava nepostojecu radnju");
                }
            } while (unos < 1 || unos > 11);
            Console.Clear();
            switch (unos) {
                case 1:
                    IspisOnakoKakoSuSpremljeni(igraci);

                    unos = PocetakIliKraj();
                    continue;
                case 2:
                    IspisPoRatinguUzlazno(igraci);

                    unos = PocetakIliKraj();

                    continue;
                case 3:
                    IspisPoRatinguSilazno(igraci);

                    unos = PocetakIliKraj();

                    continue;
                case 4:
                    IspisIgracaPoImenuIPrezimenu(igraci);

                    unos = PocetakIliKraj();

                    continue;
                case 5:
                    IspisIgracaPoRatingu(igraci);

                    unos = PocetakIliKraj();

                    continue;
                case 6:
                    IspisIgracaPoPoziciji(igraci);

                    unos = PocetakIliKraj();

                    continue;
                case 7:
                    IspisTrenutnihPrvih11Igraca(Prvih11(igraci));

                    unos = PocetakIliKraj();

                    continue;

                case 8:
                    IspisStrijelacaIGolova(Strijelci);

                    unos = PocetakIliKraj();

                    continue;

                case 9:
                    IspisSvihRezultataEkipe(OdigraneUtakmice);

                    unos = PocetakIliKraj();

                    continue;

                case 10:
                    IspisSvihUtakmicaDoSada(OdigraneUtakmice);

                    unos = PocetakIliKraj();

                    continue;

                case 11:
                    Tablica = UpdateStanja(OdigraneUtakmice, kolo - 1);

                    string[] Sortirani = SortiraniPoBodovima(Tablica);

                    for (int i = 0; i < Sortirani.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Sortirani[i]} - {Tablica[Sortirani[i]]}");
                    }

                    unos = PocetakIliKraj();

                    continue;
            }

            continue;

        case 4:
            Console.WriteLine("1 - Unos novog igrača");
            Console.WriteLine("2 - Brisanje igrača");
            Console.WriteLine("3 - Uređivanje igrača");
            Console.WriteLine("4 - Izlaz iz aplikacije");
            do {
                parsiranje = int.TryParse(Console.ReadLine(), out unos);
                if (!parsiranje)
                {
                    Console.WriteLine("Neisprasvan unos");
                    unos = 0;
                }
                if (unos < 1 || unos > 4)
                {
                    Console.WriteLine("Uneseni broj ne oznacava nepostojecu radnju");
                }
            } while (unos < 1 || unos > 4);

            switch (unos) {
                case 1:
                    if (igraci.Count < 26)
                    {
                        igraci = UnosIgraca(igraci);
                    }

                    else {
                        Console.WriteLine("U ekipi imate 26 igrača, nije moguće unijeti novog");
                    }
                    unos = PocetakIliKraj();

                    continue;

                case 2:

                    igraci = BrisanjeIgraca(igraci);

                    unos = PocetakIliKraj();

                    continue;


                case 3:
                    igraci = UredivanjeIgraca(igraci);

                    unos = PocetakIliKraj();
                    
                    continue;

                case 0:

                    return;
            }

            continue;

        case 0:

            return;
    }
} while (unos != 0);

return;


Dictionary<string, (string, int)> OdradiTrening(Dictionary<string, (string position, int rating)> players) {
    Random rnd = new Random();
    Dictionary<string, (string, int)> nakonTreninga = new Dictionary<string, (string, int)>();
    foreach (var player in players)
    {
        double num = (double)rnd.Next(-5,5)/100;
        var x = players[player.Key];
        x.rating = (int)Math.Round(x.rating + x.rating*num);
        if (x.rating > 100)
            x.rating = 100;
        nakonTreninga.Add(player.Key, x);
    }
    return nakonTreninga;
}

Dictionary<string, int> OdigrajUtakmicuHrv(int kolo) {
    (int, int) rezultat;
    switch (kolo) {
        case 1:
            Dictionary<string, int> GoloviPrvoKolo = new Dictionary<string, int>();
            Console.WriteLine("Utakmica: Maroko-Hrvatska");
            rezultat = RezultatUtakmice();
            GoloviPrvoKolo.Add("Maroko", rezultat.Item1);
            GoloviPrvoKolo.Add("Hrvatska", rezultat.Item2);
            Console.WriteLine($"Rezultat:\n Maroko:{GoloviPrvoKolo["Maroko"]} - Hrvatska:{GoloviPrvoKolo["Hrvatska"]}");

            return GoloviPrvoKolo;

        case 2:
            Dictionary<string, int> GoloviDrugoKolo = new Dictionary<string, int>();
            Console.WriteLine("Utakmica: Hrvatska-Kanada");
            rezultat = RezultatUtakmice();
            GoloviDrugoKolo.Add("Hrvatska", rezultat.Item1);
            GoloviDrugoKolo.Add("Kanada", rezultat.Item2);
            Console.WriteLine($"Rezultat:\n Hrvatska:{GoloviDrugoKolo["Hrvatska"]} - Kanada:{GoloviDrugoKolo["Kanada"]}");

            return GoloviDrugoKolo;

        case 3:
            Dictionary<string, int> GoloviTreceKolo = new Dictionary<string, int>();
            Console.WriteLine("Utakmica: Hrvatska-Belgija");
            rezultat = RezultatUtakmice();
            GoloviTreceKolo.Add("Hrvatska", rezultat.Item1);
            GoloviTreceKolo.Add("Belgija", rezultat.Item2);
            Console.WriteLine($"Rezultat:\n Hrvatska:{GoloviTreceKolo["Hrvatska"]} - Belgija:{GoloviTreceKolo["Belgija"]}");

            return GoloviTreceKolo;
        default:
            Dictionary<string, int> Default = new Dictionary<string, int>() { { "", 0 } };
            return Default;
    }
}

Dictionary<string, int> OdigrajUtakmicuOstali(int kolo)
{
    (int, int) rezultat;
    switch (kolo)
    {
        case 1:
            Dictionary<string, int> GoloviPrvoKolo = new Dictionary<string, int>();
            Console.WriteLine("Utakmica: Belgija-Kanada");
            rezultat = RezultatUtakmice();
            GoloviPrvoKolo.Add("Belgija", rezultat.Item1);
            GoloviPrvoKolo.Add("Kanada", rezultat.Item2);
            Console.WriteLine($"Rezultat:\n Belgija:{GoloviPrvoKolo["Belgija"]} - Kanada:{GoloviPrvoKolo["Kanada"]}");

            return GoloviPrvoKolo;

        case 2:
            Dictionary<string, int> GoloviDrugoKolo = new Dictionary<string, int>();
            Console.WriteLine("Utakmica: Belgija-Maroko");
            rezultat = RezultatUtakmice();
            GoloviDrugoKolo.Add("Belgija", rezultat.Item1);
            GoloviDrugoKolo.Add("Maroko", rezultat.Item2);
            Console.WriteLine($"Rezultat:\n Belgija:{GoloviDrugoKolo["Belgija"]} - Maroko:{GoloviDrugoKolo["Maroko"]}");

            return GoloviDrugoKolo;

        case 3:
            Dictionary<string, int> GoloviTreceKolo = new Dictionary<string, int>();
            Console.WriteLine("Utakmica: Kanada-Maroko");
            rezultat = RezultatUtakmice();
            GoloviTreceKolo.Add("Kanada", rezultat.Item1);
            GoloviTreceKolo.Add("Maroko", rezultat.Item2);
            Console.WriteLine($"Rezultat:\n Kanada:{GoloviTreceKolo["Kanada"]} - Maroko:{GoloviTreceKolo["Maroko"]}");

            return GoloviTreceKolo;
        default:
            Dictionary<string, int> Default = new Dictionary<string, int>() { { "", 0 } };
            return Default;
    }
}

(int, int) RezultatUtakmice() {
    Random rnd = new Random();
    Random rnd2 = new Random();
    int Tim1 = rnd.Next(0, 10);
    int Tim2 = rnd2.Next(0, 10);
    return (Tim1, Tim2);
}

Dictionary<string, int> Shooters(Dictionary<string, (string, int)>players,Dictionary<string, int> postojeci, int golovi) {
    Random rnd = new Random();
    int[] izabrani = new int[golovi];
    for (int i = 0; i < izabrani.Length; i++) {
        izabrani[i] = rnd.Next(0,players.Count-1);
    }
    int brojac1 = 0;
    foreach (var player in players) {
        if (izabrani.Contains(brojac1)) {
            if (postojeci.Keys.Contains(player.Key)){
                int x = postojeci[player.Key];
                postojeci.Remove(player.Key);
                postojeci.Add(player.Key, x + izabrani.Count(c => c == brojac1));
            }
            else
                postojeci.Add(player.Key, izabrani.Count(c => c == brojac1));
        }
        brojac1++;
    }
    return postojeci; 
}

Dictionary<string, (string, int)> DizanjeRatingaStrijelcima(Dictionary<string, (string position, int rating)>players, Dictionary<string, int> Strijelci) {
    Dictionary<string, (string, int)> NoviRejzinzi = new Dictionary<string, (string, int)>();
    foreach (var player in players) {
        if (Strijelci.Keys.Contains(player.Key))
        {
            var x = players[player.Key];
            x.rating = (int)Math.Round(x.rating * 1.05);
            if (x.rating > 100)
                x.rating = 100;
            NoviRejzinzi.Add(player.Key, x);
        }
        else NoviRejzinzi.Add(player.Key, player.Value);
    }

    return NoviRejzinzi;
}

Dictionary<string, (string, int)> Pobjeda(Dictionary<string, (string, int rating)> players) {
    Dictionary<string, (string, int)> NakonPobjede = new Dictionary<string, (string, int)>();
    foreach (var player in players) {
        var x = players[player.Key];
        x.rating = (int)Math.Round(x.rating * 1.02);
        if (x.rating > 100)
            x.rating = 100;
        NakonPobjede.Add(player.Key, x);
    }
    return NakonPobjede;
}

Dictionary<string, (string, int)> Gubitak(Dictionary<string, (string, int rating)> players)
{
    Dictionary<string, (string, int)> NakonGubitka = new Dictionary<string, (string, int)>();
    foreach (var player in players)
    {
        var x = players[player.Key];
        x.rating = (int)Math.Round(x.rating * 0.98);
        if (x.rating > 100)
            x.rating = 100;
        NakonGubitka.Add(player.Key, x);
    }
    return NakonGubitka;
}

Dictionary<string, (string, int)> SpajanjeNakonUtakmice(Dictionary<string, (string, int rating)>players, Dictionary<string, (string, int rating)> prvapostava) {
    Dictionary<string, (string, int)> Spojeni = new Dictionary<string, (string, int)>();
    foreach(var player in players) { 
        if (prvapostava.Keys.Contains(player.Key))
            Spojeni.Add(player.Key, prvapostava[player.Key]);
        else
            Spojeni.Add(player.Key, player.Value);
    }

    return Spojeni;
}


void IspisOnakoKakoSuSpremljeni(Dictionary<string, (string, int)> players) {
    foreach (var player in players) {
        Console.WriteLine($"{player.Key} {player.Value}");
    }
}

void IspisPoRatinguUzlazno(Dictionary<string, (string, int)> players)
{
    string[] sorted = SortinranoPoRatinguUzlazno(players);
    foreach (var player in sorted) {
        Console.WriteLine($"{player} {players[player]}");
    }
}

void IspisPoRatinguSilazno(Dictionary<string, (string, int)> players) {
    string [] sorted = SortinranoPoRatinguSilazno(players);
    foreach (var player in sorted) {
        Console.WriteLine($"{player} {players[player]}");
    }
}

string[] SortinranoPoRatinguUzlazno(Dictionary<string, (string, int)> players) {
    string[] sortirani = new string[players.Count];
    int k = 0;
    string temp;
    foreach (var player in players){
        sortirani[k] = player.Key;
        k++;
    }
    for (int j = 0; j <= sortirani.Length - 2; j++)
    {
        for (int i = 0; i <= sortirani.Length - 2; i++)
        {
            if (players[sortirani[i]].Item2 > players[sortirani[i + 1]].Item2)
            {
                temp = sortirani[i + 1];
                sortirani[i + 1] = sortirani[i];
                sortirani[i] = temp;
            }
        }
    }
    return sortirani;
}

string[] SortinranoPoRatinguSilazno(Dictionary<string, (string, int)> players)
{
    string[] sortirani = new string[players.Count];
    int k = 0;
    string temp;
    foreach (var player in players)
    {
        sortirani[k] = player.Key;
        k++;
    }
    for (int j = 0; j <= sortirani.Length - 2; j++)
    {
        for (int i = 0; i <= sortirani.Length - 2; i++)
        {
            if (players[sortirani[i]].Item2 < players[sortirani[i + 1]].Item2)
            {
                temp = sortirani[i + 1];
                sortirani[i + 1] = sortirani[i];
                sortirani[i] = temp;
            }
        }
    }
    return sortirani;
}

void IspisIgracaPoImenuIPrezimenu(Dictionary<string, (string, int)> players) {
    Console.WriteLine("Unesi ime igrača kojemu želiš saznati poziciju i rating");
    string ime;
    bool poklapanje = false;
    do
    {
        ime = Console.ReadLine();
        foreach (var player in players)
        {
            if (player.Key.Equals(ime))
            {
                poklapanje = true;
            }
        }
        if (!poklapanje)
        {
            Console.WriteLine("Ne postoji igrac sa unesenim imenom i prezimenom");
        }
    } while (!poklapanje);
    Console.WriteLine(players[ime]);
}

void IspisIgracaPoRatingu(Dictionary<string, (string, int)> players) {
    Console.WriteLine("Za koji rating želiš ispisati igrače (1 - 100)?");
    int rej;
    bool rejting;
    bool ispis = false;
    do
    {
        rejting = int.TryParse(Console.ReadLine(), out rej);
        if (!rejting)
        {
            Console.WriteLine("Neispravan unos");
            rej = -1;
        }
        else if (rej < 1 || rej > 100)
        {
            Console.WriteLine("Unijeli ste broj izvan raspona");
        }
    } while (rej < 1 || rej > 100);
    foreach (var player in players) {
        if (player.Value.Item2 == rej){
            Console.WriteLine(player.Key);
            ispis = true;
        }
    }
    if (!ispis)
        Console.WriteLine("Ne postoji igrač s tim ratingom");
}

void IspisIgracaPoPoziciji(Dictionary<string, (string, int)> players) {
    Console.WriteLine("Za koju poziciju želiš ispisati igrače?");
    string pozicija;
    bool ispis = false;
    do
    {
        pozicija = Console.ReadLine().ToUpper();
        if (pozicija != "GK" && pozicija != "DF" && pozicija != "MF" && pozicija != "FW")
            Console.WriteLine("Ne postoji unesena pozicija");
    } while (pozicija != "GK" && pozicija != "DF" && pozicija != "MF" && pozicija != "FW");
    foreach (var player in players)
    {
        if (player.Value.Item1 == pozicija)
        {
            Console.WriteLine(player.Key);
            ispis = true;
        }
    }
    if (!ispis)
        Console.WriteLine("Ne postoji igrač s tom pozicijom");
}

void IspisTrenutnihPrvih11Igraca(Dictionary<string, (string, int)> players) {
    if (players.Count < 11)
        Console.WriteLine("Nema dovoljno igrača u reprezentaciji za ispis prvih 11");
    else{
        foreach(var player in players){
            Console.WriteLine($"{player.Key} {player.Value}");
        }
    }
        
}

void IspisStrijelacaIGolova(Dictionary<string, int> strijelci){
    Console.WriteLine("Strijelci su");
    foreach(var strijelac in strijelci){
        Console.WriteLine($"{strijelac.Key} sa {strijelac.Value} golova");
    }
}

void IspisSvihRezultataEkipe(Dictionary<string, int>[] utakmice){
    Console.WriteLine("Utakmice Hrvatske:\n");
    foreach (var utak in utakmice) {
        if (utak.Keys.Contains("Hrvatska")) {
            Console.WriteLine("Utakmica\n");
            foreach(var u in utak){
                Console.WriteLine($"{u.Key}: {u.Value}");
            }
            Console.WriteLine("");
        }
    }
}

void IspisSvihUtakmicaDoSada(Dictionary<string, int>[] utakmice)
{
    Console.WriteLine("Sve utakmice do sada");
    foreach (var utak in utakmice)
    {
        Console.WriteLine("Utakmica\n");
        foreach (var u in utak)
        {
            Console.WriteLine($"{u.Key}: {u.Value}");
        }
        Console.WriteLine("");
    }
}

Dictionary<string, (int, int)> UpdateStanja(Dictionary<string, int>[] odigrane, int kolo) {
    Dictionary<string, (int, int)> Osvjezeno = new Dictionary<string, (int, int)> ();
    string[] drzave = new string[4] {"Belgija", "Kanada", "Maroko", "Hrvatska"};
    int[] golovi = new int[4] {0,0,0,0};
    int[] bodovi = new int[4] {0,0,0,0};
    int[] golrazlika = new int[4] {0,0,0,0};
    int brojac = 0;
    for(int i = 0; i < kolo; i++)
    {
        foreach (var u in odigrane[i]) {
            if (u.Key.Equals("Belgija"))
            {
                golovi[0] = u.Value;
            }
            if (u.Key.Equals("Kanada"))
            {
                golovi[1] = u.Value;
            }
            if (u.Key.Equals("Maroko"))
            {
                golovi[2] = u.Value;
            }
            if (u.Key.Equals("Hrvatska"))
            {
                golovi[3] = u.Value;
            }
        }
        switch (brojac)
        {
            case 0:
                if (golovi[2] > golovi[3])
                {
                    bodovi[2] += 3;
                    golrazlika[2] = golrazlika[2] + golovi[2] - golovi[3];
                    golrazlika[3] = golrazlika[3] + golovi[3] - golovi[2];
                }
                else if (golovi[3] > golovi[2])
                {
                    bodovi[3] += 3;
                    golrazlika[2] = golrazlika[2] + golovi[2] - golovi[3];
                    golrazlika[3] = golrazlika[3] + golovi[3] - golovi[2];
                }
                else
                {
                    bodovi[2] += 1;
                    bodovi[3] += 1;
                }
                if (golovi[0] > golovi[1])
                {
                    bodovi[0] += 3;
                    golrazlika[0] = golrazlika[0] + golovi[0] - golovi[1];
                    golrazlika[1] = golrazlika[1] + golovi[1] - golovi[0];
                }
                else if (golovi[1] > golovi[0])
                {
                    bodovi[1] += 3;
                    golrazlika[0] = golrazlika[0] + golovi[0] - golovi[1];
                    golrazlika[1] = golrazlika[1] + golovi[1] - golovi[0];
                }
                else
                {
                    bodovi[0] += 1;
                    bodovi[1] += 1;
                }
                if (brojac == kolo)
                    break;
                else
                    continue;

            case 1:
                if (golovi[0] > golovi[2])
                {
                    bodovi[0] += 3;
                    golrazlika[0] = golrazlika[0] + golovi[0] - golovi[2];
                    golrazlika[2] = golrazlika[2] + golovi[2] - golovi[0];
                }
                else if (golovi[2] > golovi[0])
                {
                    bodovi[2] += 3;
                    golrazlika[0] = golrazlika[0] + golovi[0] - golovi[2];
                    golrazlika[2] = golrazlika[2] + golovi[2] - golovi[0];
                }
                else
                {
                    bodovi[0] += 1;
                    bodovi[2] += 1;
                }
                if (golovi[3] > golovi[1])
                {
                    bodovi[3] += 3;
                    golrazlika[3] = golrazlika[3] + golovi[3] - golovi[1];
                    golrazlika[1] = golrazlika[1] + golovi[1] - golovi[3];
                }
                else if (golovi[1] > golovi[3])
                {
                    bodovi[1] += 3;
                    golrazlika[3] = golrazlika[3] + golovi[3] - golovi[3];
                    golrazlika[1] = golrazlika[1] + golovi[1] - golovi[3];
                }
                else
                {
                    bodovi[3] += 1;
                    bodovi[1] += 1;
                }

                if (brojac == kolo)
                    break;
                else
                    continue;

            case 3:
                if (golovi[3] > golovi[0])
                {
                    bodovi[3] += 3;
                    golrazlika[3] = golrazlika[3] + golovi[3] - golovi[0];
                    golrazlika[0] = golrazlika[0] + golovi[0] - golovi[3];
                }
                else if (golovi[0] > golovi[3])
                {
                    bodovi[0] += 3;
                    golrazlika[3] = golrazlika[3] + golovi[3] - golovi[0];
                    golrazlika[0] = golrazlika[0] + golovi[0] - golovi[3];
                }
                else
                {
                    bodovi[0] += 1;
                    bodovi[3] += 1;
                }
                if (golovi[1] > golovi[2])
                {
                    bodovi[1] += 3;
                    golrazlika[1] = golrazlika[1] + golovi[1] - golovi[2];
                    golrazlika[2] = golrazlika[2] + golovi[2] - golovi[1];
                }
                else if (golovi[2] > golovi[1])
                {
                    bodovi[2] += 3;
                    golrazlika[1] = golrazlika[1] + golovi[1] - golovi[2];
                    golrazlika[2] = golrazlika[2] + golovi[2] - golovi[1];
                }
                else
                {
                    bodovi[2] += 1;
                    bodovi[1] += 1;
                }

                if (brojac == kolo)
                    break;
                else
                    continue;
        }
        brojac++;
    }
    for (int i = 0; i < 4; i++)
    {
        Osvjezeno.Add(drzave[i], (bodovi[i], golrazlika[i]));
    }

    return Osvjezeno;
}

string[] SortiraniPoBodovima(Dictionary<string, (int, int)> tablica)
{
    string[] sortirani = new string[tablica.Count];
    int k = 0;
    string temp;
    foreach (var t in tablica)
    {
        sortirani[k] = t.Key;
        k++;
    }
    for (int j = 0; j <= sortirani.Length - 2; j++)
    {
        for (int i = 0; i <= sortirani.Length - 2; i++)
        {
            if (tablica[sortirani[i]].Item1 < tablica[sortirani[i + 1]].Item1)
            {
                temp = sortirani[i + 1];
                sortirani[i + 1] = sortirani[i];
                sortirani[i] = temp;
            }
        }
    }
    return sortirani;
}

Dictionary<string, (string, int)> Prvih11(Dictionary<string, (string, int)> players) {
    Dictionary<string, (string, int)> PrvaPostava = new Dictionary<string, (string, int)>();
    string[] sortirani = SortinranoPoRatinguSilazno(players);
    int[] postava = { 0, 0, 0, 0 };
    foreach (var player in sortirani) {
        if (PrvaPostava.Count == 11)
            return PrvaPostava;
        else{
            switch (players[player].Item1) {
                case "GK":
                    if (postava[0] < 1)
                    {
                        PrvaPostava.Add(player, players[player]);
                        postava[0]++;
                        continue;
                    }
                    else
                        continue;
                case "DF":
                    if (postava[1] < 4){
                        PrvaPostava.Add(player, players[player]);
                        postava[1]++;
                        continue;
                    }
                    else
                        continue;
                case "MF":
                    if (postava[2] < 3){
                        PrvaPostava.Add(player, players[player]);
                        postava[2]++;
                        continue;
                    }
                    else
                        continue;
                case "FW":
                    if (postava[3] < 3){
                        PrvaPostava.Add(player, players[player]);
                        postava[3]++;
                        continue;
                    }
                    else
                        continue;
            }
        }
    }

    return PrvaPostava;
}

Dictionary<string, (string, int)> UnosIgraca(Dictionary<string, (string position, int rating)> players) {
    Console.WriteLine("Unesi ime novog igraca");
    string ime;
    bool poklapanje;
    do
    {
        poklapanje = false;
        ime = Console.ReadLine();
        foreach (var player in players) {
            if (player.Key.Equals(ime)){
                Console.WriteLine("Već postoji igrac sa imenom koji ste unijeli");
                poklapanje=true;
            }
        }
    } while (poklapanje);
    Console.WriteLine("Unesi poziciju novog igraca(GK, DF, MF, FW)");
    string pozicija;
    do
    {
        pozicija = Console.ReadLine().ToUpper();
        if (pozicija != "GK" && pozicija != "DF" && pozicija != "MF" && pozicija != "FW") {
            Console.WriteLine("Unijeli ste nepostojeću poziciju");
        }
    } while (pozicija != "GK" && pozicija != "DF" && pozicija != "MF" && pozicija != "FW");
    Console.WriteLine("Unesi rejting igraca(1-100)");
    int rej;
    bool rejting;
    do
    {
        rejting = int.TryParse(Console.ReadLine(), out rej);
        if (!rejting)
        {
            Console.WriteLine("Neispravan unos");
            rej = -1;
        }
        else if (rej < 1 || rej > 100) {
            Console.WriteLine("Unijeli ste broj izvan raspona");
        }
    } while (rej<1 ||  rej>100);
    if (PotvrdaPromjene())
    {
        players.Add(ime, (pozicija, rej));
    }
    return players;
}

Dictionary<string, (string, int)> BrisanjeIgraca(Dictionary<string, (string position, int rating)> players) {
    Console.WriteLine("Unesi ime i prezime igraca");
    string ime;
    bool poklapanje = false;
    do
    {
        ime = Console.ReadLine();
        foreach (var player in players)
        {
            if (player.Key.Equals(ime))
            {
                poklapanje = true;
            }
        }
        if (!poklapanje)
        {
            Console.WriteLine("Ne postoji igrac sa unesenim imenom i prezimenom");
        }
    } while (!poklapanje);
    if (PotvrdaPromjene())
    {
        players.Remove(ime);
    }
    return players;
}

Dictionary<string, (string, int)> UredivanjeIgraca(Dictionary<string, (string position, int rating)> players) {
    Console.WriteLine("1 - Uredi ime i prezime igraca");
    Console.WriteLine("2 - Uredi poziciju igraca");
    Console.WriteLine("3 - Uredi rating igraca (od 1 do 100)");
    int izbor;
    bool iz;
    string ime;
    bool poklapanje;
    do {
        iz = int.TryParse(Console.ReadLine(), out izbor);
        if (!iz) {
            Console.WriteLine("Neisprasvan unos");
            izbor = 0;
        }
        if (izbor < 1 || izbor > 3) {
            Console.WriteLine("Uneseni broj ne oznacava nepostojecu radnju");
        }
    }while (izbor < 1 || izbor > 3);

    switch (izbor) {
        case 1:
            Console.WriteLine("Unesi ime igraca kojemu zelis promjeniti ime i prezime");
            string novoIme;
            poklapanje = false;
            (string, int) value = ("", 0);
            do{
                ime = Console.ReadLine();
                foreach (var player in players){
                    if (player.Key.Equals(ime)){
                        poklapanje = true;
                    }
                }
                if (!poklapanje){
                    Console.WriteLine("Ne postoji igrac sa unesenim imenom i prezimenom");
                }
            } while (!poklapanje);
            Console.WriteLine("Unesi novo ime i prezime za odabranog igraca");
            do
            {
                novoIme = Console.ReadLine();
                foreach (var player in players) {
                    if (player.Key.Equals(novoIme)) {
                        Console.WriteLine("Već postoji igrač sa imenom koji želite unijeti, molim unesite novo ime");
                        poklapanje= true;
                    }
                }
            } while (poklapanje);
            foreach(var player in players){
                if (player.Key.Equals(ime))
                {
                    value = player.Value;
                }
            }
            if (PotvrdaPromjene())
            {
                players.Remove(ime);
                players.Add(novoIme, value);
            }
            return players;

        case 2:
            Console.WriteLine("Unesi ime igraca kojemu zelis promijeniti poziciju");
            poklapanje = false;
            do
            {
                ime = Console.ReadLine();
                foreach (var player in players)
                {
                    if (player.Key.Equals(ime))
                    {
                        poklapanje = true;
                    }
                }
                if (!poklapanje)
                {
                    Console.WriteLine("Ne postoji igrac sa unesenim imenom i prezimenom");
                }
            } while (!poklapanje);

            string pozicija;
            do
            {
                pozicija = Console.ReadLine();
                if (pozicija != "GK" && pozicija != "DF" && pozicija != "MF" && pozicija != "FW")
                    Console.WriteLine("Ne postoji unesena pozicija");
            } while (pozicija != "GK" && pozicija != "DF" && pozicija != "MF" && pozicija != "FW");
            int rejting = players[ime].rating;
            if (PotvrdaPromjene())
            {
                players.Remove(ime);
                players.Add(ime, (pozicija, rejting));
            }
            return players;

        case 3:
            Console.WriteLine("Unesi ime igraca kojemu zelis promijeniti rejting");
            poklapanje = false;
            do
            {
                ime = Console.ReadLine();
                foreach (var player in players)
                {
                    if (player.Key.Equals(ime))
                    {
                        poklapanje = true;
                    }
                }
                if (!poklapanje)
                {
                    Console.WriteLine("Ne postoji igrac sa unesenim imenom i prezimenom");
                }
            } while (!poklapanje);

            int rating;
            bool rat;

            Console.WriteLine("Unesi novi rating za odabranog igraca");

            do
            {
                rat = int.TryParse(Console.ReadLine(), out rating);
                if (!rat)
                {
                    Console.WriteLine("Neispravan unos");
                    rating = -1;
                }
                else if (rating < 1 || rating > 100)
                {
                    Console.WriteLine("Unijeli ste broj izvan raspona");
                }
            } while (rating < 1 || rating > 100);

            var pose = players[ime].position;
            if (PotvrdaPromjene())
            {
                players.Remove(ime);
                players.Add(ime, (pose, rating));
            }

            return players;
        default:
            return players;
    }
}



int PocetakIliKraj() {
    int unos;
    Console.WriteLine("1-Vraćanje na početni izbornik");
    Console.WriteLine("0-Izlaz iz aplikacije");
    do
    {
        bool parsiranje2;
        parsiranje2 = int.TryParse(Console.ReadLine(), out unos);
        if (!parsiranje2)
        {
            Console.WriteLine("Neispravan unos");
            unos = -1;
        }
        else if (unos < 0 || unos > 1)
            Console.WriteLine("Uneseni broj ne označava ni jednu moguću radnju");
    } while (unos < 0 || unos > 1);
    Console.Clear();
    return unos;
}

bool PotvrdaPromjene(){
    Console.WriteLine("Potvrdi promjene(D/N)");
    string unos = "";
    do
    {
        unos = Console.ReadLine();
        if (unos != "D" && unos != "d" && unos != "N" && unos != "n") {
            Console.WriteLine("Unos ne oznacava ništa");
        }
    }while(unos != "D" && unos != "d" && unos != "N" && unos != "n");
    if (unos == "D" || unos == "d")
        return true;
    else
        return false;
}
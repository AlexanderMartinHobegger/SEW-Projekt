


namespace projektRebuiltFunktionierenBItte
{
    public partial class MainPage : ContentPage
    {

        int rechnungEveryone;


        public static Dictionary<string, (int numberTrue, int numberFalse, int calculationsDone)> statistic1;

        string[] falschantwortSprüche = new string[]
        {
            "Ups... das war wohl nix!",
            "Mathe ist halt doch kein Bauchgefühl.",
            "Das war so falsch, Pythagoras weint.",
            "Schon mal was von Taschenrechner gehört?",
            "Wenn das 'ne Prüfung wär... wär's jetzt peinlich 😬",
            "Ach komm, das war Absicht, oder?",
            "Mut zur Lücke, aber die war zu groß!",
            "Bitte nicht nochmal so.",
            "So daneben, das ist schon Kunst.",
            "Da hat selbst der Taschenrechner gezuckt.",
            "Mathe ist kein Wunschkonzert 🎶",
            "Rechnen? War wohl gestern!",
            "Nicht mal nah dran 😅",
            "Immerhin warst du schnell falsch!",
            "Hoffentlich war das ein Zahlendreher...",
            "Oh oh... denk nochmal nach!",
            "Wenn das ein Schuss war, ging er meilenweit vorbei!",
            "Falscher als Montagmorgen!",
            "Das war... kreativ. Aber falsch.",
            "Zum Glück gibt's keine Minus-Punkte... oder doch?"
        };
        string[] richtigAntwortSprüche = {
            "Perfekt! ...wurde auch Zeit.",
            "Na endlich mal richtig.",
            "Wow! Du kannst Mathe? 😲",
            "Ich bin... beeindruckt. Ehrlich.",
            "Nicht schlecht. Für deine Verhältnisse.",
            "Langsam wird's gefährlich – für die Aufgaben.",
            "Du hast richtig geantwortet. Zufall?",
            "YES! Du hast einen von 1000 Momenten erwischt.",
            "Könnte man fast für Talent halten.",
            "Gut geraten – oder war’s doch Können?",
            "Mathe-Maschine aktiviert. Kurz jedenfalls.",
            "Uff, das war knapp. Für mich – vor Scham.",
            "Wuhu! Direkt mal Screenshot machen!",
            "Wow… aus Versehen richtig?",
            "Selbst eine kaputte Uhr hat 2× am Tag recht…",
            "Na toll… jetzt musst du weitermachen.",
            "Ich glaub, das war geraten – gib’s zu!",
            "Respekt. Das war nicht komplett peinlich.",
            "Endlich! Schon gedacht du schläfst ein.",
            "Das war... nicht so schlimm wie erwartet.",
            "Du hast getroffen – oder dein Haustier hat geantwortet?",
            "Wenn du so weitermachst, wirst du fast brauchbar.",
            "War das wirklich du, oder hat dir jemand geholfen?",
            "Okay... das war nicht völlig dumm.",
            "Nicht schlecht. Für deine Verhältnisse.",
            "Wow. Ein Funken Hoffnung!",
            "Richtige Antwort. Ausnahmsweise.",
            "Ich bin… überrascht. Positiv, (ne doch nicht!)"

    };









        public MainPage()
        {
            InitializeComponent();

            statistic1 = new Dictionary<string, (int numberTrue, int numberFalse, int calculationsDone)>();

            statistic1["Leicht"] = (0, 0, 0);
            statistic1["Mittel"] = (0, 0, 0);
            statistic1["Schwer"] = (0, 0, 0);

        }
        private string GetSchwierigkeit()
        {
            if (leicht.IsChecked)
                return "Leicht";
            else if (mittel.IsChecked)
                return "Mittel";
            else
                return "Schwer";
        }
        private void schwierigkeit_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            statisticUpdate();
            if (bBereit.IsVisible == true)
            {
                return;
            }

            bBereit_Clicked(sender, e);




        }
        private void statisticUpdate()
        {


            lSchwierigkeitStatistik.Text = GetSchwierigkeit();
            lAufgabenGemacht.Text = "Gesamte Aufgabenanzahl: " + statistic1[GetSchwierigkeit()].calculationsDone;
            lAnzahlFalsch.Text = "Falsch: " + statistic1[GetSchwierigkeit()].numberFalse;
            lAnzahlRichtig.Text = "Richtig: " + statistic1[GetSchwierigkeit()].numberTrue;
            if (statistic1[GetSchwierigkeit()].calculationsDone == 0)
            {
                lErfolgsquote.Text = "Quote: " + (statistic1[GetSchwierigkeit()].numberTrue * 100) / 1;
                return;
            }
            lErfolgsquote.Text = "Quote: " + (statistic1[GetSchwierigkeit()].numberTrue * 100) / statistic1[GetSchwierigkeit()].calculationsDone + "%";

            return;

        }

        private void bBereit_Clicked(object sender, EventArgs e)
        {
           


            eRechnung.Text = "";
            bNächsteAufgabe.IsVisible = false;
            bRight.IsVisible = false;
            lRight.IsVisible = false;
            bWrong.IsVisible = false;
            lWrong.IsVisible = false;
            eRechnung.IsVisible = true;



            if (leicht.IsChecked)
            {
                (string, int) rechnung1 = OperationEasy();
                rechnungEveryone = rechnung1.Item2;
                bBereit.IsVisible = false;
                lRechnung.Text = rechnung1.Item1;
                rechnung.IsVisible = true;
                eRechnung.Focus();
                return;
            }
            else if (mittel.IsChecked)
            {
                (string, int) rechnung1 = OperationMedium();
                rechnungEveryone = rechnung1.Item2;
                bBereit.IsVisible = false;
                lRechnung.Text = rechnung1.Item1;
                rechnung.IsVisible = true;
                eRechnung.Focus();
                return;

            }
            else if (schwer.IsChecked)
            {
                (string, int) rechnung1 = OperationHard();
                rechnungEveryone = rechnung1.Item2;
                bBereit.IsVisible = false;
                lRechnung.Text = rechnung1.Item1;
                rechnung.IsVisible = true;
                eRechnung.Focus();
                return;
            }
            else
            {
                DisplayAlert("Fehler", "Bitte wählen Sie eine Schwierigkeit aus!", "OK");
                return;
            }

            

        }
        private void eRechnung_Completed(object sender, EventArgs e)
        {
            Random spruchGenerator = new Random();
            bWrong.IsVisible = false;
            lWrong.IsVisible = false;

            (int numberTrue, int NumberFalse, int calculationsDone) currentValues = statistic1[GetSchwierigkeit()];

            int solution = int.Parse(eRechnung.Text);
            if (solution == rechnungEveryone)
            {

                int spruch = spruchGenerator.Next(richtigAntwortSprüche.Length);

                lRight.Text = richtigAntwortSprüche[spruch];
                eRechnung.Text = "";
                bRight.IsVisible = true;
                lRight.IsVisible = true;
                eRechnung.IsVisible = false;

                statistic1[GetSchwierigkeit()] = (currentValues.numberTrue + 1, currentValues.NumberFalse, currentValues.calculationsDone + 1);


                bNächsteAufgabe.IsVisible = true;


            }
            else
            {
                eRechnung.Text = "";
                int spruch = spruchGenerator.Next(falschantwortSprüche.Length);
               

                lWrong.Text = falschantwortSprüche[spruch];
                bWrong.IsVisible = true;
                lWrong.IsVisible = true;
                eRechnung.IsVisible = false;

                statistic1[GetSchwierigkeit()] = (currentValues.numberTrue, currentValues.NumberFalse + 1, currentValues.calculationsDone + 1);

                bNächsteAufgabe.IsVisible = true;
            }

            statisticUpdate();

        }




        private (string, int) OperationEasy()
        {
            Random randomOperatorGenerator = new Random();

            int numb = randomOperatorGenerator.Next(2, 50);
            int numb2 = randomOperatorGenerator.Next(2, 50);
            string[] operators = ["+", "-"];
            int operate = randomOperatorGenerator.Next(0, 2);


            int rechnungInt;
            if (operators[operate] == "+")
                rechnungInt = numb + numb2;
            else
                rechnungInt = numb - numb2;

            string rechnung = $"{numb} {operators[operate]} {numb2}";
            return (rechnung, rechnungInt);

        }

        private (string, int) OperationMedium()
        {
            Random randomOperatorGenerator = new Random();

            int numb = randomOperatorGenerator.Next(2, 100);
            int numb2 = randomOperatorGenerator.Next(2, 100);
            string[] operators = ["+", "-", "*", "/"];
            int operate = randomOperatorGenerator.Next(0, 4);
            if (operate == 3)
            {
                while (numb % numb2 != 0 || numb == numb2)
                {
                    numb2 = randomOperatorGenerator.Next(2, 100);
                    if (IsPrim(numb))
                    {
                        numb = randomOperatorGenerator.Next(2, 100);
                    }
                }
            }

            int rechnungInt;
            if (operators[operate] == "+")
                rechnungInt = numb + numb2;
            else if (operators[operate] == "-")
                rechnungInt = numb - numb2;
            else if (operators[operate] == "*")
            {
                numb2 = randomOperatorGenerator.Next(0, 16);
                rechnungInt = numb * numb2;
            }
            else
            {
                rechnungInt = numb / numb2;
            }
            string rechnung = $"{numb} {operators[operate]} {numb2}";
            return (rechnung, rechnungInt);

        }

        private (string, int) OperationHard()
        {
            Random randomOperatorGenerator = new Random();

            int numb = randomOperatorGenerator.Next(2, 1000);
            int numb2 = randomOperatorGenerator.Next(2, 1000);
            string[] operators = ["+", "-", "*", "/"];
            int operate = randomOperatorGenerator.Next(0, 4);
            if (operate == 3)
            {
                while (numb % numb2 != 0 || numb == numb2)
                {
                    numb2 = randomOperatorGenerator.Next(2, 100);
                    if (IsPrim(numb))
                    {
                        numb = randomOperatorGenerator.Next(2, 100);
                    }
                }
            }

            int rechnungInt;
            if (operators[operate] == "+")
                rechnungInt = numb + numb2;
            else if (operators[operate] == "-")
                rechnungInt = numb - numb2;
            else if (operators[operate] == "*")
            {
                numb2 = randomOperatorGenerator.Next(0, 101);
                rechnungInt = numb * numb2;
            }
            else
            {
                rechnungInt = numb / numb2;
            }
            string rechnung = $"{numb} {operators[operate]} {numb2}";
            return (rechnung, rechnungInt);

        }


        private bool IsPrim(int number)
        {
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;

                }
            }
            return true;
        }

        private void Reset()
        {
            bRight.IsVisible = false;
            lRight.IsVisible = false;
            bWrong.IsVisible = false;
            bWrong.IsVisible = false;
            rechnung.IsVisible = false;
            bBereit.IsVisible = true;
        }


    }

}

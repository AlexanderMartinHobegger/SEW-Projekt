


namespace projektRebuiltFunktionierenBItte
{
    public partial class MainPage : ContentPage
    {

        int rechnungEveryone;
        

        public static Dictionary<string, (int numberTrue, int numberFalse, int calculationsDone)> statistic1;


       






        public MainPage()
        {
            InitializeComponent();

            statistic1 = new Dictionary<string, (int numberTrue, int numberFalse, int calculationsDone)>();

            statistic1["Leicht"] = (0, 0, 0);
            statistic1["Mittel"] = (0, 0, 0) ;
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
        private void statisticUpdate() {


            lSchwierigkeitStatistik.Text = GetSchwierigkeit();
            lAufgabenGemacht.Text = "Gesamte Aufgabenanzahl: " + statistic1[GetSchwierigkeit()].calculationsDone;
            lAnzahlFalsch.Text = "Falsch: " + statistic1[GetSchwierigkeit()].numberFalse;
            lAnzahlRichtig.Text = "Richtig: " + statistic1[GetSchwierigkeit()].numberTrue;
            if (statistic1[GetSchwierigkeit()].calculationsDone == 0)
            {
                lErfolgsquote.Text = "Quote: " + (statistic1[GetSchwierigkeit()].numberTrue * 100) / 1;
                return;
            }
            lErfolgsquote.Text = "Quote: " + (statistic1[GetSchwierigkeit()].numberTrue * 100)/ statistic1[GetSchwierigkeit()].calculationsDone + "%";

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

                return;
            }
            else if (mittel.IsChecked)
            {
                (string, int) rechnung1 = OperationMedium();
                rechnungEveryone = rechnung1.Item2;
                bBereit.IsVisible = false;
                lRechnung.Text = rechnung1.Item1;
                rechnung.IsVisible = true;

                return;

            }
            else if (schwer.IsChecked)
            {
                (string, int) rechnung1 = OperationHard();
                rechnungEveryone = rechnung1.Item2;
                bBereit.IsVisible = false;
                lRechnung.Text = rechnung1.Item1;
                rechnung.IsVisible = true;

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
           
            bWrong.IsVisible = false;
            lWrong.IsVisible = false;

            (int numberTrue, int NumberFalse, int calculationsDone) currentValues = statistic1[GetSchwierigkeit()];

            int solution = int.Parse(eRechnung.Text);
            if (solution == rechnungEveryone)
            {


                lRight.Text = $"Richtig!";
                eRechnung.Text = "";
                bRight.IsVisible = true;
                lRight.IsVisible = true;
                eRechnung.IsVisible = false;

                statistic1[GetSchwierigkeit()] = (currentValues.numberTrue+1, currentValues.NumberFalse, currentValues.calculationsDone +1);

               
                bNächsteAufgabe.IsVisible = true;


            }
            else
            {
                eRechnung.Text = "";

                
                lWrong.Text = $"Falsch!, Schade...";
                bWrong.IsVisible = true;
                lWrong.IsVisible = true;
                eRechnung.IsVisible = false;

                statistic1[GetSchwierigkeit()] = (currentValues.numberTrue, currentValues.NumberFalse+1, currentValues.calculationsDone+1);
               
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

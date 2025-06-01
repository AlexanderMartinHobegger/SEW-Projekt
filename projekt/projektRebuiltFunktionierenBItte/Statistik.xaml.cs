namespace projektRebuiltFunktionierenBItte;


public partial class Statistik : ContentPage
{
    Dictionary<string, (int numberTrue, int numberFalse, int calculationsDone)> statisticsTransformed = MainPage.statistic1;
    public Statistik()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateStatistik(statisticsTransformed);
    }

    public void UpdateStatistik(Dictionary<string, (int numberTrue, int numberFalse, int calculationsDone)> statistik)
    {
        (int numberTrue, int numberFalse, int calculationsDone) leicht = statistik["Leicht"];
        (int numberTrue, int numberFalse, int calculationsDone) mittel = statistik["Mittel"];
        (int numberTrue, int numberFalse, int calculationsDone) schwer = statistik["Schwer"];





        lLeichtHowManyDone.Text = "Aufgabenanzahl Insgesamt: " +leicht.calculationsDone.ToString();
        lLeichtNumberTrue.Text = "Aufgaben Richtig Beantwortet: " + leicht.numberTrue.ToString();
        lLeichtNumberFalse.Text = "Aufgaben Falsch Beantwortet: " +leicht.numberFalse.ToString();
        if (leicht.calculationsDone == 0)
            leicht.calculationsDone = 1;
        lLeichtQuote.Text ="Erfolgsquote: " +((leicht.numberTrue * 100) / leicht.calculationsDone).ToString() + "%";

        lMediumHowManyDone.Text = "Aufgabenanzahl Insgesamt: " + mittel.numberTrue.ToString();
        lMediumNumberTrue.Text = "Aufgaben Richtig Beantwortet: " + mittel.numberTrue.ToString();
        lMediumNumberFalse.Text = "Aufgaben Falsch Beantwortet: " + mittel.numberFalse.ToString();
        if (mittel.calculationsDone == 0)
            mittel.calculationsDone = 1;
        lMediumQuote.Text = "Erfolgsquote: " + ((mittel.numberTrue * 100) / mittel.calculationsDone).ToString() + "%";

        lHardHowManyDone.Text = "Aufgabenanzahl Insgesamt: " + schwer.calculationsDone.ToString();
        lHardNumberTrue.Text = "Aufgaben Richtig Beantwortet: " + schwer.numberTrue.ToString();
        lHardNumberFalse.Text = "Aufgaben Richtig Beantwortet: " + schwer.numberFalse.ToString();
        if (schwer.calculationsDone == 0)
            schwer.calculationsDone = 1;
        lHardQuote.Text = "Erfolgsquote: " +((schwer.numberTrue * 100) / schwer.calculationsDone).ToString() +"%";


    }


    private void zurücksetzen_Clicked(object sender, EventArgs e)
    {
       
        MainPage.statistic1["Leicht"] = (0, 0, 0);
        MainPage.statistic1["Mittel"] = (0, 0, 0);
        MainPage.statistic1["Schwer"] = (0, 0, 0);
        UpdateStatistik(MainPage.statistic1);
    }
}
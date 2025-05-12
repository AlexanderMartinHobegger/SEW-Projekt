

using System.Security.Cryptography;

namespace ThirtySevenProjekt {
    public partial class MainPage : ContentPage {

        int rechnungEveryone;
        int numberRichtig = 0;
        int numberRichtigRow = 0;
        int calculationsDone = 1;
        public MainPage() {
            InitializeComponent();
        }

        private async void bBereit_Clicked(object sender, EventArgs e) {



            eRechnung.Text = "";
            bNächsteAufgabe.IsVisible = false;
            bRight.IsVisible = false;
            lRight.IsVisible = false;
            bWrong.IsVisible = false;
            lWrong.IsVisible = false;
            eRechnung.IsVisible = true;

            

            if (leicht.IsChecked == true) {
                
            } else if (mittel.IsChecked == true) {
                (string, int) rechnung1 = OperationMedium();
                rechnungEveryone = rechnung1.Item2;
                bBereit.IsVisible = false;
                lRechnung.Text = rechnung1.Item1;
                rechnung.IsVisible = true;

                return;

            } else if (schwer.IsChecked == true) {
                
            } else {
                DisplayAlert("Fehler", "Bitte wählen Sie eine Schwierigkeit aus!", "OK");
                return;
            }

            

        }
        private async void eRechnung_Completed(object sender, EventArgs e) {
            if(eRechnung.Text == null) {
                return;
            }
            bWrong.IsVisible = false;
            lWrong.IsVisible=false;
            
            int solution = int.Parse(eRechnung.Text);
            if( solution == rechnungEveryone) {
                
                numberRichtig++;

                if (calculationsDone == 5) {
                    
                    eRechnung.IsVisible = false;
                    bRight.IsVisible = true;
                    lRight.IsVisible = true;
                    eRechnung.IsVisible = false;

                    lRight.Text = $"{numberRichtig}/{calculationsDone} Richtig ";
                    await Task.Delay(3000);

                    calculationsDone = 0;
                    numberRichtig = 0;
                    Reset();
                    return;
                }

                lRight.Text = $"Richtig!";
                eRechnung.Text = "";
                bRight.IsVisible = true;
                lRight.IsVisible = true;
                eRechnung.IsVisible = false;
        
                
                calculationsDone++;
                bNächsteAufgabe.IsVisible = true;
                
            } else {
                eRechnung.Text = "";
                
                
                lWrong.Text = $"Falsch!, Schade...";
                bWrong.IsVisible = true;
                lWrong.IsVisible = true;
                eRechnung.IsVisible = false;
                
                if (calculationsDone == 5) {
                    
                    bWrong.IsVisible = true;
                    lWrong.IsVisible = true;
                    eRechnung.IsVisible = false;

                    lWrong.Text = $"{numberRichtig}/{calculationsDone} Richtig!";
                    await Task.Delay(3000);

                    calculationsDone = 0;
                    numberRichtig = 0;
                    Reset();
                    return;
                }
                calculationsDone++;
                await Task.Delay(2000);
                bBereit_Clicked(sender, e);


            }
        }


        private void schwierigkeit_CheckedChanged(object sender, CheckedChangedEventArgs e) {
            if(bBereit.IsVisible == true) {
                return;
            }
            bBereit_Clicked(sender,  e);




        }

        private (string, int) OperationMedium() {
            Random randomOperatorGenerator = new Random();

            int numb = randomOperatorGenerator.Next(2, 100);
            int numb2 = randomOperatorGenerator.Next(2, 100);
            string[] operators = ["+", "-", "*", "/"];
            int operate = randomOperatorGenerator.Next(0, 4);
            if (operate == 3) {
                while (numb % numb2 != 0 || numb == numb2) {
                    numb2 = randomOperatorGenerator.Next(2, 100);
                    if (IsPrim(numb)) {
                        numb = randomOperatorGenerator.Next(2, 100);
                    }
                }
            }
            string rechnung = $"{numb} {operators[operate]} {numb2}";
            int rechnungInt;
            if (operators[operate] == "+")
                rechnungInt = numb + numb2;
            else if (operators[operate] == "-")
                rechnungInt = numb - numb2;
            else if (operators[operate] == "*")
                rechnungInt = numb * numb2;
            else {
                rechnungInt = numb / numb2;
            }
                return (rechnung, rechnungInt);
            
        }
        private bool IsPrim(int number) {
            for (int i = 2; i < number; i++) {
                if (number % i == 0) {
                    return false;
                    
                }
            }
            return true;
        }
        private void Reset() {
            bRight.IsVisible = false;
            lRight.IsVisible = false;
            bWrong.IsVisible = false;
            bWrong.IsVisible = false;
            rechnung.IsVisible = false;
            bBereit.IsVisible = true;
        }
        

    }
}

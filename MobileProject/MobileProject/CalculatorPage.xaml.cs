namespace MobileProject;

using MobileProject.Models;
using MobileProject.Services;

public partial class CalculatorPage : ContentPage
{
    ServiceController dataController = App.DataController;

    double firstEntryAmount;
    Currency firstCurrencyPick;
    double secondEntryAmount;
    Currency secondCurrencyPick;
    double thirdEntryAmount;
    Currency thirdCurrencyPick;
    double fourthEntryAmount;
    Currency fourthCurrencyPick;
    double fifthEntryAmount;
    Currency fifthCurrencyPick;
    double sixthEntryAmount;
    Currency sixthCurrencyPick;
    double resultAmount;
    Currency resultCurrencyPick;

    public CalculatorPage()
    {
        InitializeComponent();

        Reset();
    }


    private void CalculateSum()
    {
        if (resultCurrencyPick == null) { return; }


        double sumToUSD = 0;

        if (firstCurrencyPick != null && firstEntryAmount != 0)
        {
            double amountToUSD = firstEntryAmount / Math.Round(firstCurrencyPick.LatestRatePerUSD, 6);
            sumToUSD += amountToUSD;
        }

        if (secondCurrencyPick != null && secondEntryAmount != 0)
        {
            double amountToUSD = secondEntryAmount / Math.Round(secondCurrencyPick.LatestRatePerUSD, 6);
            sumToUSD += amountToUSD;
        }

        if (thirdCurrencyPick != null && thirdEntryAmount != 0)
        {
            double amountToUSD = thirdEntryAmount / Math.Round(thirdCurrencyPick.LatestRatePerUSD, 6);
            sumToUSD += amountToUSD;
        }

        if (fourthCurrencyPick != null && fourthEntryAmount != 0)
        {
            double amountToUSD = fourthEntryAmount / Math.Round(fourthCurrencyPick.LatestRatePerUSD, 6);
            sumToUSD += amountToUSD;
        }

        if (fifthCurrencyPick != null && fifthEntryAmount != 0)
        {
            double amountToUSD = fifthEntryAmount / Math.Round(fifthCurrencyPick.LatestRatePerUSD, 6);
            sumToUSD += amountToUSD;
        }

        if (sixthCurrencyPick != null && sixthEntryAmount != 0)
        {
            double amountToUSD = sixthEntryAmount / Math.Round(sixthCurrencyPick.LatestRatePerUSD, 6);
            sumToUSD += amountToUSD;
        }

        double sumToResultCurrency = sumToUSD * Math.Round(resultCurrencyPick.LatestRatePerUSD, 6);
        resultAmount = sumToResultCurrency;

        ResultLabel.Text = resultAmount.ToString("F3");
    }

    private void Reset()
    {
        firstEntryAmount = 0;
        FirstEntry.Text = firstEntryAmount.ToString();

        secondEntryAmount = 0;
        SecondEntry.Text = secondEntryAmount.ToString();

        thirdEntryAmount = 0;
        ThirdEntry.Text = thirdEntryAmount.ToString();

        fourthEntryAmount = 0;
        FourthEntry.Text = fourthEntryAmount.ToString();

        fifthEntryAmount = 0;
        FifthEntry.Text = fifthEntryAmount.ToString();

        sixthEntryAmount = 0;
        SixthEntry.Text = sixthEntryAmount.ToString();

        FirstPicker.ItemsSource = null;
        FirstPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));
        firstCurrencyPick = dataController.InterestingOne;
        FirstPicker.SelectedItem = firstCurrencyPick.Symbol;

        SecondPicker.ItemsSource = null;
        SecondPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));
        secondCurrencyPick = dataController.InterestingTwo;
        SecondPicker.SelectedItem = secondCurrencyPick.Symbol;

        ThirdPicker.ItemsSource = null;
        ThirdPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));

        FourthPicker.ItemsSource = null;
        FourthPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));

        FifthPicker.ItemsSource = null;
        FifthPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));

        SixthPicker.ItemsSource = null;
        SixthPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));

        ResultPicker.ItemsSource = null;
        ResultPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));

        resultCurrencyPick = dataController.MyCurrency;
        ResultPicker.SelectedItem = resultCurrencyPick.Symbol;

        CalculateSum();
    }

    private async Task UpdateLatest()
    {
        if (dataController.CheckLatestUpdate())
        {
            await dataController.CurrentRateUpdate();
        }

        int apiNumber = dataController.GetApiRequestNumber();

        if (apiNumber != 0)
        {
            DisplayAlert($"{apiNumber} API Request(s) Used", "Always try the limited use of API request", "OK");
        }
    }

    private void FirstPickerChanged(object sender, EventArgs e)
    {
        if (FirstPicker.SelectedItem == null)
        {
            return;
        }

        foreach (Currency currency in dataController.AllCurrencies)
        {
            if (currency.Symbol == (CurrencySymbol)FirstPicker.SelectedItem)
            {
                firstCurrencyPick = currency;
                break;
            }
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            FirstEntry.Text = "0";
            firstEntryAmount = 0;
            CalculateSum();
        }
    }

    private void FirstEntryFocused(object sender, EventArgs e)
    {
        if (FirstEntry.Text == "0")
        {
            FirstEntry.Text = String.Empty;
            FirstEntry.Focus();
        }
    }

    private void FirstEntryCompleted(object sender, EventArgs e)
    {
        if (FirstPicker.SelectedItem == null && FirstEntry.Text != "0")
        {
            DisplayAlert("No Currency Selected", "Please Choose Currecy Type First!", "OK");
            FirstEntry.Text = "0";
            firstEntryAmount = 0;
            return;
        }

        if (FirstEntry.Text.Contains('.') && FirstEntry.Text.Contains('-'))
        {
            if (FirstEntry.Text.Length > 13)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                FirstEntry.Text = "0";
                firstEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else if (FirstEntry.Text.Contains('.') || FirstEntry.Text.Contains('-'))
        {
            if (FirstEntry.Text.Length > 12)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                FirstEntry.Text = "0";
                firstEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else
        {
            if (FirstEntry.Text.Length > 11)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                FirstEntry.Text = "0";
                firstEntryAmount = 0;
                CalculateSum();
                return;
            }
        }

        if (double.TryParse(FirstEntry.Text, out double doubleResult))
        {
            firstEntryAmount = doubleResult;
        }
        else
        {
            FirstEntry.Text = "0";
            firstEntryAmount = 0;
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            FirstEntry.Text = "0";
            firstEntryAmount = 0;
            CalculateSum();
        }

    }

    private void SecondPickerChanged(object sender, EventArgs e)
    {
        if (SecondPicker.SelectedItem == null)
        {
            return;
        }

        foreach (Currency currency in dataController.AllCurrencies)
        {
            if (currency.Symbol == (CurrencySymbol)SecondPicker.SelectedItem)
            {
                secondCurrencyPick = currency;
                break;
            }
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            SecondEntry.Text = "0";
            secondEntryAmount = 0;
            CalculateSum();
        }
    }

    private void SecondEntryFocused(object sender, EventArgs e)
    {
        if (SecondEntry.Text == "0")
        {
            SecondEntry.Text = String.Empty;
            SecondEntry.Focus();
        }
    }

    private void SecondEntryCompleted(object sender, EventArgs e)
    {
        if (SecondPicker.SelectedItem == null && SecondEntry.Text != "0")
        {
            DisplayAlert("No Currency Selected", "Please Choose Currecy Type First!", "OK");
            SecondEntry.Text = "0";
            return;
        }

        if (SecondEntry.Text.Contains('.') && SecondEntry.Text.Contains('-'))
        {
            if (SecondEntry.Text.Length > 13)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                SecondEntry.Text = "0";
                secondEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else if (SecondEntry.Text.Contains('.') || SecondEntry.Text.Contains('-'))
        {
            if (SecondEntry.Text.Length > 12)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                SecondEntry.Text = "0";
                secondEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else
        {
            if (SecondEntry.Text.Length > 11)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                SecondEntry.Text = "0";
                secondEntryAmount = 0;
                CalculateSum();
                return;
            }
        }

        if (double.TryParse(SecondEntry.Text, out double doubleResult))
        {
            secondEntryAmount = doubleResult;
        }
        else
        {
            SecondEntry.Text = "0";
            secondEntryAmount = 0;
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            SecondEntry.Text = "0";
            secondEntryAmount = 0;
            CalculateSum();
        }
    }

    private void ThirdPickerChanged(object sender, EventArgs e)
    {
        if (ThirdPicker.SelectedItem == null)
        {
            return;
        }

        foreach (Currency currency in dataController.AllCurrencies)
        {
            if (currency.Symbol == (CurrencySymbol)ThirdPicker.SelectedItem)
            {
                thirdCurrencyPick = currency;
                break;
            }
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            ThirdEntry.Text = "0";
            thirdEntryAmount = 0;
            CalculateSum();
        }

    }

    private void ThirdEntryFocused(object sender, EventArgs e)
    {
        if (ThirdEntry.Text == "0")
        {
            ThirdEntry.Text = String.Empty;
            ThirdEntry.Focus();
        }
    }

    private void ThirdEntryCompleted(object sender, EventArgs e)
    {
        if (ThirdPicker.SelectedItem == null && ThirdEntry.Text != "0")
        {
            DisplayAlert("No Currency Selected", "Please Choose Currecy Type First!", "OK");
            ThirdEntry.Text = "0";
            return;
        }

        if (ThirdEntry.Text.Contains('.') && ThirdEntry.Text.Contains('-'))
        {
            if (ThirdEntry.Text.Length > 13)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                ThirdEntry.Text = "0";
                thirdEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else if (ThirdEntry.Text.Contains('.') || ThirdEntry.Text.Contains('-'))
        {
            if (ThirdEntry.Text.Length > 12)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                ThirdEntry.Text = "0";
                thirdEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else
        {
            if (ThirdEntry.Text.Length > 11)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                ThirdEntry.Text = "0";
                thirdEntryAmount = 0;
                CalculateSum();
                return;
            }
        }

        if (double.TryParse(ThirdEntry.Text, out double doubleResult))
        {
            thirdEntryAmount = doubleResult;
        }
        else
        {
            ThirdEntry.Text = "0";
            thirdEntryAmount = 0;
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            ThirdEntry.Text = "0";
            thirdEntryAmount = 0;
            CalculateSum();
        }
    }

    private void FourthPickerChanged(object sender, EventArgs e)
    {
        if (FourthPicker.SelectedItem == null)
        {
            return;
        }

        foreach (Currency currency in dataController.AllCurrencies)
        {
            if (currency.Symbol == (CurrencySymbol)FourthPicker.SelectedItem)
            {
                fourthCurrencyPick = currency;
                break;
            }
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            FourthEntry.Text = "0";
            fourthEntryAmount = 0;
            CalculateSum();
        }
    }

    private void FourthEntryFocused(object sender, EventArgs e)
    {
        if (FourthEntry.Text == "0")
        {
            FourthEntry.Text = String.Empty;
            FourthEntry.Focus();
        }
    }

    private void FourthEntryCompleted(object sender, EventArgs e)
    {
        if (FourthPicker.SelectedItem == null && FourthEntry.Text != "0")
        {
            DisplayAlert("No Currency Selected", "Please Choose Currecy Type First!", "OK");
            FourthEntry.Text = "0";
            return;
        }

        if (FourthEntry.Text.Contains('.') && FourthEntry.Text.Contains('-'))
        {
            if (FourthEntry.Text.Length > 13)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                FourthEntry.Text = "0";
                fourthEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else if (FourthEntry.Text.Contains('.') || FourthEntry.Text.Contains('-'))
        {
            if (FourthEntry.Text.Length > 12)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                FourthEntry.Text = "0";
                fourthEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else
        {
            if (FourthEntry.Text.Length > 11)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                FourthEntry.Text = "0";
                fourthEntryAmount = 0;
                CalculateSum();
                return;
            }
        }

        if (double.TryParse(FourthEntry.Text, out double doubleResult))
        {
            fourthEntryAmount = doubleResult;
        }
        else
        {
            FourthEntry.Text = "0";
            fourthEntryAmount = 0;
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            FourthEntry.Text = "0";
            fourthEntryAmount = 0;
            CalculateSum();
        }
    }

    private void FifthPickerChanged(object sender, EventArgs e)
    {
        if (FifthPicker.SelectedItem == null)
        {
            return;
        }

        foreach (Currency currency in dataController.AllCurrencies)
        {
            if (currency.Symbol == (CurrencySymbol)FifthPicker.SelectedItem)
            {
                fifthCurrencyPick = currency;
                break;
            }
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            FifthEntry.Text = "0";
            fifthEntryAmount = 0;
            CalculateSum();
        }
    }

    private void FifthEntryFocused(object sender, EventArgs e)
    {
        if (FifthEntry.Text == "0")
        {
            FifthEntry.Text = String.Empty;
            FifthEntry.Focus();
        }
    }

    private void FifthEntryCompleted(object sender, EventArgs e)
    {
        if (FifthPicker.SelectedItem == null && FifthEntry.Text != "0")
        {
            DisplayAlert("No Currency Selected", "Please Choose Currecy Type First!", "OK");
            FifthEntry.Text = "0";
            return;
        }

        if (FifthEntry.Text.Contains('.') && FifthEntry.Text.Contains('-'))
        {
            if (FifthEntry.Text.Length > 13)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                FifthEntry.Text = "0";
                fifthEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else if (FifthEntry.Text.Contains('.') || FifthEntry.Text.Contains('-'))
        {
            if (FifthEntry.Text.Length > 12)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                FifthEntry.Text = "0";
                fifthEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else
        {
            if (FifthEntry.Text.Length > 11)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                FifthEntry.Text = "0";
                fifthEntryAmount = 0;
                CalculateSum();
                return;
            }
        }

        if (double.TryParse(FifthEntry.Text, out double doubleResult))
        {
            fifthEntryAmount = doubleResult;
        }
        else
        {
            FifthEntry.Text = "0";
            fifthEntryAmount = 0;
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            FifthEntry.Text = "0";
            fifthEntryAmount = 0;
            CalculateSum();
        }
    }

    private void SixthPickerChanged(object sender, EventArgs e)
    {
        if (SixthPicker.SelectedItem == null)
        {
            return;
        }

        foreach (Currency currency in dataController.AllCurrencies)
        {
            if (currency.Symbol == (CurrencySymbol)SixthPicker.SelectedItem)
            {
                sixthCurrencyPick = currency;
                break;
            }
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            SixthEntry.Text = "0";
            sixthEntryAmount = 0;
            CalculateSum();
        }
    }

    private void SixthEntryFocused(object sender, EventArgs e)
    {
        if (SixthEntry.Text == "0")
        {
            SixthEntry.Text = String.Empty;
            SixthEntry.Focus();
        }
    }

    private void SixthEntryCompleted(object sender, EventArgs e)
    {
        if (SixthPicker.SelectedItem == null && SixthEntry.Text != "0")
        {
            DisplayAlert("No Currency Selected", "Please Choose Currecy Type First!", "OK");
            SixthEntry.Text = "0";
            return;
        }

        if (SixthEntry.Text.Contains('.') && SixthEntry.Text.Contains('-'))
        {
            if (SixthEntry.Text.Length > 13)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                SixthEntry.Text = "0";
                sixthEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else if (SixthEntry.Text.Contains('.') || SixthEntry.Text.Contains('-'))
        {
            if (SixthEntry.Text.Length > 12)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                SixthEntry.Text = "0";
                sixthEntryAmount = 0;
                CalculateSum();
                return;
            }
        }
        else
        {
            if (SixthEntry.Text.Length > 11)
            {
                DisplayAlert("Too long digits (> 11)", "Please input less digits!", "OK");
                SixthEntry.Text = "0";
                sixthEntryAmount = 0;
                CalculateSum();
                return;
            }
        }

        if (double.TryParse(SixthEntry.Text, out double doubleResult))
        {
            sixthEntryAmount = doubleResult;
        }
        else
        {
            SixthEntry.Text = "0";
            sixthEntryAmount = 0;
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please input less amount!", "OK");
            SixthEntry.Text = "0";
            sixthEntryAmount = 0;
            CalculateSum();
        }
    }

    private void ResultPickerChanged(object sender, EventArgs e)
    {
        if (ResultPicker.SelectedItem == null)
        {
            return;
        }

        Currency previousCurrency = resultCurrencyPick;

        foreach (Currency currency in dataController.AllCurrencies)
        {
            if (currency.Symbol == (CurrencySymbol)ResultPicker.SelectedItem)
            {
                resultCurrencyPick = currency;
                break;
            }
        }

        CalculateSum();

        if (Math.Round(resultAmount, 3) >= 10000000000 || Math.Round(resultAmount, 3) <= -10000000000)
        {
            DisplayAlert("Sum output is out of range (-10 Billion < & 10 Billion >)", "Please choose different currency!", "OK");
            resultCurrencyPick = previousCurrency;
            ResultPicker.SelectedItem = previousCurrency.Symbol;
            CalculateSum();
        }
    }

    private async void OnResetClicked(object sender, EventArgs e)
    {
        Reset();
        await UpdateLatest();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateLatest();
    }

}
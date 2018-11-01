using AsynchroneousClientSocket;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenExchangeRatesGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        public static List<BaseRate> BaseRateList = new List<BaseRate>() { new BaseRate("UNK", "Uspecificeret"), new BaseRate("DKK", "Danske Kroner"), new BaseRate("EUR", "Euro"), new BaseRate("GBP", "Britiske Pund"), new BaseRate("USD", "Amerikanske Dollars") };
        public List<Rate> UsdRateList = new List<Rate>();
        public List<Rate> DkkRateList = new List<Rate>();
        public List<Rate> EurRateList = new List<Rate>();
        public List<Rate> GbpRateList = new List<Rate>();
        public AsynchroneousClient CAC = new AsynchroneousClient();
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            FillRateLists();
            ComboBoxBaseRates.ItemsSource = BaseRateList;
            ComboBoxBaseRates.SelectedIndex = 0;
        }

        #region Events
        private void ComboBoxBaseRates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxBaseRates.SelectedIndex)
            {
                case -1:
                    ListBoxRateList.ItemsSource = null;
                    LabelHundred.Content = "100 enheder koster:";
                    break;
                case 0:
                    ListBoxRateList.ItemsSource = null;
                    LabelHundred.Content = "100 enheder koster:";
                    break;
                case 1:
                    ListBoxRateList.ItemsSource = DkkRateList;
                    LabelHundred.Content = "100 DKK koster:";
                    break;
                case 2:
                    ListBoxRateList.ItemsSource = EurRateList;
                    LabelHundred.Content = "100 EUR koster:";
                    break;
                case 3:
                    ListBoxRateList.ItemsSource = GbpRateList;
                    LabelHundred.Content = "100 GBP koster:";
                    break;
                case 4:
                    ListBoxRateList.ItemsSource = UsdRateList;
                    LabelHundred.Content = "100 USD koster:";
                    break;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method, that converts Rate List with US-Dollar Base Rate to requested Base Rates
        /// </summary>
        /// <param name="currencyCode">string - requested Base Rate - 3 character string</param>
        /// <param name="converter">double</param>
        /// <returns>List<Rate></returns>
        private List<Rate> ConvertRateList(string currencyCode, double converter)
        {
            List<Rate> tempList = new List<Rate>();

            foreach (Rate rate in UsdRateList)
            {
                Rate tempRate = new Rate((Rate)rate);
                if (rate.CurrencyCode == currencyCode)
                {
                    tempRate.Value = 100;
                }
                else if (rate.CurrencyCode == "USD")
                {
                    tempRate.Value = converter;
                }
                else
                {
                    tempRate.Value = Convert.ToDouble(rate.Value * converter);
                }
                tempList.Add(tempRate);
            }

            return tempList;
        }

        /// <summary>
        /// Method, that converts Rate List with US-Dollar Base Rate to Rate Lists with DKK, EUR & GBP Base Rates
        /// </summary>
        private void ConvertRateLists()
        {
            double dkkConverter = GetConverter("DKK");
            double eurConverter = GetConverter("EUR");
            double gbpConverter = GetConverter("GBP");
            DkkRateList = ConvertRateList("DKK", dkkConverter);
            EurRateList = ConvertRateList("EUR", eurConverter);
            GbpRateList = ConvertRateList("GBD", gbpConverter);
        }

        /// <summary>
        /// Method, that fills Rate Lists with currency values
        /// </summary>
        private void FillRateLists()
        {
            FillUsdRateList();
            ConvertRateLists();
        }

        /// <summary>
        /// Method, that fills the US-Dollar Rate List with data from server
        /// </summary>
        private void FillUsdRateList()
        {
            string response = "";
            try
            {
                response = CAC.StartClient("OE<EOF>");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Der opstod en fejl under forsøg på at hente valutalister\n" + ex.ToString(), "Hent valutalister", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (response.Length >= 1)
            {
                UsdRateList = new List<Rate>();
                string[] responseData = response.Split(';');
                if (responseData.Count() > 2 && responseData.Count() % 2 != 0)
                {
                    responseData = responseData.Where(w => w != responseData[responseData.Count() - 1]).ToArray();

                    for (int i = 0; i < responseData.Length; i++)
                    {
                        Rate tempRate = new Rate(responseData[i], responseData[i + 1]);
                        UsdRateList.Add(tempRate);
                        i++;
                        i++;
                    }
                }
                else if (responseData.Count() > 2 && responseData.Count() % 2 == 0)
                {
                    for (int i = 0; i < responseData.Length; i++)
                    {
                        Rate tempRate = new Rate(responseData[i], responseData[i + 1]);
                        UsdRateList.Add(tempRate);
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Method, that calculate the value to convert Rates from US-Dollars
        /// </summary>
        /// <param name="currencyCode">string - requested Base Rate - 3 character string</param>
        /// <returns>double</returns>
        private double GetConverter(string currencyCode)
        {
            double result = 0;
            if (UsdRateList.Count >= 1)
            {
                foreach (Rate rate in UsdRateList)
                {
                    if (rate.CurrencyCode == currencyCode)
                    {
                        result = 100 / rate.Value;
                        break;
                    }
                }
            }

            return result;
        }

        #endregion

    }
}

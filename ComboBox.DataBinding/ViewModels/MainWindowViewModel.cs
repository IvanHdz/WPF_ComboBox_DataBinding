using ComboBox.DataBinding.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComboBox.DataBinding.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Item Sources

        public ObservableCollection<ShippingCarrier> ShippingCarriers { get; set; }
        public ObservableCollection<Country> Countries { get; set; }
        public ObservableCollection<ShippingCarrier> AvailableShippingCarriers { get; set; }
        public ObservableCollection<ShippingMethod> AvailableShippingMethods { get; set; }

        #endregion Item Sources

        #region Binding Examples

        // Property backing fields.
        private string selectedSourceCountryTwoLetterCode;

        private Country selectedDestinationCountry;
        private ShippingCarrier selectedShippingCarrier;
        private ShippingMethod selectedShippingMethod;

        /// <summary>
        /// Gets whether or not the user should be able to select a shipping carrier.
        /// </summary>
        public bool AllowShippingCarrierSelection
        {
            get { return (!string.IsNullOrWhiteSpace(this.SelectedSourceCountryTwoLetterCode) && this.SelectedDestinationCountry != null); }
        }

        /// <summary>
        /// Gets whether or not the user should be able to select a shipping method.
        /// </summary>
        public bool AllowShippingMethodSelection
        {
            get { return (this.SelectedShippingCarrier != null); }
        }

        /// <summary>
        /// Gets or sets the shipping source country.
        /// </summary>
        public string SelectedSourceCountryTwoLetterCode
        {
            get { return this.selectedSourceCountryTwoLetterCode; }
            set
            {
                this.selectedSourceCountryTwoLetterCode = value;
                this.NotifyPropertyChanged("SelectedSourceCountryTwoLetterCode");
                this.NotifyPropertyChanged("AllowShippingCarrierSelection");
            }
        }

        /// <summary>
        /// Gets or sets the shipping destination country.
        /// </summary>
        public Country SelectedDestinationCountry
        {
            get { return this.selectedDestinationCountry; }
            set
            {
                // Set the local field value.
                this.selectedDestinationCountry = value;

                // The lists of available shipping carriers/methods are invalidated now, so we need to update it.
                this.AvailableShippingMethods.Clear();
                this.AvailableShippingCarriers.Clear();

                if (value != null)
                {
                    foreach (var carrier in this.ShippingCarriers)
                    {
                        if (carrier.CanShipToCountry(value.TwoLetterCode))
                            this.AvailableShippingCarriers.Add(carrier);
                    }
                }

                this.NotifyPropertyChanged("SelectedDestinationCountry");
                this.NotifyPropertyChanged("AllowShippingCarrierSelection");
            }
        }

        /// <summary>
        /// Gets or sets the selected shipping carrier.
        /// </summary>
        public ShippingCarrier SelectedShippingCarrier
        {
            get { return this.selectedShippingCarrier; }
            set
            {
                this.selectedShippingCarrier = value;

                // The list of available shipping methods is now invalidated.  Clear the list and populate new shipping methods based
                // on the selected carrier and destination country.
                this.AvailableShippingMethods.Clear();

                if (value != null)
                {
                    var methods = value.GetAvailableShippingMethodsForCountry(this.SelectedDestinationCountry.TwoLetterCode);
                    foreach (var method in methods)
                        this.AvailableShippingMethods.Add(method);
                }

                this.NotifyPropertyChanged("SelectedShippingCarrier");
                this.NotifyPropertyChanged("AllowShippingMethodSelection");
            }
        }

        /// <summary>
        /// Gets or sets the selected shipping method.
        /// </summary>
        public ShippingMethod SelectedShippingMethod
        {
            get { return this.selectedShippingMethod; }
            set
            {
                this.selectedShippingMethod = value;
                this.NotifyPropertyChanged("SelectedShippingMethod");
            }
        }

        #endregion Binding Examples

        #region Constructors

        public MainWindowViewModel()
        {
            // Instantiate our item sources.
            this.ShippingCarriers = new ObservableCollection<ShippingCarrier>();
            this.Countries = new ObservableCollection<Country>();
            this.AvailableShippingCarriers = new ObservableCollection<ShippingCarrier>();
            this.AvailableShippingMethods = new ObservableCollection<ShippingMethod>();

            // Simulate data retrieval.
            this.FakeGetShippingCarrierDataFromWebService();
        }

        #endregion Constructors

        #region Simulated Data-Retrieval Methods

        /// <summary>
        /// Simulate the population of data.  In reality this method would most likely be retrieving data from a database, web service, config file, etc.
        /// </summary>
        public void FakeGetShippingCarrierDataFromWebService()
        {
            // Simulate the population of the Countries ObservableCollection.
            this.Countries.Add(new Country("United States", "US"));
            this.Countries.Add(new Country("Puerto Rico", "PR"));
            this.Countries.Add(new Country("Canada", "CA"));
            this.Countries.Add(new Country("Sweden", "SE"));
            this.Countries.Add(new Country("Finland", "FI"));
            this.Countries.Add(new Country("Norway", "NO"));
            this.Countries.Add(new Country("Denmark", "DK"));
            this.Countries.Add(new Country("China", "ZH"));

            // Simulate the population of the ShippingCarriers ObservableCollection.  Note that I just made these up.
            this.ShippingCarriers.Add(new ShippingCarrier(1, "UPS",
                new ShippingMethod(1, "UPS Next Day Air (Early AM)", this.Countries.Where(c => c.TwoLetterCode == "US")),
                new ShippingMethod(2, "UPS Next Day Air", this.Countries.Where(c => new string[] { "US", "PR" }.Contains(c.TwoLetterCode))),
                new ShippingMethod(3, "UPS Ground", this.Countries.Where(c => new string[] { "US", "PR" }.Contains(c.TwoLetterCode))),
                new ShippingMethod(4, "UPS Standard to Canada", this.Countries.Where(c => c.TwoLetterCode == "CA"))
                ));
            this.ShippingCarriers.Add(new ShippingCarrier(2, "DHL Scandinavia",
                new ShippingMethod(5, "DHL Overnight to Scandinavia", this.Countries.Where(c => new string[] { "SE", "FI", "NO", "DK" }.Contains(c.TwoLetterCode))),
                new ShippingMethod(6, "DHL Standard to Scandinavia", this.Countries.Where(c => new string[] { "SE", "FI", "NO", "DK" }.Contains(c.TwoLetterCode)))
                ));

            this.ShippingCarriers.Add(new ShippingCarrier(3, "FedEx",
                new ShippingMethod(7, "FedEx Worldwide"),
                new ShippingMethod(2, "FedEx Express Saver", this.Countries.Where(c => new string[] { "US", "PR" }.Contains(c.TwoLetterCode)))
                ));
        }

        #endregion Simulated Data-Retrieval Methods
    }
}
using System.Collections.Generic;
using System.Linq;

namespace ComboBox.DataBinding.Models
{
    public class ShippingCarrier
    {
        #region Fields

        private List<ShippingMethod> shippingMethods;

        #endregion Fields

        #region Properties [public]

        /// <summary>
        /// Gets the ID which identifies this shipping carrier.
        /// </summary>
        public byte ID { get; private set; }

        /// <summary>
        /// Gets the name of this shipping carrier.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a list of shipping methods available for this shipping carrier.
        /// </summary>
        public IEnumerable<ShippingMethod> ShippingMethods
        {
            get { return this.shippingMethods; }
        }

        #endregion Properties [public]

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ShippingCarrier class.
        /// </summary>
        /// <param name="id">The ID which identifies the shipping carrier.</param>
        /// <param name="name">The name of the shipping carrier.</param>
        /// <param name="shippingMethods">A List of shipping methods supported by the shipping carrier.</param>
        public ShippingCarrier(byte id, string name, params ShippingMethod[] shippingMethods)
        {
            this.ID = id;
            this.Name = name;
            this.shippingMethods = new List<ShippingMethod>(shippingMethods ?? new ShippingMethod[0]);
        }

        #endregion Constructors

        /// <summary>
        /// Gets a list of available shipping methods by country code.
        /// </summary>
        /// <param name="twoLetterCountryCode">The two-letter country code of the destination country.</param>
        /// <returns>A list of available shipping methods for the specified country.</returns>
        public IEnumerable<ShippingMethod> GetAvailableShippingMethodsForCountry(string destinationTwoLetterCountryCode)
        {
            return this.ShippingMethods
                .Where(sm => sm.IsWorldWide || sm.DestinationCountries
                    .Where(dc => dc.TwoLetterCode == destinationTwoLetterCountryCode).Count() > 0);
        }

        /// <summary>
        /// Gets whether or not this carrier can ship to the specified country.
        /// </summary>
        /// <param name="destinationTwoLetterCountryCode">The two-letter country code of the destination country.</param>
        /// <returns>True if the carrier can ship to the specified country, false otherwise.</returns>
        public bool CanShipToCountry(string destinationTwoLetterCountryCode)
        {
            return (this.GetAvailableShippingMethodsForCountry(destinationTwoLetterCountryCode).Count() > 0);
        }
    }
}
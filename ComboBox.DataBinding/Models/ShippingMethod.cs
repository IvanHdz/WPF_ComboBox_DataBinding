using System.Collections.Generic;

namespace ComboBox.DataBinding.Models
{
    public class ShippingMethod
    {
        private List<Country> destinationCountries { get; set; }

        public byte ID { get; private set; }
        public string Name { get; private set; }
        public bool IsWorldWide { get; private set; }

        public IEnumerable<Country> DestinationCountries
        {
            get { return this.destinationCountries; }
        }

        /// <summary>
        /// Initializes a worldwide shipping method.
        /// </summary>
        /// <param name="id">The ID of the shipping method.</param>
        /// <param name="name">The name of the shipping method.</param>
        public ShippingMethod(byte id, string name)
            : this(id, name, null)
        {
            this.IsWorldWide = true;
        }

        /// <summary>
        /// Initializes a shipping method which is only available for select countries.
        /// </summary>
        /// <param name="id">The ID of the shipping method.</param>
        /// <param name="name">The name of the shipping method.</param>
        /// <param name="destinationCountries">A list of countries which this shipping method is available to.</param>
        public ShippingMethod(byte id, string name, IEnumerable<Country> destinationCountries)
        {
            this.ID = id;
            this.Name = name;
            this.IsWorldWide = false;
            this.destinationCountries = new List<Country>(destinationCountries ?? new Country[0]);
        }
    }
}
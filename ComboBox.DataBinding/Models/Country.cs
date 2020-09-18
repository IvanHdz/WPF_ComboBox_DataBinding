namespace ComboBox.DataBinding.Models
{
    public class Country
    {
        public string Name { get; set; }
        public string TwoLetterCode { get; set; }

        public Country(string name, string twoLetterCode)
        {
            this.Name = name;
            this.TwoLetterCode = twoLetterCode;
        }
    }
}
namespace ComputerWebShop.Models
{
    public class Gender : Person
    {
        private string _gender;

        public Gender() : this("", "", "")
        {
            // the ":this("","","")" calls Gender(string newFirstName, string newLastName, string newGender):base(newFirstName,newLastName)
        }

        public Gender(string newFirstName, string newLastName, string newGender) : base(newFirstName, newLastName, GenderType.Male)
        {
            this._gender = newGender;
            // the ":base(newFirstName,newLastName)" calls Person(newFirstName, newLastName)
        }
    }
}
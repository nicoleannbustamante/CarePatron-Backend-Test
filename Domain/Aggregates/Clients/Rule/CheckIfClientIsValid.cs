using Domain.Interfaces;
using System.Text;

namespace Domain.Aggregates.Clients.Rule
{
    internal class CheckIfClientIsValid : IBusinessRule
    {
        private string _message;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _email;
        private readonly string _phoneNumber;
        public string Message => _message;

        internal CheckIfClientIsValid(string firstName, string lastName, string email, string phoneNumber)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phoneNumber = phoneNumber;
        }

        public bool IsBroken()
        {
            var errorMessage = new StringBuilder();

            if (string.IsNullOrEmpty(_firstName))
            {
                errorMessage.AppendLine($"First Name is required");
            }

            if (string.IsNullOrEmpty(_lastName))
            {
                errorMessage.AppendLine($"Last Name is required");
            }

            if (string.IsNullOrEmpty(_email))
            {
                errorMessage.AppendLine($"Email is required");
            }

            if (string.IsNullOrEmpty(_phoneNumber))
            {
                errorMessage.AppendLine($"Phone Number is required");
            }

            if (errorMessage.Length > 0)
            {
                _message = errorMessage.ToString();
            }

            return false;
        }
    }
}

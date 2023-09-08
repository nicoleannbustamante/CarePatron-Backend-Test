using Domain.Aggregates.Clients.Rule;

namespace Domain.Aggregates.Clients
{
    public class Client : Aggregate
    {
        #region Constructors
        public static Client CreateClient(string firstName, string lastName, string email, string phoneNumber) 
            => new Client(firstName, lastName, email, phoneNumber);

        public Client(string firstName, string lastName, string email, string phoneNumber) : base()
        {
            CheckRule(new CheckIfClientIsValid(firstName, lastName, email, phoneNumber));
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        #endregion

        #region Properties
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }
        #endregion

        #region Methods
        public void Update(string firstName, string lastName, string email, string phoneNumber)
        {
            CheckRule(new CheckIfClientIsValid(firstName, lastName, email, phoneNumber));
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;  
        }
        #endregion


    }
}

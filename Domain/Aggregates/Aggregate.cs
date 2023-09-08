using Domain.Aggregates.Clients.Rule;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Aggregates
{
    public class Aggregate
    {
        public Aggregate() 
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                if (rule is CheckIfClientIsValid clientValidationRule)
                {
                    // Custom Domain Exception
                    throw new ClientValidationException(clientValidationRule.Message);
                }
            }
        }
    }
}

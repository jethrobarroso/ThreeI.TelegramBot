using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public class InMemoryData : IDataRepository
    {
        private List<Beneficiary> _beneficiaries;

        public InMemoryData()
        {
            InitializeData();
        }

        public Beneficiary GetBeneficiary(int beneficiaryId)
        {
            var result = _beneficiaries.FirstOrDefault(b => b.Id == beneficiaryId);
            return result;
        }

        public Beneficiary UpdateBeneficiary(Beneficiary beneficiary)
        {
            return null;
        }

        private void InitializeData()
        {
            _beneficiaries = new List<Beneficiary>()
            {
                new Beneficiary()
                {
                    Id = 111,
                    FirstName = "Jonathan",
                    LastName = "Joestark",
                    CellNumber = "0763482937",
                    Conversation = new ConversationState()
                    {
                        Block = "103",
                        Unit = "9",
                        Description = "Water leak",
                        Option = 1,
                        Confirmation = 1,
                        ChatPhase = 5
                    }
                },
                new Beneficiary()
                {
                    Id = 222,
                    FirstName = "Giorno",
                    LastName = "Giovanni",
                    CellNumber = "0834639999",
                    Conversation = new ConversationState()
                }
            };
        }
    }
}

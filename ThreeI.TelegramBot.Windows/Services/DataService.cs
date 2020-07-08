using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Windows.Services
{
    public class DataService : IDataService
    {
        private readonly IDataRepository _repo;

        public DataService(IDataRepository repo)
        {
            _repo = repo;
        }

        public Beneficiary GetBeneficiary(int id)
        {
            return _repo.GetBeneficiary(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Services
{
    public interface IDataService
    {
        Beneficiary GetBeneficiary(int id);
    }
}

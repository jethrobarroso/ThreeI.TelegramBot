using System;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public interface IDataRepository
    {
        Beneficiary GetBeneficiary(int beneficiaryId);
        Beneficiary UpdateBeneficiary(Beneficiary beneficiary);
    }
}

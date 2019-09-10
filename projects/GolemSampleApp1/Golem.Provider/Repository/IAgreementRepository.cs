using Golem.Provider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Repository
{
    public interface IAgreementRepository
    {
        Agreement GetAgreement(string agreementId);
    }
}

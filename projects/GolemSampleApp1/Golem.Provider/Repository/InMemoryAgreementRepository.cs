using System;
using System.Collections.Generic;
using System.Text;
using Golem.Provider.Entities;

namespace Golem.Provider.Repository
{
    public class InMemoryAgreementRepository : IAgreementRepository
    {
        protected Dictionary<string, Agreement> AgreementsByAgreementId { get; set; } = new Dictionary<string, Agreement>();

        public Agreement GetAgreement(string agreementId)
        {
            if (AgreementsByAgreementId.ContainsKey(agreementId))
            {
                return AgreementsByAgreementId[agreementId];
            }
            else
            {
                return null;
            }
        }

        public Agreement SaveAgreement(Agreement agreement)
        {
            lock(AgreementsByAgreementId)
            {
                if(AgreementsByAgreementId.ContainsKey(agreement.AgreementId))
                {
                    AgreementsByAgreementId[agreement.AgreementId] = agreement;
                }
                else
                {
                    AgreementsByAgreementId.Add(agreement.AgreementId, agreement);
                }
            }

            return agreement;
        }
    }
}

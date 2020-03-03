using Golem.Provider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Repository
{
    /// <summary>
    /// Interface of a repository to persist Agreements.
    /// Note, at some point this may receive implementation that would use the Golem Client as repository.
    /// </summary>
    public interface IAgreementRepository
    {
        Agreement GetAgreement(string agreementId);

        /// <summary>
        /// Persist the Agreement content in repository.
        /// </summary>
        /// <param name="agreement"></param>
        /// <returns></returns>
        Agreement SaveAgreement(Agreement agreement);
    }
}

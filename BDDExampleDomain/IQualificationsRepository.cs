using System;
using System.Collections.Generic;

namespace BDDExampleDomain
{
    public interface IQualificationsRepository
    {
        bool Create(Qualification item);
        Qualification Read(Guid qualificationId);
        List<Qualification> ReadAll();
        bool Delete(Guid qualificationId);
        void DeleteAll();
    }
}

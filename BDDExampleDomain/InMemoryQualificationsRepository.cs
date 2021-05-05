using System;
using System.Collections.Generic;

namespace BDDExampleDomain
{
    public class InMemoryQualificationsRepository : IQualificationsRepository
    {
        private List<Qualification> _qualifications = new List<Qualification>();

        public bool Create(Qualification item)
        {
            if (Exists(item.Id))
            {
                return false;
            }

            _qualifications.Add(item);
            return true;
        }

        public bool Delete (Guid qualificationId) => _qualifications.RemoveAll(q => q.Id == qualificationId) > 0;

        public void DeleteAll() => _qualifications.Clear();

        public Qualification Read(Guid qualificationId)
        {
            if (!Exists(qualificationId))
            {
                throw new ArgumentException(nameof(qualificationId));
            }

            return _qualifications.Find(q => q.Id == qualificationId);
        }

        public List<Qualification> ReadAll() => _qualifications;

        private bool Exists(Guid qualificationId) => _qualifications.Exists(q => q.Id == qualificationId);
    }
}

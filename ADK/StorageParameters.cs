using System.Collections.Generic;
using DataAccessLibrary;

namespace GSS
{
    public class StorageParameters
    {
        private readonly Dictionary<StorageRequirements, int> _parameters;

        public StorageParameters(int gen, int cold, int freezer)
        {
            _parameters = new Dictionary<StorageRequirements, int>
            {
                [StorageRequirements.General] = gen,
                [StorageRequirements.Cold] = cold,
                [StorageRequirements.Freezer] = freezer
            };
        }

        public int this[StorageRequirements reqs] => _parameters[reqs];
    }
}
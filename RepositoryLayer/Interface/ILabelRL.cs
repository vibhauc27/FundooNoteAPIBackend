using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public bool CreateLabel(string name, long noteID, long userID);
        public IEnumerable<LabelEntity> GetLabel(long labelID);
    }
}

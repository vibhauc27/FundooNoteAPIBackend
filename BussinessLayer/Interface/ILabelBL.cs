using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ILabelBL
    {
        public bool CreateLabel(string name, long noteID, long userID);
        public IEnumerable<LabelEntity> GetLabel(long labelID);

        public bool DeleteLabel(long LabelID);
    }
}

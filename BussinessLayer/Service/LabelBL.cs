using BussinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL iLabelRL;
        public LabelBL(ILabelRL iLabelRL)
        {
            this.iLabelRL = iLabelRL;
        }

        public bool CreateLabel(string name, long noteID, long userID)
        {
            try
            {
                return iLabelRL.CreateLabel(name, noteID, userID);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<LabelEntity> GetLabel(long labelID)
        {
            try
            {
                return iLabelRL.GetLabel(labelID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}

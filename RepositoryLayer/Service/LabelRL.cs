using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public bool CreateLabel(string name, long noteID, long userID)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.NoteID == noteID).FirstOrDefault();
                if (result != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.Name = name;
                    labelEntity.NoteID = result.NoteID;
                    labelEntity.UserId = result.UserId;
                    fundooContext.LabelTable.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var result = fundooContext.LabelTable.Where(x => x.LabelID == labelID);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteLabel(long LabelID)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.LabelID == LabelID).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.LabelTable.Remove(result);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateLabel(string name, long labelID)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.LabelID == labelID).FirstOrDefault();
                if (result != null)
                {
                    result.Name = name;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

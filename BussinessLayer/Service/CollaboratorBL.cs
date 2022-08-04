using BussinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class CollaboratorBL : ICollaboratorBL
    {
        private readonly ICollaboratorRL iCollabRL;
        public CollaboratorBL(ICollaboratorRL iCollabRL)
        {
            this.iCollabRL = iCollabRL;
        }
        public CollaboratorEntity CreateCollab(long NoteID, string Email)
        {
            try
            {
                return iCollabRL.CreateCollab(NoteID, Email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CollaboratorEntity> GetCollab(long userID)
        {
            try
            {
                return iCollabRL.GetCollab(userID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

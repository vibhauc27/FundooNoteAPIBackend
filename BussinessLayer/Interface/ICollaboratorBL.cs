using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ICollaboratorBL
    {
        public CollaboratorEntity CreateCollab(long NoteID, string Email);
        public IEnumerable<CollaboratorEntity> GetCollab(long userID);
    }
}

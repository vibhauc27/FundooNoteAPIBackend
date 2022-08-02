using CommonLayer.Modal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity AddNotes(NotesModal notesModel, long userId);
        public IEnumerable<NotesEntity> ReadNotes(long userId);
        public bool DeleteNotes(long userId, long noteId);
    }
}

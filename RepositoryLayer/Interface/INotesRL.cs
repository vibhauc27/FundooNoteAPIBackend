using CommonLayer.Modal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity AddNotes(NotesModal notesModal, long userId);
        public IEnumerable<NotesEntity> ReadNotes(long userId);
        public bool DeleteNotes(long userId, long noteId);
        NotesEntity UpdateNote(NotesModal noteModal, long noteId, long userId);
        public bool PinToDashboard(long NoteID, long userId);

        public bool Archive(long NoteID, long userId);
        public bool Trash(long NoteID, long userId);
    }
}

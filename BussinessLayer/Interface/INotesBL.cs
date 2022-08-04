using CommonLayer.Modal;
using Microsoft.AspNetCore.Http;
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
        public NotesEntity UpdateNote(NotesModal noteModal, long NoteId, long userId);
        public bool PinToDashboard(long NoteID, long userId);

        public bool Archive(long NoteID, long userId);
        public bool Trash(long NoteID, long userId);

        public NotesEntity Colour(long NoteID, string colour);
        public string Image(IFormFile image, long noteID, long userID);

    }
}

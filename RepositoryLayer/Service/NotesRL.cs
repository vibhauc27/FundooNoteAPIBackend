﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
        public class NotesRL : INotesRL
        {
            private readonly FundooContext fundooContext;
            private readonly IConfiguration configuration;
            public NotesRL(FundooContext fundooContext, IConfiguration configuration)
            {
                this.fundooContext = fundooContext;
                this.configuration = configuration;


        }
            public NotesEntity AddNotes(NotesModal notesModal, long userId)
            {
                try
                {
                    NotesEntity notesEntity = new NotesEntity();
                    var result = fundooContext.NotesTable.Where(e => e.UserId == userId);
                    if (result != null)
                    {
                        notesEntity.UserId = userId;
                        notesEntity.Title = notesModal.Title;
                        notesEntity.Description = notesModal.Description;
                        notesEntity.Reminder = notesModal.Reminder;
                        notesEntity.Colour = notesModal.Colour;
                        notesEntity.Image = notesModal.Image;
                        notesEntity.Archive = notesModal.Archive;
                        notesEntity.Pin = notesModal.Pin;
                        notesEntity.Trash = notesModal.Trash;
                        notesEntity.Created = notesModal.Created;
                        notesEntity.Edited = notesModal.Edited;

                        fundooContext.NotesTable.Add(notesEntity);
                        fundooContext.SaveChanges();
                        return notesEntity;
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

            public IEnumerable<NotesEntity> ReadNotes(long userId)
            {
                try
                {
                    var result = this.fundooContext.NotesTable.Where(e => e.UserId == userId);
                    return result;
                }

                catch (Exception)
                {

                    throw;
                }

            }

            public bool DeleteNotes(long userId, long noteId)
            {
                try
                {

                    var result = fundooContext.NotesTable.Where(e => e.UserId == userId && e.NoteID == noteId).FirstOrDefault();
                    if (result != null)
                    {
                        fundooContext.NotesTable.Remove(result);
                        this.fundooContext.SaveChanges();
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
            public NotesEntity UpdateNote(NotesModal noteModal, long NoteId, long userId)
            {
                try
                {
                    var result = fundooContext.NotesTable.Where(note => note.UserId == userId && note.NoteID == NoteId).FirstOrDefault();
                    if (result != null)
                    {
                        result.Title = noteModal.Title;
                        result.Description = noteModal.Description;
                        result.Reminder = noteModal.Reminder;
                        result.Edited = DateTime.Now;
                        result.Colour = noteModal.Colour;
                        result.Image = noteModal.Image;

                        this.fundooContext.SaveChanges();
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

        public bool PinToDashboard(long NoteID, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();

                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Archive(long NoteID, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();

                if (result.Archive == true)
                {
                    result.Archive = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Trash(long NoteID, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();

                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity Colour(long NoteID, string colour)
        {
            try
            {
                var findNotes = fundooContext.NotesTable.First(e => e.NoteID == NoteID);
                if (findNotes != null)
                {
                    findNotes.Colour = colour;
                    fundooContext.SaveChanges();
                    return findNotes;
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

        public string Image(IFormFile image, long noteID, long userID)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userID && x.NoteID == noteID).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(
                       this.configuration["CloudinarySettings:CloudName"],
                       this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]
                        );
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    fundooContext.SaveChanges();
                    return "Image uploaded successfully";
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

        
    }
}




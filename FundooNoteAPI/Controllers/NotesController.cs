﻿using BussinessLayer.Interface;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL iNotesBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;

        public NotesController(INotesBL iNotesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iNotesBL = iNotesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }
        [Authorize]
        [HttpPost]
        [Route("CreateNote")]
        public IActionResult CreateNote(NotesModal noteData)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.AddNotes(noteData, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Created Successful", data = result });
                }

                return BadRequest(new { success = false, message = "Notes not Created" });
            }
            catch (Exception)
            {
                // return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
                throw;
            }
        }
        [HttpPost]
        [Route("ReadNotes")]
        public IActionResult ReadNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.ReadNotes(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Received ", data = result });
                }
                return BadRequest(new { success = false, message = "Notes not Received" });
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNotes(long NoteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.DeleteNotes(userId, NoteID);
                if (result != false)
                {
                    return Ok(new { success = true, message = "Note Deleted." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot delete nNte." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateNote")]
        public IActionResult UpdateNote(NotesModal noteModal, long NoteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.UpdateNote(noteModal, NoteID, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Updated Successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot update note." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Pin")]
        public IActionResult pinToDashboard(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.PinToDashboard(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Note Pinned Successfully" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Note Unpinned successfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Archive")]
        public IActionResult Archive(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.Archive(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Note Archived successfully" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Note UnArchived successfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Trash")]
        public IActionResult Trash(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.Trash(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Notes Trashed successfully" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Notes UnTrashed successfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Colour")]
        public IActionResult Colour(long notesId, string colour)
        {
            try
            {
                var result = iNotesBL.Colour(notesId, colour);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Colour Changed", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Colour change Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Image")]
        public IActionResult Image(IFormFile image, long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.Image(image, NoteID, userID);
                if (result != null)
                {
                    return Ok(new { success = true, message = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot upload image." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long userID = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "userID").Value);

            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }

    }
}
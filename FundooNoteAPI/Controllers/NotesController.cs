using BussinessLayer.Interface;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {

        private readonly INotesBL iNotesBL;

        public NotesController(INotesBL iNotesBL)
        {
            this.iNotesBL = iNotesBL;
        }
        
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

        
        [HttpGet]
        [Route("ReadNote")]
        public IActionResult ReadNotes()
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.ReadNotes(userID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Able to read the notes.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to read the notes." });
                }
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
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.DeleteNotes(userID, NoteID);
                if (result != false)
                {
                    return Ok(new { success = true, message = "Note Deleted." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot delete note." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

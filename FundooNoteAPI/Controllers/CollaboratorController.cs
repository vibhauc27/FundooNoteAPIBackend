using BussinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL icollabBL;
        public CollaboratorController(ICollaboratorBL icollabBL)
        {
            this.icollabBL = icollabBL;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCollab(long NoteID, string Email)
        {
            try
            {
                var result = icollabBL.CreateCollab(NoteID, Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborator Created successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Collaborator not Created" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

using BussinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL iLabelBL;
        public LabelController(ILabelBL iLabelBL)
        {
            this.iLabelBL = iLabelBL;
        }


        [HttpPost]
        [Route("Create")]
        public IActionResult CreateLabel(string Name, long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iLabelBL.CreateLabel(Name, noteID, userID);
                if (result)
                {
                    return Ok(new { success = true, message = "Label Created successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label has not created" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult GetLabel(long LabelID)
        {
            try
            {
                var result = iLabelBL.GetLabel(LabelID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Ablle to get the labels", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to get the labels" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

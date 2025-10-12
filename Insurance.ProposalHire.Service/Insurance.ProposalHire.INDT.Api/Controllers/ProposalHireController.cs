using Microsoft.AspNetCore.Mvc;

namespace Insurance.HireProposal.INDT.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProposalHireController : ControllerBase
    {
    
        private readonly ILogger<ProposalHireController> _logger;

        public ProposalHireController(ILogger<ProposalHireController> logger)
        {
            _logger = logger;
        }

       
    }
}

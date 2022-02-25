using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.CreateManifest;
using SadadMisr.BLL.Models.DeleteManifest;
using SadadMisr.BLL.Models.GetByIds;
using SadadMisr.BLL.Models.Manifests.GetByIds;
using SadadMisr.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ManifestsController : ControllerBase
    {
        private readonly IManifestService _manifestService;

        public ManifestsController(IManifestService manifestService)
        {
            _manifestService = manifestService;
        }

        [HttpPost]
        public async Task<Output<bool>> Create([FromBody] CreateManifestRequest request, CancellationToken cancellationToken)
        {
            return await _manifestService.CreateManifest(request, cancellationToken);
        }

        [HttpPost("getdetailsbyids")]
        public async Task<Output<List<ManifestModel>>> GetById(GetDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            return await _manifestService.GetManifestByIds(query, cancellationToken);
        }
        [HttpPut]
        public async Task<Output<bool>> Update([FromBody] UpdateManifestRequest request, CancellationToken cancellationToken)
        {
            return await _manifestService.UpdateManifest(request, cancellationToken);
        }

        [HttpDelete]
        public async Task<Output<bool>> Delete([FromBody] DeleteManifestRequest request, CancellationToken cancellationToken)
        {
            return await _manifestService.DeleteManifest(request, cancellationToken);
        }

        [HttpGet]
        public async Task<Output<List<ManifestModel>>> GetAll(CancellationToken cancellationToken)
        {
            return await _manifestService.GetAllManifests(cancellationToken);
        }

    }
}
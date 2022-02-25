using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.CreateManifest;
using SadadMisr.BLL.Models.DeleteManifest;
using SadadMisr.BLL.Models.GetByIds;
using SadadMisr.BLL.Models.Manifests.GetByIds;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Interfaces
{
    public interface IManifestService
    {
        Task<Output<bool>> CreateManifest(CreateManifestRequest request, CancellationToken cancellationToken);
        Task<Output<bool>> UpdateManifest(UpdateManifestRequest request, CancellationToken cancellationToken);
        Task<Output<bool>> DeleteManifest(DeleteManifestRequest request, CancellationToken cancellationToken);
        Task<Output<List<ManifestModel>>> GetManifestByIds(GetDetailsByIdsQuery query, CancellationToken cancellationToken);
        Task<Output<List<ManifestModel>>> GetAllManifests(CancellationToken cancellationToken);
    }
}